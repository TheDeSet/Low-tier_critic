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
        event Action<int> GameSelected;
        event Action<int> GameOpened;
        void SetGame(Game game);
        void SetSelected(bool selected);
    }
}
