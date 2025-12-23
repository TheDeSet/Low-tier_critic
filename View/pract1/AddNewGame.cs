using BusinessLogic;
using BusinessLogic.Services;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View
{
    public partial class AddNewGame : Form, IAddGameView
    {
        private string currentIcon;
        private List<string> screenshots = new List<string>();
        public AddNewGame()
        {

            InitializeComponent();

            BTN_Add.Click += (s, e) => AddGameRequested?.Invoke(this, EventArgs.Empty);
            BTN_Reset.Click += (s, e) => ResetRequested?.Invoke(this, EventArgs.Empty);

            // Заполнение CHKLTB_Platform
            foreach (Entities.EnumPlatforms platform in Enum.GetValues(typeof(Entities.EnumPlatforms)))
            {
                CHKLTB_Platform.Items.Add(platform);
            }
            BTN_AddIconImage.Click += BTN_AddIconImage_Click;
            BTN_AddScreenshotImage.Click += BTN_AddScreenshotImage_Click;
            //BTN_Reset.Click += BTN_Reset_Click;
            //BTN_Add.Click += BTN_Add_Click;
            PIC_Game.Image = View.Properties.Resources.No_image; // заглушка
        }
        public string GameName => TB_GameName.Text;
        public string Developer => TB_Developer.Text;
        public string Description => RTB_Description.Text;
        public string YearOfRelease => TB_YearOfRelease.Text;
        public string Icon => TB_IconPath.Text;

        public List<int> SelectedPlatforms =>
            CHKLTB_Platform.CheckedIndices.Cast<int>().ToList();

        public List<string> Screenshots { get; } = new();

        public event EventHandler? AddGameRequested;
        public event EventHandler? ResetRequested;

        public void ShowMessage(string text, string caption) =>
            MessageBox.Show(text, caption);

        public void CloseView() => Close();

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
                currentIcon = Path.GetFileNameWithoutExtension(dialog.FileName);
                TB_IconPath.Text = dialog.FileName;
                try
                {
                    // Получаем имя файла
                    string sourceFile = dialog.FileName;
                    string fileName = Path.GetFileName(sourceFile);

                    // Определяем путь назначения
                    string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictures", fileName);

                    // Копируем файл
                    File.Copy(sourceFile, destPath, overwrite: true);

                    // Сохраняем только имя файла
                    currentIcon = fileName;
                    TB_IconPath.Text = fileName;

                    // Отображаем
                    PIC_Game.Image = Image.FromFile(destPath);
                }
                catch (Exception ex)
                {
                    PIC_Game.Image = Properties.Resources.No_image;
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                screenshots.Clear();
                foreach (string file in dialog.FileNames)
                {
                    try
                    {
                        string fileName = Path.GetFileName(file);
                        string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictures", fileName);

                        File.Copy(file, destPath, overwrite: true);
                        screenshots.Add(fileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не удалось загрузить: {file}\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                TB_ScreenshotsPath.Text = $"{screenshots.Count} файлов выбрано";
            }
        }

        /// <summary>
        /// Сбрасывает текущую иконку игры к заглушке (No_image) и очищает текстовое поле пути к иконке.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        private void BTN_Reset_Click(object sender, EventArgs e)
        {
            PIC_Game.Image = View.Properties.Resources.No_image;
            TB_IconPath.Text = "";
        }

        /// <summary>
        /// Обрабатывает событие нажатия кнопки добавления игры. Проверяет обязательные поля, создает новую игру с введенными данными, 
        /// добавляет её через Logic.AddGame, показывает сообщение об успехе и сбрасывает форму.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        /*private void BTN_Add_Click(object sender, EventArgs e)
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
            var newGame = new Entities.Game
            {
                Name = TB_GameName.Text.Trim(),
                Developer = TB_Developer.Text.Trim(),
                Description = RTB_Description.Text.Trim(),
                Icon = Path.GetFileName(TB_IconPath.Text),
                Screenshots = new List<string>(screenshots.Select(f => Path.GetFileName(f)).ToList()),
                Platforms = new List<Entities.EnumPlatforms>()
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
                if (Enum.IsDefined(typeof(Entities.EnumPlatforms), index))
                {
                    newGame.Platforms.Add((Entities.EnumPlatforms)index);
                }
            }
            
            gameService.AddGame(newGame);

            MessageBox.Show($"Игра \"{newGame.Name}\" успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);         
            ResetForm();
            this.Close();
        }*/

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

            PIC_Game.Image = View.Properties.Resources.No_image;
            screenshots.Clear();
        }
    }
}
