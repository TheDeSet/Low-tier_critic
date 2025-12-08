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

        public List<Game> GetFilteredGames(string searchField, string searchText, string sortOption)
        {
            unitOfWork.TransactionBegin();
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

        public void AddGame(Game game)
        {
            unitOfWork.TransactionBegin();

            game.Platforms ??= new List<EnumPlatforms>();
            game.Screenshots ??= new List<string>();
            game.Reviews ??= new List<Review>();

            unitOfWork.GameRepository.Add(game);
            unitOfWork.SaveChanges();
        }

        public bool UpdateGame(Game updatedGame)
        {
            unitOfWork.TransactionBegin();

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

        public bool DeleteGame(int gameId)
        {
            unitOfWork.TransactionBegin();
            var game = unitOfWork.GameRepository.ReadById(gameId);

            if (game == null) return false;

            unitOfWork.GameRepository.Delete<Game>(gameId);
            unitOfWork.SaveChanges();

            return true;
        }
    }
}
