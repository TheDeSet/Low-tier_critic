using Lab_3._1.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3._1
{
    public class Logic
    {
        // Пример: хранилище в памяти. В будущем можно заменить на БД или файл.
        private static List<Game> _games = new List<Game>();

        static Logic()
        {
            // Загружаем тестовые данные при первом обращении
            _games = TestData.GenerateSampleGames();
        }

        // Метод для получения игры по ID
        public static Game GetGameById(int id)
        {
            return _games.FirstOrDefault(g => g.ID == id);
        }

        // Метод для получения всех игр (для плиток)
        public static List<Game> GetGames()
        {
            return _games;
        }

        public static List<Game> GetFilteredGames(string searchField, string searchText, string sortOption)
        {
            var result = _games.AsEnumerable();

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
            // Сортировка
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

        public static void AddGame(Game game)
        {
            // Автоматически назначаем ID
            game.ID = _games.Count > 0 ? _games.Max(g => g.ID) + 1 : 1;

            // Инициализируем списки, если null
            game.Platforms ??= new List<EnumPlatforms>();
            game.Screenshots ??= new List<Image>();
            game.Reviews ??= new List<Review>();

            
            _games.Add(game);
        }
        public static bool UpdateGame(Game updatedGame)
        {
            var existingGame = _games.FirstOrDefault(g => g.ID == updatedGame.ID);
            if (existingGame == null) return false;

            
            existingGame.Name = updatedGame.Name;
            existingGame.Developer = updatedGame.Developer;
            existingGame.YearOfRelease = updatedGame.YearOfRelease;
            existingGame.Platforms = updatedGame.Platforms;
            existingGame.Description = updatedGame.Description;
            existingGame.Icon = updatedGame.Icon;
            existingGame.Screenshots = updatedGame.Screenshots;

            return true;
        }

        public static bool DeleteGame(int gameId)
        {
            var game = _games.FirstOrDefault(g => g.ID == gameId);
            if (game == null) return false;

            return _games.Remove(game);
        }

        public static bool AddReviewToGame(int gameId, Review review)
        {
            var game = _games.FirstOrDefault(g => g.ID == gameId);
            if (game == null) return false;

            // Инициализируем список, если null
            game.Reviews ??= new List<Review>();

            // Автоимя "Аноним", если не указано
            review.Username = string.IsNullOrWhiteSpace(review.Username) ? "Аноним" : review.Username.Trim();

            // Ограничиваем рейтинг 1.0–5.0
            review.Rating = Math.Max(1.0f, Math.Min(5.0f, review.Rating));

            // Добавляем отзыв
            game.Reviews.Add(review);

            // ➤➤➤ Опционально: пересчитываем рейтинг игры (среднее по отзывам)
            if (game.Reviews.Count > 0)
            {
                game.Rating = game.Reviews.Average(r => r.Rating);
            }

            return true;
        }

    }
}
