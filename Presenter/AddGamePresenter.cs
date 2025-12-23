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
        private Game newGame;

        public AddGamePresenter(IAddGameView view, IGameService gameService)
        {
            this.view = view;
            this.gameService = gameService;

            view.GameAdded += OnGameAdded;
        }
        private void OnGameAdded(Game game)
        {
            try
            {
                gameService.AddGame(game);
                view.ShowMessage($"Игра \"{game.Name}\" успешно добавлена!", "Успех");
            }
            catch (Exception ex)
            {
                view.ShowMessage($"Ошибка при добавлении игры: {ex.Message}", "Ошибка");
                return;
            }

            view.ResetForm();
        }

        public void AddGame(string name, string developer, string description,
            string icon, List<string> screenshots, List<EnumPlatforms> platforms,
            int? yearOfRelease, float? rating)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                view.SetFormState(false, "Введите название игры.");
                return;
            }

            if (string.IsNullOrWhiteSpace(developer))
            {
                view.SetFormState(false, "Введите разработчика.");
                return;
            }

            if (yearOfRelease.HasValue && yearOfRelease < 1925)
            {
                view.SetFormState(false, "Дата релиза не может быть меньше чем 1925.");
                return;
            }

            newGame = new Game
            {
                Name = name,
                Developer = developer,
                Description = description,
                Icon = icon,
                Screenshots = screenshots,
                Platforms = platforms,
                YearOfRelease = yearOfRelease,
                Rating = rating
            };

            view.SetFormState(true, string.Empty);
            view.GameAdded?.Invoke(newGame);
        }

    }
}
