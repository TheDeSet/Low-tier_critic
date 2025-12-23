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
        event Action<Game> GameAdded;
        DialogResult ShowDialog();
        void SetFormState(bool isValid, string errorMessage);
        void ResetForm();
        void ShowMessage(string message, string title);
    }
}
