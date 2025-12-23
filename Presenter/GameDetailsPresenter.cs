using BusinessLogic.Services;
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
        private readonly int gameId;
        public GameDetailsPresenter(IGameDetailsView view, int gameId, IGameService gameService)
        {
            this.view = view;
            this.gameId = gameId;
            this.gameService = gameService;

            view.AddReviewRequested += (s, e) => view.ShowMessage("Открытие формы отзыва", "Info");

            LoadGameData();
        }
        private void LoadGameData()
        {
            var game = gameService.GetGameById(gameId);
            if (game == null)
            {
                view.ShowMessage("Игра не найдена", "Ошибка");
                return;
            }

            view.ShowGame(game);
        }
    }
}
