using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public interface IGameTileView
    {
        event EventHandler<int> GameSelected;
        event EventHandler AddGameRequested;
        event EventHandler UpdateGameRequested;
        event EventHandler DeleteGameRequested;
        void ShowGames(List<GameDTO> games);
        int SelectedGameId { get; }
        void ShowMessage(string text, string caption);
    }
}
