using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public interface IReviewView
    {
        void ShowReviews(List<Review> reviews);
        void ShowMessage(string text, string caption);
    }
}
