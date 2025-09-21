using Lab_3._1;

namespace pract1
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            LoadGames();
            CMB_Filter.SelectedIndex = 0;
            CMB_Sort.SelectedIndex = 0;
            CMB_GamesFromSeries.SelectedIndex = 0;
            CMB_Sort.SelectedIndexChanged += (s, e) => LoadGames();
            TB_Search.TextChanged += (s, e) => LoadGames();
            CMB_Filter.SelectedIndexChanged += (s, e) => LoadGames();
            CMB_GamesFromSeries.SelectedIndexChanged += (s, e) => LoadGames();
            BTN_Reset.Click += (s, e) =>
            {
                CMB_Filter.SelectedIndex = 0;
                TB_Search.Text = "";
                CMB_Sort.SelectedIndex = 0;
                CMB_GamesFromSeries.SelectedIndex = 0;
            };
            BTN_Menu.Click += (s, e) =>
            {
                // Переключаем видимость
                PNL_Menu.Visible = !PNL_Menu.Visible;
            };
            BTN_Delete.Click += OnDeleteButtonClick;
        }

        private void LoadGames()
        {
            if (FLP_GamesView == null) return;
            FLP_GamesView.Controls.Clear();

            // Получаем параметры из UI
            string searchField = CMB_Filter.SelectedItem?.ToString() ?? "искать по всему";
            string searchText = TB_Search.Text?.Trim() ?? "";
            string sortOption = CMB_Sort.SelectedItem?.ToString() ?? "без сортировки";
            string seriesFilter = CMB_GamesFromSeries.SelectedItem?.ToString() ?? "все серии";

            var filteredGames = Logic.GetFilteredGames(
                searchField,
                searchText,
                sortOption,
                seriesFilter == "все серии" ? null : seriesFilter
            );

            // Отображаем результат
            foreach (var game in filteredGames)
            {
                var tile = new ShowGameView();
                tile.SetGame(game);
                FLP_GamesView.Controls.Add(tile);
            }
        }
        private void OnDeleteButtonClick(object sender, EventArgs e)
        {
            // Находим выделенную плитку
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
                MessageBox.Show("Сначала выберите игру для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Подтверждение
            var result = MessageBox.Show(
                $"Вы уверены, что хотите удалить игру \"{selectedTile.GameData.Name}\"?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                
                bool success = Logic.DeleteGame(selectedTile.GameData.ID);

                if (success)
                {
                    MessageBox.Show("Успешно удалено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadGames(); // Обновляем список плиток
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
        }
    }
}
