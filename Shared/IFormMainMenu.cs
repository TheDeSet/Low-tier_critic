using Entities;

namespace Shared
{
    public interface IFormMainMenu
    {
        event Action AddGameRequested;
        event Action DeleteGameRequested;
        event Action UpdateGameRequested;
        event Action SearchRequested;
        event Action SortRequested;
        event Action ResetRequested;

        int? GetSelectedGameId();
        void LoadGames(List<Game> games);
        void ShowMessage(string message, string title);
        void ShowConfirmation(string message, string title, Action onConfirm, Action onCancel);
    }
}
