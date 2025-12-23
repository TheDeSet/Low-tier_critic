using Entities;

namespace Shared
{
    public interface IFormMainMenu
    {
        event EventHandler ShowGamesRequested;
        event EventHandler AddGameRequested;
        event EventHandler ExitRequested;
        void CloseView();
    }
}
