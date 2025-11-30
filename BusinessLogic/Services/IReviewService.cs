using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public interface IReviewService
    {
        bool AddReviewToGame(int gameId, Review review);
        List<Review> GetReviewsByGameId(int gameId);
    }
}
