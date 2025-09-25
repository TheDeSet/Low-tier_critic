using Lab_3._1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View
{
    public partial class UpdateGame : Form
    {
        /// <summary>
        /// Инициализирует форму редактирования игры. Заполняет список платформ из EnumPlatforms, загружает данные игры по указанному ID 
        /// и настраивает обработчики событий для кнопок выбора изображений, сброса и сохранения изменений.
        /// </summary>
        /// <param name="gameId">ID игры для редактирования.</param>
        public UpdateGame(int gameId)
        {
            InitializeComponent();
            foreach (EnumPlatforms platform in Enum.GetValues(typeof(EnumPlatforms)))
            {
                CHKLTB_Platform.Items.Add(platform);
            }
            LoadGame(gameId);
            PopulateFields();
            BTN_AddIconImage.Click += BTN_AddIconImage_Click;
            BTN_AddScreenshotImage.Click += BTN_AddScreenshotImage_Click;
            BTN_Reset.Click += BTN_Reset_Click;
            BTN_Add.Click += BTN_Add_Click;
        }

        private Game _gameToEdit;
        private Image _currentIcon;
        private List<Image> _screenshots = new List<Image>();

        /// <summary>
        /// Загружает данные игры по указанному ID. Если игра не найдена, показывает сообщение об ошибке и закрывает форму.
        /// </summary>
        /// <param name="gameId">ID игры для загрузки.</param>
        private void LoadGame(int gameId)
        {
            _gameToEdit = Logic.GetGameById(gameId);
            if (_gameToEdit == null)
            {
                MessageBox.Show("Игра не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
        }
        /// <summary>
        /// Заполняет элементы управления формы данными загруженной игры: название, разработчик, год выпуска, описание, иконка, 
        /// скриншоты и платформы. Устанавливает текущие значения в интерфейсе.
        /// </summary>
        private void PopulateFields()
        {
            if (_gameToEdit == null) return;

            TB_GameName.Text = _gameToEdit.Name;
            TB_Developer.Text = _gameToEdit.Developer;
            TB_YearOfRelease.Text = _gameToEdit.YearOfRelease?.ToString() ?? "";
            RTB_Description.Text = _gameToEdit.Description;

            // Иконка
            _currentIcon = _gameToEdit.Icon ?? View.Properties.Resources.No_image;
            PIC_Game.Image = _currentIcon;
            TB_IconPath.Text = "Иконка загружена из данных";

            // Скриншоты
            _screenshots = _gameToEdit.Screenshots?.ToList() ?? new List<Image>();
            TB_ScreenshotsPath.Text = $"{_screenshots.Count} скриншотов";

            // Платформы
            for (int i = 0; i < CHKLTB_Platform.Items.Count; i++)
            {
                var platform = (EnumPlatforms)i;
                CHKLTB_Platform.SetItemChecked(i, _gameToEdit.Platforms?.Contains(platform) == true);
            }
        }

        /// <summary>
        /// Обрабатывает событие нажатия кнопки выбора иконки. Открывает диалог OpenFileDialog для выбора файла иконки, 
        /// загружает изображение и обновляет PictureBox и текстовое поле пути.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        private void BTN_AddIconImage_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Title = "Выберите иконку",
                Filter = "Изображения|*.png;*.jpg;*.jpeg;*.bmp|Все файлы|*.*"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _currentIcon = Image.FromFile(dialog.FileName);
                    PIC_Game.Image = _currentIcon;
                    TB_IconPath.Text = dialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Обрабатывает событие нажатия кнопки выбора скриншотов. Открывает диалог OpenFileDialog с множественным выбором файлов, 
        /// загружает выбранные изображения в список и обновляет текстовое поле с количеством файлов.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        private void BTN_AddScreenshotImage_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Title = "Выберите скриншоты",
                Filter = "Изображения|*.png;*.jpg;*.jpeg;*.bmp|Все файлы|*.*",
                Multiselect = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _screenshots.Clear();
                foreach (string file in dialog.FileNames)
                {
                    try
                    {
                        _screenshots.Add(Image.FromFile(file));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не удалось загрузить: {file}\n{ex.Message}", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                TB_ScreenshotsPath.Text = $"{_screenshots.Count} файлов";
            }
        }

        /// <summary>
        /// Сбрасывает иконку игры к заглушке (No_image) и обновляет текстовое поле пути к иконке.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        private void BTN_Reset_Click(object sender, EventArgs e)
        {
            _currentIcon = View.Properties.Resources.No_image;
            PIC_Game.Image = _currentIcon;
            TB_IconPath.Text = "Иконка сброшена";
        }

        /// <summary>
        /// Обрабатывает событие сохранения изменений. Валидирует обязательные поля, обновляет данные игры, проверяет корректность года выпуска, 
        /// обновляет платформы и сохраняет изменения через Logic.UpdateGame. Отображает сообщение об успехе или ошибке.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        private void BTN_Add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TB_GameName.Text))
            {
                MessageBox.Show("Введите название игры.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(TB_Developer.Text))
            {
                MessageBox.Show("Введите разработчика.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _gameToEdit.Name = TB_GameName.Text.Trim();
            _gameToEdit.Developer = TB_Developer.Text.Trim();
            _gameToEdit.Description = RTB_Description.Text.Trim();
            _gameToEdit.Icon = _currentIcon;
            _gameToEdit.Screenshots = new List<Image>(_screenshots);

            if (int.TryParse(TB_YearOfRelease.Text, out int year) && year > 1925)
            {
                _gameToEdit.YearOfRelease = year;
            }
            else
            {
                MessageBox.Show("Дата релиза не может быть меньше чем 1925.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Платформы
            _gameToEdit.Platforms = new List<EnumPlatforms>();
            for (int i = 0; i < CHKLTB_Platform.Items.Count; i++)
            {
                if (CHKLTB_Platform.GetItemChecked(i))
                {
                    _gameToEdit.Platforms.Add((EnumPlatforms)i);
                }
            }

            bool success = Logic.UpdateGame(_gameToEdit);

            if (success)
            {
                MessageBox.Show($"Игра \"{_gameToEdit.Name}\" успешно обновлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // для вызывающей формы
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при обновлении игры.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
