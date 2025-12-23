using BusinessLogic.Services;
using Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter
{
    public class GameDetailsPresenter
    {
        private readonly IGameDetailsView view;
        private readonly IGameService gameService;
        private readonly IReviewService reviewService;
        private readonly int gameId;
        public GameDetailsPresenter(IGameDetailsView view, int gameId, IGameService gameService, IReviewService reviewService)
        {
            this.view = view;
            this.gameId = gameId;
            this.gameService = gameService;
            this.reviewService = reviewService;

            view.MakeReviewRequested += OnMakeReview;

            LoadGameData();
        }
        private void LoadGameData()
        {
            var game = gameService.GetGameById(gameId);
            if (game != null)
            {
                view.LoadGameData(game);
            }
            else
            {
                view.ShowMessage("Игра не найдена.", "Ошибка");
            }
        }
        private void OnMakeReview()
        {
            var addReviewView = Starter.CreateAddReviewView(gameId);
            if (addReviewView.ShowDialog() == DialogResult.OK)
            {
                LoadGameData();
                view.GameUpdated?.Invoke(gameId);
            }
        }
    }
}
