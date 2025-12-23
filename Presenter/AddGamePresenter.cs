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
    public class AddGamePresenter
    {
        private readonly IAddGameView view;
        private readonly IGameService gameService;

        public AddGamePresenter(IAddGameView view, IGameService gameService)
        {
            this.view = view;
            this.gameService = gameService;

            view.AddGameRequested += OnAddGame;
            view.ResetRequested += (s, e) => view.CloseView();
        }
        private void OnAddGame(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(view.GameName))
            {
                view.ShowMessage("Введите название игры", "Ошибка");
                return;
            }

            if (!int.TryParse(view.YearOfRelease, out int year) || year < 1925)
            {
                view.ShowMessage("Некорректный год выпуска", "Ошибка");
                return;
            }

            var game = new Game
            {
                Name = view.GameName.Trim(),
                Developer = view.Developer.Trim(),
                Description = view.Description.Trim(),
                YearOfRelease = year,
                Icon = view.Icon,
                Screenshots = view.Screenshots,
                Platforms = view.SelectedPlatforms
                    .Select(i => (EnumPlatforms)i)
                    .ToList()
            };

            gameService.AddGame(game);
            view.ShowMessage("Игра добавлена", "Успех");
            view.CloseView();
        }
    }
}
