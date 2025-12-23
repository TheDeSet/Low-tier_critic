using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Services;
using Entities;
using Shared;

namespace Presenter
{
    public class AddReviewPresenter
    {
        private readonly IAddReviewView view;
        private readonly IReviewService reviewService;
        private readonly int gameId;
        private Review newReview;
        public AddReviewPresenter(IAddReviewView view, int gameId, IReviewService reviewService)
        {
            this.view = view;
            this.gameId = gameId;
            this.reviewService = reviewService;

            view.ReviewSubmitted += OnReviewSubmitted;
        }
        private void OnReviewSubmitted(Review review)
        {
            try
            {
                bool success = reviewService.AddReviewToGame(gameId, review);
                if (success)
                {
                    view.ShowMessage("Отзыв успешно добавлен!", "Успех");
                }
                else
                {
                    view.ShowMessage("Не удалось добавить отзыв.", "Ошибка");
                }
            }
            catch (Exception ex)
            {
                view.ShowMessage($"Ошибка при добавлении отзыва: {ex.Message}", "Ошибка");
            }
        }
        public void SubmitReview(string username, float rating, string reviewText)
        {
            if (rating < 1.0f || rating > 5.0f)
            {
                view.SetFormState(false, "Пожалуйста, введите рейтинг от 1.0 до 5.0 (например: 4.5)");
                return;
            }

            if (string.IsNullOrWhiteSpace(reviewText))
            {
                view.SetFormState(false, "Введите ваши впечатления.");
                return;
            }

            newReview = new Review
            {
                Username = string.IsNullOrWhiteSpace(username) ? "Аноним" : username,
                Rating = rating,
                ReviewText = reviewText
            };

            view.SetFormState(true, string.Empty);
            view.ReviewSubmitted?.Invoke(newReview);
        }
    }
}
