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
        event EventHandler AddReviewRequested;
        void ShowGame(Game game);
        void ShowMessage(string text, string caption);
    }
}
