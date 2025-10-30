using Entities;
using Entities.Test;
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
        private static List<Game> _games = new List<Game>();

        static DataAccessLayer.DapperRepository<Game> gamesDapper = new DataAccessLayer.DapperRepository<Game>();
        static DataAccessLayer.DapperRepository<Review> reviewsDapper = new DataAccessLayer.DapperRepository<Review>();

        static Logic()
        {
            _games = TestData.GenerateSampleGames();
        }

        /// <summary>
        /// Получает игру по указанному ID.
        /// </summary>
        /// <param name="id">ID игры для поиска.</param>
        /// <returns>Объект Game, если найден; иначе null.</returns>
        public static Game GetGameById(int id)
        {
            return gamesDapper.ReadById(id);
        }

        /// <summary>
        /// Возвращает все игры из хранилища.
        /// </summary>
        /// <returns>Список всех игр.</returns>
        public static List<Game> GetGames()
        {
            return gamesDapper.ReadAll();
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
            var result = gamesDapper.ReadAll().AsEnumerable();

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

            gamesDapper.Add(game);
        }

        /// <summary>
        /// Обновляет существующую игру по ID.
        /// </summary>
        /// <param name="updatedGame">Обновленный объект игры.</param>
        /// <returns>true, если игра обновлена; иначе false.</returns>
        public static bool UpdateGame(Game updatedGame)
        {
            var existingGame = gamesDapper.ReadAll().FirstOrDefault(g => g.ID == updatedGame.ID);
            if (existingGame == null) return false;

            gamesDapper.Update(updatedGame);
            
            /*existingGame.Name = updatedGame.Name;
            existingGame.Developer = updatedGame.Developer;
            existingGame.YearOfRelease = updatedGame.YearOfRelease;
            existingGame.Platforms = updatedGame.Platforms;
            existingGame.Description = updatedGame.Description;
            existingGame.Icon = updatedGame.Icon;
            existingGame.Screenshots = updatedGame.Screenshots;*/

            return true;
        }

        /// <summary>
        /// Удаляет игру по указанному ID из хранилища.
        /// </summary>
        /// <param name="gameId">ID удаляемой игры.</param>
        /// <returns>true, если игра удалена; иначе false.</returns>
        public static bool DeleteGame(int gameId)
        {
            var game = gamesDapper.ReadAll().FirstOrDefault(g => g.ID == gameId);
            if (game == null) return false;

            foreach (Review review in game.Reviews)
            {
                reviewsDapper.Delete<Review>(review.ID);
            }
            gamesDapper.Delete<Game>(gameId);

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
            var game = gamesDapper.ReadAll().FirstOrDefault(g => g.ID == gameId);
            if (game == null) return false;

            game.Reviews ??= new List<Review>();

            review.Username = string.IsNullOrWhiteSpace(review.Username) ? "Аноним" : review.Username.Trim();

            review.Rating = Math.Max(1.0f, Math.Min(5.0f, review.Rating));

            reviewsDapper.Add(review);

            if (game.Reviews.Count > 0)
            {
                game.Rating = game.Reviews.Average(r => r.Rating);
            }

            return true;
        }

    }
}
