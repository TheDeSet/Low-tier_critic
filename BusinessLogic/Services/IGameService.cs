using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public interface IGameService
    {
        Game GetGameById(int id);
        List<Game> GetGames();
        List<Game> GetFilteredGames(string searchField, string searchText, string sortOption);
        void AddGame(Game game);
        bool UpdateGame(Game updatedGame);
        bool DeleteGame(int gameId);
    }
}
