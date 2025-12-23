using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shared
{
    public interface IAddGameView
    {
        string GameName { get; }
        string Developer { get; }
        string Description { get; }
        string YearOfRelease { get; }
        string Icon { get; }
        List<int> SelectedPlatforms { get; }
        List<string> Screenshots { get; }

        event EventHandler AddGameRequested;
        event EventHandler ResetRequested;

        void ShowMessage(string text, string caption);
        void CloseView();
    }
}
