using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    internal interface IConsoleView : IFormGeneral
    {
        event EventHandler ShowGamesRequested;
        event EventHandler ExitRequested;
        void ShowText(string text);
    }
}
