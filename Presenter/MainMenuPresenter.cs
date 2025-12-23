using Shared;
using BusinessLogic;
using BusinessLogic.Services;
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
    public class MainMenuPresenter
    {
        private readonly IFormMainMenu view;
        private readonly IGameService gameService;
        private readonly IReviewService reviewService;
        
        private string searchField = "искать по всему";
        private string searchText = "";
        private string sortOption = "без сортировки";
        
        public MainMenuPresenter(IFormMainMenu view, IGameService gameService, IReviewService reviewService)
        {
            this.view = view;
            this.gameService = gameService;
            this.reviewService = reviewService;

            view.AddGameRequested += OnAddGame;
            view.DeleteGameRequested += OnDeleteGame;
            view.UpdateGameRequested += OnUpdateGame;
            view.SearchRequested += OnSearch;
            view.SortRequested += OnSort;
            view.ResetRequested += OnReset;

            LoadGames();
        }
        private void LoadGames()
        {
            var filteredGames = gameService.GetFilteredGames(
                searchField,
                searchText,
                sortOption
            );
            view.LoadGames(filteredGames);
        }
        private void OnAddGame()
        {
            var addGameView = Starter.CreateAddGameView();
            if (addGameView.ShowDialog() == DialogResult.OK)
            {
                LoadGames();
            }
        }
        private void OnDeleteGame(object? sender, EventArgs e)
        {
            var gameId = view.GetSelectedGameId();
            if (gameId == null)
            {
                view.ShowMessage("Сначала выберите игру для удаления.", "Внимание");
                return;
            }

            var game = gameService.GetGameById(gameId.Value);
            if (game == null)
            {
                view.ShowMessage("Игра не найдена.", "Ошибка");
                return;
            }

            view.ShowConfirmation(
                $"Вы уверены, что хотите удалить игру \"{game.Name}\"?",
                "Подтверждение удаления",
                onConfirm: () => {
                    bool success = gameService.DeleteGame(gameId.Value);
                    if (success)
                    {
                        view.ShowMessage("Успешно удалено", "Успех");
                        LoadGames();
                    }
                    else
                    {
                        view.ShowMessage("Ошибка при удалении игры.", "Ошибка");
                    }
                },
                onCancel: () => {
                    view.ShowMessage("Элемент остаётся без изменений", "Отмена");
                }
            );
        }
        private void OnDeleteGame()
        {
            ShowGameView selectedTile = null;
            foreach (Control ctrl in ((MainMenu)view).FLP_GamesView.Controls)
            {
                if (ctrl is ShowGameView tile && tile.IsSelected)
                {
                    selectedTile = tile;
                    break;
                }
            }

            if (selectedTile == null)
            {
                view.ShowMessage("Сначала выберите игру для удаления.", "Внимание");
                return;
            }

            view.ShowConfirmation(
                $"Вы уверены, что хотите удалить игру \"{selectedTile.GameData.Name}\"?",
                "Подтверждение удаления",
                onConfirm: () => {
                    bool success = gameService.DeleteGame(selectedTile.GameData.ID);
                    if (success)
                    {
                        view.ShowMessage("Успешно удалено", "Успех");
                        LoadGames();
                    }
                    else
                    {
                        view.ShowMessage("Ошибка при удалении игры.", "Ошибка");
                    }
                },
                onCancel: () => {
                    view.ShowMessage("Элемент остаётся без изменений", "Отмена");
                }
            );
        }
        private void OnUpdateGame()
        {
            var gameId = view.GetSelectedGameId();
            if (gameId == null)
            {
                view.ShowMessage("Сначала выберите игру для изменения.", "Внимание");
                return;
            }

            var updForm = Starter.CreateUpdateGameView(gameId.Value);
            if (updForm.ShowDialog() == DialogResult.OK)
            {
                LoadGames();
            }
        }
        private void OnSearch()
        {
            LoadGames();
        }
        private void OnSort()
        {
            LoadGames();
        }
        private void OnReset()
        {
            searchField = "искать по всему";
            searchText = "";
            sortOption = "без сортировки";
            LoadGames();
        }
    }
}
