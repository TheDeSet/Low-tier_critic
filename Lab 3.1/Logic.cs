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

        public static List<Game> GetFilteredGames(
                                                    string searchField,
                                                    string searchText,
                                                    string sortOption,
                                                    string seriesFilter)
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

        // Метод для добавления игры (для тестовых данных)
        public static void AddGame(Game game)
        {
            _games.Add(game);
        }

        public static bool DeleteGame(int gameId)
        {
            var game = _games.FirstOrDefault(g => g.ID == gameId);
            if (game == null) return false;

            return _games.Remove(game);
        }
        // ... другие методы: обновление и т.д.
    }
}
