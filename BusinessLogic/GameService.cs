using BusinessLogic.Services;
using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork unitOfWork;
        public GameService(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }
        public Game GetGameById(int id)
        {
            return unitOfWork.GameRepository.ReadById(id);
        }

        public List<Game> GetGames()
        {
            return unitOfWork.GameRepository.ReadAll();
        }

        /*static Logic()
        {
            List<Game> _games = TestData.GenerateSampleGames();
            using var uow = UnitOfWorkContextWork.Create(useEntityFramework);
            if (uow.GameRepository.ReadAll().Count == 0)
            {
                foreach (var game in _games)
                {

                    var gameCopy = new Game
                    {
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
                            Username = review.Username,
                            Rating = review.Rating,
                            ReviewText = review.ReviewText
                        };
                        uow.ReviewRepository.Add(reviewCopy);
                        uow.ReviewRepository.GetType().GetMethod("SaveChanges")?.Invoke(uow.ReviewRepository, null);
                        gameCopy.Reviews.Add(reviewCopy);
                    }

                    uow.GameRepository.Add(gameCopy);
                }
                uow.SaveChanges();
            }
        }*/


        /// <summary>
        /// Фильтрует и сортирует игры по заданным параметрам.
        /// </summary>
        /// <param name="searchField">Поле для поиска (названию, разработчику, искать по всему).</param>
        /// <param name="searchText">Текст для поиска.</param>
        /// <param name="sortOption">Опция сортировки (возрастанию (рейтинг), убыванию (рейтинг)).</param>
        /// <returns>Отфильтрованный и отсортированный список игр.</returns>
        public List<Game> GetFilteredGames(string searchField, string searchText, string sortOption)
        {
            unitOfWork.Begin();
            var result = unitOfWork.GameRepository.ReadAll().AsEnumerable();

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
        public void AddGame(Game game)
        {
            unitOfWork.Begin();

            game.Platforms ??= new List<EnumPlatforms>();
            game.Screenshots ??= new List<string>();
            game.Reviews ??= new List<Review>();

            unitOfWork.GameRepository.Add(game);
            unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Обновляет существующую игру по ID.
        /// </summary>
        /// <param name="updatedGame">Обновленный объект игры.</param>
        /// <returns>true, если игра обновлена; иначе false.</returns>
        public bool UpdateGame(Game updatedGame)
        {
            unitOfWork.Begin();

            var existingGame = unitOfWork.GameRepository.ReadById(updatedGame.ID);
            if (existingGame == null) return false;

            existingGame.Name = updatedGame.Name;
            existingGame.Developer = updatedGame.Developer;
            existingGame.YearOfRelease = updatedGame.YearOfRelease;
            existingGame.Platforms = updatedGame.Platforms;
            existingGame.Rating = updatedGame.Rating;
            existingGame.Description = updatedGame.Description;
            existingGame.Icon = updatedGame.Icon;
            existingGame.Screenshots = updatedGame.Screenshots;

            unitOfWork.GameRepository.Update(existingGame);
            unitOfWork.SaveChanges();
            return true;
        }

        /// <summary>
        /// Удаляет игру по указанному ID из хранилища.
        /// </summary>
        /// <param name="gameId">ID удаляемой игры.</param>
        /// <returns>true, если игра удалена; иначе false.</returns>
        public bool DeleteGame(int gameId)
        {
            unitOfWork.Begin();
            var game = unitOfWork.GameRepository.ReadById(gameId);

            if (game == null) return false;

            unitOfWork.GameRepository.Delete<Game>(gameId);
            unitOfWork.SaveChanges();

            return true;
        }
    }
}
