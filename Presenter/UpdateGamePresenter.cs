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
    public class UpdateGamePresenter
    {
        private readonly IUpdateGameView view;
        private readonly IGameService gameService;
        private Game gameToEdit;

        public UpdateGamePresenter(IUpdateGameView view, int gameId, IGameService gameService)
        {
            this.view = view;
            this.gameService = gameService;

            LoadGame(gameId);
            view.GameUpdated += OnGameUpdated;
        }
        private void LoadGame(int gameId)
        {
            gameToEdit = gameService.GetGameById(gameId);
            if (gameToEdit != null)
            {
                view.LoadGame(gameToEdit);
            }
            else
            {
                view.ShowMessage("Игра не найдена.", "Ошибка");
            }
        }
        private void OnGameUpdated(Game updatedGame)
        {
            try
            {
                bool success = gameService.UpdateGame(updatedGame);
                if (success)
                {
                    view.ShowMessage($"Игра \"{updatedGame.Name}\" успешно обновлена!", "Успех");
                }
                else
                {
                    view.ShowMessage("Ошибка при обновлении игры.", "Ошибка");
                }
            }
            catch (Exception ex)
            {
                view.ShowMessage($"Ошибка при обновлении игры: {ex.Message}", "Ошибка");
            }
        }
        public void UpdateGame(string name, string developer, string description,
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

            gameToEdit.Name = name;
            gameToEdit.Developer = developer;
            gameToEdit.Description = description;
            gameToEdit.Icon = icon;
            gameToEdit.Screenshots = screenshots;
            gameToEdit.Platforms = platforms;
            gameToEdit.YearOfRelease = yearOfRelease;
            gameToEdit.Rating = rating;

            view.SetFormState(true, string.Empty);
            view.GameUpdated?.Invoke(gameToEdit);
        }
    }
}
