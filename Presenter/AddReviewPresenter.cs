using BusinessLogic.Services;
using Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace Presenter
{
    public class AddReviewPresenter
    {
        private readonly IAddReviewView view;
        private readonly IReviewService reviewService;
        private readonly int gameId;
        public AddReviewPresenter(IAddReviewView view, int gameId, IReviewService reviewService)
        {
            this.view = view;
            this.gameId = gameId;
            this.reviewService = reviewService;

            view.AddReviewRequested += OnAddReview;
        }
        private void OnAddReview(object? sender, EventArgs e)
        {
            if (!float.TryParse(view.RatingText, out float rating) ||
                rating < 1 || rating > 5)
            {
                view.ShowMessage("Рейтинг 1–5", "Ошибка");
                return;
            }

            if (string.IsNullOrWhiteSpace(view.ReviewText))
            {
                view.ShowMessage("Введите текст отзыва", "Ошибка");
                return;
            }

            var review = new Review
            {
                Username = view.Username,
                Rating = rating,
                ReviewText = view.ReviewText
            };

            if (reviewService.AddReviewToGame(gameId, review))
            {
                view.ShowMessage("Отзыв добавлен", "Успех");
                view.CloseView();
            }
            else
            {
                view.ShowMessage("Ошибка добавления", "Ошибка");
            }
        }
    }
}
