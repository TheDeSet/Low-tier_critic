using Shared;
using BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private void OnAddGame(object sender, EventArgs e)
        {
            var addGameView = Starter.CreateAddGameView();
            if (addGameView.ShowDialog() == DialogResult.OK)
            {
                LoadGames();
            }
        }
        private void OnDeleteGame(object sender, EventArgs e)
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
        private void OnUpdateGame(object sender, EventArgs e)
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
        private void OnSearch(object sender, EventArgs e)
        {
            LoadGames();
        }
        private void OnSort(object sender, EventArgs e)
        {
            LoadGames();
        }
        private void OnReset(object sender, EventArgs e)
        {
            searchField = "искать по всему";
            searchText = "";
            sortOption = "без сортировки";
            LoadGames();
        }
    }
}
