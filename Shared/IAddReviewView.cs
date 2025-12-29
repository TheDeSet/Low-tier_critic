using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shared
{
    public interface IAddReviewView : IFormGeneral
    {
        string Username { get; }
        string RatingText { get; }
        string ReviewText { get; }

        event EventHandler AddReviewRequested;
        event EventHandler ResetRequested;

        ReviewDTO GetReviewFromInput();
        void ShowMessage(string text, string caption);
        void CloseView();
    }
}
