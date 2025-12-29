using Entities;

namespace Shared
{
    public interface IFormMainMenu : IFormGeneral
    {
        event EventHandler ShowGamesRequested;
        event EventHandler AddGameRequested;
        event EventHandler DeleteGameRequested;
        event EventHandler UpdateGameRequested;
        event EventHandler SearchRequested;
        event EventHandler SortRequested;
        event EventHandler ResetRequested;
        event EventHandler ExitRequested;

        void DisplayGames(List<GameDTO> games);
        void SetSearchText(string searchText);
        void SetSortOption(string sortOption);
        void CloseView();
    }
}
