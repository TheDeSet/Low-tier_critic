using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public interface IGameDetailsView
    {
        event Action<int> MakeReviewRequested;
        event Action<int> GameUpdated;
        void LoadGameData(Game game);
        void ShowMessage(string message, string title);
    }
}
