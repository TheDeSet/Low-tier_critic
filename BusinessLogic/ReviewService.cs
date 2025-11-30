using BusinessLogic.Services;
using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork unitOfWork;
        public ReviewService(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }
        public List<Review> GetReviewsByGameId(int gameId)
        {
            var game = unitOfWork.GameRepository.ReadById(gameId);
            return game?.Reviews ?? new List<Review>();
        }
        /// <summary>
        /// Добавляет отзыв к указанной игре.
        /// </summary>
        /// <param name="gameId">ID игры, к которой добавляется отзыв.</param>
        /// <param name="review">Объект отзыва для добавления.</param>
        /// <remarks>
        /// Автоматически устанавливает имя "Аноним" при отсутствии, ограничивает рейтинг в диапазоне 1.0–5.0, пересчитывает средний рейтинг игры.
        /// </remarks>
        /// <returns>true, если отзыв добавлен; иначе false.</returns>
        public bool AddReviewToGame(int gameId, Review review)
        {
            unitOfWork.Begin();
            var game = unitOfWork.GameRepository.ReadById(gameId);

            if (game == null) return false;

            game.Reviews ??= new List<Review>();
            review.Username = string.IsNullOrWhiteSpace(review.Username) ? "Аноним" : review.Username.Trim();
            review.Rating = Math.Max(1.0f, Math.Min(5.0f, review.Rating));

            review.GameId = gameId;
            unitOfWork.ReviewRepository.Add(review);

            var updatedGame = unitOfWork.GameRepository.ReadById(gameId);
            if (updatedGame?.Reviews?.Count > 0)
            {
                updatedGame.Rating = updatedGame.Reviews.Average(r => r.Rating);
                unitOfWork.GameRepository.Update(updatedGame);
            }
            unitOfWork.SaveChanges();
            return true;
        }
    }
}
