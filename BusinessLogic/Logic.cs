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
        /// <summary>
        /// Переключает используемый слой доступа к данным между Entity Framework и Dapper.
        /// </summary>
        /// <param name="useEF">Если true, использует Entity Framework; если false, использует Dapper.</param>
        public static void ToggleDataAccessLayer(bool useEF)
        {
            /*if (useEF == true)
                useEntityFramework = true;
            if (useEF == false)
                useEntityFramework = false;*/

            useEntityFramework = useEF;
        }

        /*static AppDBContext? dbContext;
        static IRepository<Game>? gamesEntityFrameWork;
        static IRepository<Review>? reviewEntityFrameWork;
        static DataAccessLayer.DapperRepository<Game> gamesDapper = new DataAccessLayer.DapperRepository<Game>();
        static DataAccessLayer.DapperRepository<Review> reviewsDapper = new DataAccessLayer.DapperRepository<Review>();
        static IRepository<Game> GameRepo => useEntityFramework ? gamesEntityFrameWork! : gamesDapper;
        static IRepository<Review> ReviewRepo => useEntityFramework ? reviewEntityFrameWork! : reviewsDapper;

        /// <summary>
        /// Инициализирует репозиторий EntityFramework
        /// </summary>
        private static void InitializeEF()
        {
            dbContext = new AppDBContext();
            dbContext.Database.EnsureCreated();

            gamesEntityFrameWork = new EntityRepository<Game>(dbContext);
            reviewEntityFrameWork = new EntityRepository<Review>(dbContext);
        }
        static Logic()
        {
            InitializeEF();
        }

        private static void SaveChanges()
        {
            if (useEntityFramework)
                dbContext!.SaveChanges();
            
        }*/


        /// <summary>
        /// Получает игру по указанному ID.
        /// </summary>
        /// <param name="id">ID игры для поиска.</param>
        /// <returns>Объект Game, если найден; иначе null.</returns>
        public static Game GetGameById(int id)
        {
            //return GameRepo.ReadById(id);

            using var uow = UnitOfWorkContextWork.Create(useEntityFramework);
            return uow.GameRepository.ReadById(id);
        }

        /// <summary>
        /// Возвращает все игры из хранилища.
        /// </summary>
        /// <returns>Список всех игр.</returns>
        public static List<Game> GetGames()
        {
            //return GameRepo.ReadAll();

            using var uow = UnitOfWorkContextWork.Create(useEntityFramework);
            return uow.GameRepository.ReadAll();
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
            //var result = GameRepo.ReadAll().AsEnumerable();

            using var uow = UnitOfWorkContextWork.Create(useEntityFramework);
            var result = uow.GameRepository.ReadAll().AsEnumerable();

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
            using var uow = UnitOfWorkContextWork.Create(useEntityFramework);
            // Инициализируем списки, если null
            game.Platforms ??= new List<EnumPlatforms>();
            game.Screenshots ??= new List<string>();
            game.Reviews ??= new List<Review>();

            uow.GameRepository.Add(game);
            uow.SaveChanges();

            /*GameRepo.Add(game);
            SaveChanges();*/
        }

        /// <summary>
        /// Обновляет существующую игру по ID.
        /// </summary>
        /// <param name="updatedGame">Обновленный объект игры.</param>
        /// <returns>true, если игра обновлена; иначе false.</returns>
        public static bool UpdateGame(Game updatedGame)
        {
            using var uow = UnitOfWorkContextWork.Create(useEntityFramework);
            var existingGame = uow.GameRepository.ReadById(updatedGame.ID);

            //var existingGame = GameRepo.ReadAll().FirstOrDefault(g => g.ID == updatedGame.ID);
            if (existingGame == null) return false;

            existingGame.Name = updatedGame.Name;
            existingGame.Developer = updatedGame.Developer;
            existingGame.YearOfRelease = updatedGame.YearOfRelease;
            existingGame.Platforms = updatedGame.Platforms;
            existingGame.Rating = updatedGame.Rating;
            existingGame.Description = updatedGame.Description;
            existingGame.Icon = updatedGame.Icon;
            existingGame.Screenshots = updatedGame.Screenshots;

            uow.GameRepository.Update(existingGame);
            uow.SaveChanges();

            /*GameRepo.Update(updatedGame);
            SaveChanges();*/
            return true;
        }

        /// <summary>
        /// Удаляет игру по указанному ID из хранилища.
        /// </summary>
        /// <param name="gameId">ID удаляемой игры.</param>
        /// <returns>true, если игра удалена; иначе false.</returns>
        public static bool DeleteGame(int gameId)
        {
            using var uow = UnitOfWorkContextWork.Create(useEntityFramework);
            var game = uow.GameRepository.ReadById(gameId);

            //var game = GameRepo.ReadAll().FirstOrDefault(g => g.ID == gameId);

            if (game == null) return false;

            uow.GameRepository.Delete<Game>(gameId); // Удаляет и отзывы через Dapper UoW
            uow.SaveChanges();

            /*if (game.Reviews != null)
            {
                foreach (Review review in game.Reviews)
                {
                    ReviewRepo.Delete<Review>(review.ID);
                }
            }
            GameRepo.Delete<Game>(gameId);
            SaveChanges();*/
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
            using var uow = UnitOfWorkContextWork.Create(useEntityFramework);
            var game = uow.GameRepository.ReadById(gameId);

            //var game = GameRepo.ReadAll().FirstOrDefault(g => g.ID == gameId);
            if (game == null) return false;

            game.Reviews ??= new List<Review>();
            review.Username = string.IsNullOrWhiteSpace(review.Username) ? "Аноним" : review.Username.Trim();
            review.Rating = Math.Max(1.0f, Math.Min(5.0f, review.Rating));

            review.GameId = gameId;
            uow.ReviewRepository.Add(review);
            uow.SaveChanges();

            //dbContext.Entry(review).Property("GameId").CurrentValue = gameId;
            /*game.Reviews.Add(review);
            ReviewRepo.Add(review);
            SaveChanges();*/

            var updatedGame = uow.GameRepository.ReadById(gameId);
            if (updatedGame?.Reviews?.Count > 0)
            {
                updatedGame.Rating = updatedGame.Reviews.Average(r => r.Rating);
                uow.GameRepository.Update(updatedGame);
                uow.SaveChanges();
            }

            /*if (game.Reviews.Count > 0)
            {
                game.Rating = game.Reviews.Average(r => r.Rating);
                GameRepo.Update(game);
                SaveChanges();
            }*/

            return true;
        }

    }
}
