namespace Shared
{
    public interface IFormMainMenu
    {
        event EventHandler RepositoryChangeRequested;
        event EventHandler LoadGamesRequested;
        event EventHandler GameAdditionRequested;
        event EventHandler GameDeletionRequested;
        event EventHandler GameUpdateRequested;

        string SearchField { get; set; }
        string SearchText { get; set; }
        string SearchOption { get; set; }
    }
}
