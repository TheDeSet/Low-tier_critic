using Entities;
using Shared;
using View;

namespace pract1
{
    public partial class MainMenu : Form, IFormMainMenu
    {
        public MainMenu()
        {
            InitializeComponent();
            CMB_Filter.SelectedIndex = 0;
            CMB_Sort.SelectedIndex = 0;
            /*var result = MessageBox.Show(
                "Да - Использовать EntityFrameWork \n\n Нет - Использовать Dapper",
                "Выбор режима",
                MessageBoxButtons.YesNo
            );

            bool useEF = result == DialogResult.Yes;

            IKernel kernel = new StandardKernel(new NinjectConfigModule(useEF));

            gameService = kernel.Get<IGameService>();
            reviewService = kernel.Get<IReviewService>();


            // Загружаем игры
            LoadGames();

            CMB_Filter.SelectedIndex = 0;
            CMB_Sort.SelectedIndex = 0;
            CMB_Sort.SelectedIndexChanged += (s, e) => LoadGames();
            TB_Search.TextChanged += (s, e) => LoadGames();
            CMB_Filter.SelectedIndexChanged += (s, e) => LoadGames();
            BTN_Reset.Click += (s, e) =>
            {
                CMB_Filter.SelectedIndex = 0;
                TB_Search.Text = "";
                CMB_Sort.SelectedIndex = 0;
            };
            BTN_Menu.Click += (s, e) =>
            {
                PNL_Menu.Visible = !PNL_Menu.Visible;
            };
            BTN_Delete.Click += OnDeleteButtonClick;
            BTN_Add.Click += BTN_Add_Click;*/
        }
        public event Action AddGameRequested;
        public event Action DeleteGameRequested;
        public event Action UpdateGameRequested;
        public event Action SearchRequested;
        public event Action SortRequested;
        public event Action ResetRequested;

        public int? GetSelectedGameId()
        {
            foreach (Control ctrl in FLP_GamesView?.Controls)
            {
                if (ctrl is ShowGameView tile && tile.IsSelected)
                {
                    return tile.GameData?.ID;
                }
            }
            return null;
        }
        public void LoadGames(List<Game> games)
        {
            FLP_GamesView?.Controls.Clear();

            foreach (var game in games)
            {
                var tile = new ShowGameView();
                tile.SetGame(game);
                tile.GameSelected += (s, gameId) => OnGameSelected(gameId);
                tile.GameOpened += (s, gameId) => GameOpened?.Invoke(gameId);
                FLP_GamesView.Controls.Add(tile);
            }
        }
        public void ShowMessage(string message, string title)
        {
            MessageBox.Show(message, title);
        }
        public void ShowConfirmation(string message, string title, Action onConfirm, Action onCancel)
        {
            if (MessageBox.Show(message, title, MessageBoxButtons.YesNo) == DialogResult.Yes)
                onConfirm?.Invoke();
            else
                onCancel?.Invoke();
        }
        private void OnGameSelected(int gameId)
        {
            foreach (Control ctrl in FLP_GamesView.Controls)
            {
                if (ctrl is ShowGameView tile)
                {
                    tile.SetSelected(tile.GameData?.ID == gameId);
                }
            }
        }
        /// <summary>
        /// Загружает список игр с учетом текущих параметров фильтрации, поиска и сортировки из интерфейса. Очищает контейнер плиток и 
        /// добавляет новую плитку для каждой игры из отфильтрованного списка. Обрабатывает событие обновления данных игры.
        /// </summary>
        /*private void LoadGames()
        {
            if (FLP_GamesView == null) return;
            FLP_GamesView.Controls.Clear();

            string searchField = CMB_Filter.SelectedItem?.ToString() ?? "искать по всему";
            string searchText = TB_Search.Text?.Trim() ?? "";
            string sortOption = CMB_Sort.SelectedItem?.ToString() ?? "без сортировки";
            var filteredGames = gameService.GetFilteredGames(
                searchField,
                searchText,
                sortOption
            );

            foreach (var game in filteredGames)
            {
                ShowGameView tile = new ShowGameView();
                tile.SetGame(game);
                tile.OpenRequested += (s, gameId) =>
                {
                    var detailsForm = new FullGameInformation(gameId, gameService, reviewService);
                    detailsForm.FormClosed += (fs, fe) => LoadGames();
                    detailsForm.ShowDialog();
                };
                tile.GameUpdated += (s, e) => LoadGames();
                FLP_GamesView.Controls.Add(tile);
            }
        }*/

        /// <summary>
        /// Обрабатывает событие удаления игры. Находит выделенную плитку, запрашивает подтверждение удаления, удаляет игру через логику 
        /// и обновляет список плиток. Отображает сообщения об успешном удалении или ошибках.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        /*private void OnDeleteButtonClick(object sender, EventArgs e)
        {
            ShowGameView? selectedTile = null;
            foreach (Control ctrl in FLP_GamesView.Controls)
            {
                if (ctrl is ShowGameView tile && tile.IsSelected)
                {
                    selectedTile = tile;
                    break;
                }
            }

            if (selectedTile == null)
            {
                MessageBox.Show("Сначала выберите игру для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Вы уверены, что хотите удалить игру \"{selectedTile.GameData.Name}\"?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {

                bool success = gameService.DeleteGame(selectedTile.GameData.ID);

                if (success)
                {
                    MessageBox.Show("Успешно удалено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadGames();
                }
                else
                {
                    MessageBox.Show("Ошибка при удалении игры.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Элемент остаётся без изменений", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }*/

        /// <summary>
        /// Обрабатывает событие добавления новой игры. Открывает форму AddNewGame для ввода данных новой игры и обновляет список игр после закрытия формы.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        /*private void BTN_Add_Click(object? sender, EventArgs e)
        {
            AddNewGame addNewGame = new(gameService);
            if (addNewGame.ShowDialog() == DialogResult.OK)
            {
                LoadGames();
            }
            LoadGames();
        }*/

        /// <summary>
        /// Обрабатывает событие обновления данных игры. Находит выделенную плитку, открывает форму UpdateGame для редактирования данных игры 
        /// и обновляет список плиток после успешного изменения.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        /*private void BTN_Update_Click(object sender, EventArgs e)
        {
            ShowGameView selectedTile = null;
            foreach (Control ctrl in FLP_GamesView.Controls)
            {
                if (ctrl is ShowGameView tile && tile.IsSelected)
                {
                    selectedTile = tile;
                    break;
                }
            }

            if (selectedTile == null)
            {
                MessageBox.Show("Сначала выберите игру для изменения.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var updForm = new UpdateGame(selectedTile.GameData.ID, gameService);
            if (updForm.ShowDialog() == DialogResult.OK)
            {
                LoadGames();
            }
        }*/
    }
}
