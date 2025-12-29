    using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shared
{
    public interface IUpdateGameView
    {
        int GameId { get; }
        string GameName { get; }
        string Developer { get; }
        string YearOfRelease { get; }
        string Description { get; }

        event EventHandler UpdateRequested;
        event EventHandler ResetRequested;

        GameDTO GetGameFromInput();
        void SetGameForForm(GameDTO game);
        void ShowMessage(string text, string caption);
        void CloseView();
    }
}
