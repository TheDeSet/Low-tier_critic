using DataAccessLayer;
using Entities;
using Entities.Test;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessLogic
{
    public class Logic
    {
        //Переключатель
        public static bool useEntityFramework = true;
        public static void ToggleDataAccessLayer(bool useEF)
        {
            if (useEF == false)
                useEntityFramework = false;
            else
                useEntityFramework = true;
        }
        // Хранилище в памяти. В будущем можно заменить на БД или файл.
        static List<Game> _games = new List<Game>();

        static AppDBContext? dbContext;
        static IRepository<Game>? gamesEntityFrameWork;
        static IRepository<Review>? reviewEntityFrameWork;
        static DataAccessLayer.DapperRepository<Game> gamesDapper = new DataAccessLayer.DapperRepository<Game>();
        static DataAccessLayer.DapperRepository<Review> reviewsDapper = new DataAccessLayer.DapperRepository<Review>();
        static IRepository<Game> GameRepo => useEntityFramework ? gamesEntityFrameWork! : gamesDapper;
        static IRepository<Review> ReviewRepo => useEntityFramework ? reviewEntityFrameWork! : reviewsDapper;

        private static void InitializeEF()
        {
            dbContext = new AppDBContext();
            dbContext.Database.EnsureCreated();

            gamesEntityFrameWork = new EntityRepository<Game>(dbContext);
            reviewEntityFrameWork = new EntityRepository<Review>(dbContext);

            // Заполняет БД тестовыми данными, если пусто
            if (gamesEntityFrameWork.ReadAll().Count==0)
            {
                foreach (var game in _games)
                {

                    var gameCopy = new Game
                    {
                        ID = game.ID,
                        Name = game.Name,
                        Developer = game.Developer,
                        YearOfRelease = game.YearOfRelease,
                        Platforms = new List<EnumPlatforms>(game.Platforms),
                        Rating = game.Rating,
                        Description = game.Description,
                        Icon = game.Icon,
                        Screenshots = new List<string>(game.Screenshots),
                        Reviews = new List<Review>()
                    };


                    foreach (var review in game.Reviews)
                    {
                        var reviewCopy = new Review
                        {
                            ID = review.ID,
                            Username = review.Username,
                            Rating = review.Rating,
                            ReviewText = review.ReviewText
                        };
                        reviewEntityFrameWork.Add(reviewCopy);
                        reviewEntityFrameWork.GetType().GetMethod("SaveChanges")?.Invoke(reviewEntityFrameWork, null);
                        gameCopy.Reviews.Add(reviewCopy);
                    }

                    gamesEntityFrameWork.Add(gameCopy);
                }
                dbContext.SaveChanges();
            }
        }
        static Logic()
        {
            _games = TestData.GenerateSampleGames();
            InitializeEF();
        }
        private static void SaveChanges()
        {
            if (useEntityFramework)
                dbContext!.SaveChanges();
            // Dapper сохраняет сразу 
        }
        /// <summary>
        /// Получает игру по указанному ID.
        /// </summary>
        /// <param name="id">ID игры для поиска.</param>
        /// <returns>Объект Game, если найден; иначе null.</returns>
        public static Game GetGameById(int id)
        {
            return GameRepo.ReadById(id);
        }

        /// <summary>
        /// Возвращает все игры из хранилища.
        /// </summary>
        /// <returns>Список всех игр.</returns>
        public static List<Game> GetGames()
        {
            return GameRepo.ReadAll();
        }

        /// <summary>
        /// Фильтрует и сортирует игры по заданным параметрам.
        /// </summary>
        /// <param name="searchField">Поле для поиска (названию, разработчику, искать по всему).</param>
        /// <param name="searchText">Текст для поиска.</param>
        /// <param name="sortOption">Опция сортировки (возрастанию (рейтинг), убыванию (рейтинг)).</param>
        /// <returns>Отфильтрованный и отсортированный список игр.</returns>
        public static List<Game> GetFilteredGames(string searchField, string searchText, string sortOption)
        {
            var result = GameRepo.ReadAll().AsEnumerable();


            // Фильтрация по поиску
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                string query = searchText.ToLower();
                switch (searchField)
                {
                    case "названию":
                        result = result.Where(g => g.Name?.ToLower().Contains(query) == true);
                        break;
                    case "разработчику":
                        result = result.Where(g => g.Developer?.ToLower().Contains(query) == true);
                        break;
                    case "искать по всему":
                    default:
                        result = result.Where(g =>
                            (g.Name?.ToLower().Contains(query) == true) ||
                            (g.Developer?.ToLower().Contains(query) == true));
                        break;
                }
            }
            switch (sortOption)
            {
                case "возрастанию (рейтинг)":
                    result = result.OrderBy(g => g.Rating ?? 0);
                    break;
                case "убыванию (рейтинг)":
                    result = result.OrderByDescending(g => g.Rating ?? 0);
                    break;
            }
            return result.ToList();
        }

        /// <summary>
        /// Добавляет новую игру в хранилище.
        /// </summary>
        /// <param name="game">Объект игры для добавления.</param>
        /// <remarks>
        /// Автоматически назначает ID (максимальный текущий +1), инициализирует пустые списки (Platforms, Screenshots, Reviews) при необходимости.
        /// </remarks>
        public static void AddGame(Game game)
        {
            // Автоматически назначаем ID
            game.ID = _games.Count > 0 ? _games.Max(g => g.ID) + 1 : 1;

            // Инициализируем списки, если null
            game.Platforms ??= new List<EnumPlatforms>();
            game.Screenshots ??= new List<string>();
            game.Reviews ??= new List<Review>();

            GameRepo.Add(game);
            SaveChanges();
        }

        /// <summary>
        /// Обновляет существующую игру по ID.
        /// </summary>
        /// <param name="updatedGame">Обновленный объект игры.</param>
        /// <returns>true, если игра обновлена; иначе false.</returns>
        public static bool UpdateGame(Game updatedGame)
        {
            var existingGame = GameRepo.ReadAll().FirstOrDefault(g => g.ID == updatedGame.ID);
            if (existingGame == null) return false;

            GameRepo.Update(updatedGame);

            /*existingGame.Name = updatedGame.Name;
            existingGame.Developer = updatedGame.Developer;
            existingGame.YearOfRelease = updatedGame.YearOfRelease;
            existingGame.Platforms = updatedGame.Platforms;
            existingGame.Description = updatedGame.Description;
            existingGame.Icon = updatedGame.Icon;
            existingGame.Screenshots = updatedGame.Screenshots;*/
            SaveChanges();
            return true;
        }

        /// <summary>
        /// Удаляет игру по указанному ID из хранилища.
        /// </summary>
        /// <param name="gameId">ID удаляемой игры.</param>
        /// <returns>true, если игра удалена; иначе false.</returns>
        public static bool DeleteGame(int gameId)
        {
            var game = GameRepo.ReadAll().FirstOrDefault(g => g.ID == gameId);
            if (game == null) return false;

            foreach (Review review in game.Reviews)
            {
                reviewsDapper.Delete<Review>(review.ID);
            }
            GameRepo.Delete<Game>(gameId);
            SaveChanges();
            return true;
        }

        /// <summary>
        /// Добавляет отзыв к указанной игре.
        /// </summary>
        /// <param name="gameId">ID игры, к которой добавляется отзыв.</param>
        /// <param name="review">Объект отзыва для добавления.</param>
        /// <remarks>
        /// Автоматически устанавливает имя "Аноним" при отсутствии, ограничивает рейтинг в диапазоне 1.0–5.0, пересчитывает средний рейтинг игры.
        /// </remarks>
        /// <returns>true, если отзыв добавлен; иначе false.</returns>
        public static bool AddReviewToGame(int gameId, Review review)
        {
            var game = GameRepo.ReadAll().FirstOrDefault(g => g.ID == gameId);
            if (game == null) return false;

            game.Reviews ??= new List<Review>();

            review.Username = string.IsNullOrWhiteSpace(review.Username) ? "Аноним" : review.Username.Trim();

            review.Rating = Math.Max(1.0f, Math.Min(5.0f, review.Rating));

            ReviewRepo.Add(review);
            SaveChanges();

            if (game.Reviews.Count > 0)
            {
                game.Rating = game.Reviews.Average(r => r.Rating);
                GameRepo.Update(game);
                SaveChanges();
            }

            return true;
        }

    }
}
