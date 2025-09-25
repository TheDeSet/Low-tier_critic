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
    public partial class AddNewGame : Form
    {
        public AddNewGame()
        {
            InitializeComponent();
            // Заполнение CHKLTB_Platform
            foreach (EnumPlatforms platform in Enum.GetValues(typeof(EnumPlatforms)))
            {
                CHKLTB_Platform.Items.Add(platform);
            }
            BTN_AddIconImage.Click += BTN_AddIconImage_Click;
            BTN_AddScreenshotImage.Click += BTN_AddScreenshotImage_Click;
            BTN_Reset.Click += BTN_Reset_Click;
            BTN_Add.Click += BTN_Add_Click;
            PIC_Game.Image = View.Properties.Resources.No_image; // заглушка
            _currentIcon = View.Properties.Resources.No_image;
        }

        private Image _currentIcon;
        private List<Image> _screenshots = new List<Image>();

        /// <summary>
        /// Обрабатывает событие нажатия кнопки выбора иконки игры. Открывает диалог OpenFileDialog для выбора файла иконки, 
        /// загружает изображение и обновляет PictureBox и текстовое поле пути.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        private void BTN_AddIconImage_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Title = "Выберите иконку игры",
                Filter = "Изображения|*.png;*.jpg;*.jpeg;*.bmp|Все файлы|*.*",
                Multiselect = false
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
                    MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Обрабатывает событие нажатия кнопки выбора скриншотов. Открывает диалог OpenFileDialog с множественным выбором файлов, 
        /// загружает изображения в список и обновляет текстовое поле с количеством выбранных файлов.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        private void BTN_AddScreenshotImage_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Title = "Выберите скриншоты",
                Filter = "Изображения|*.png;*.jpg;*.jpeg;*.bmp|Все файлы|*.*",
                Multiselect = true // можно выбрать несколько!
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
                        MessageBox.Show($"Не удалось загрузить: {file}\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                TB_ScreenshotsPath.Text = $"{_screenshots.Count} файлов выбрано";
            }
        }

        /// <summary>
        /// Сбрасывает текущую иконку игры к заглушке (No_image) и очищает текстовое поле пути к иконке.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        private void BTN_Reset_Click(object sender, EventArgs e)
        {
            _currentIcon = View.Properties.Resources.No_image;
            PIC_Game.Image = _currentIcon;
            TB_IconPath.Text = "";
        }

        /// <summary>
        /// Обрабатывает событие нажатия кнопки добавления игры. Проверяет обязательные поля, создает новую игру с введенными данными, 
        /// добавляет её через Logic.AddGame, показывает сообщение об успехе и сбрасывает форму.
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
            var newGame = new Game
            {
                Name = TB_GameName.Text.Trim(),
                Developer = TB_Developer.Text.Trim(),
                Description = RTB_Description.Text.Trim(),
                Icon = _currentIcon,
                Screenshots = new List<Image>(_screenshots),
                Platforms = new List<EnumPlatforms>()
            };
            if (int.TryParse(TB_YearOfRelease.Text, out int year) && year >= 1925)
            {
                newGame.YearOfRelease = year;
            }
            else
            {
                MessageBox.Show("Дата релиза не может быть меньше чем 1925.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (int index in CHKLTB_Platform.CheckedIndices)
            {
                if (Enum.IsDefined(typeof(EnumPlatforms), index))
                {
                    newGame.Platforms.Add((EnumPlatforms)index);
                }
            }

            
            Logic.AddGame(newGame);

            MessageBox.Show($"Игра \"{newGame.Name}\" успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);         
            ResetForm();
            this.Close();
        }

        /// <summary>
        /// Сбрасывает все поля формы в начальное состояние: очищает текстовые поля, сбрасывает выбранные платформы и изображения.
        /// </summary>
        private void ResetForm()
        {
            TB_GameName.Text = "";
            TB_Developer.Text = "";
            TB_YearOfRelease.Text = "";
            RTB_Description.Text = "";
            TB_IconPath.Text = "";
            TB_ScreenshotsPath.Text = "";
            CHKLTB_Platform.ClearSelected();

            _currentIcon = View.Properties.Resources.No_image;
            PIC_Game.Image = _currentIcon;
            _screenshots.Clear();
        }
    }
}
