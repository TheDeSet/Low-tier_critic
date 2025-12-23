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
        event Action<Game> GameUpdated;
        DialogResult ShowDialog();
        void LoadGame(Game game);
        void SetFormState(bool isValid, string errorMessage);
        void ShowMessage(string message, string title);
    }
}
