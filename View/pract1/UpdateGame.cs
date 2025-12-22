using BusinessLogic;
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
            foreach (Entities.EnumPlatforms platform in Enum.GetValues(typeof(Entities.EnumPlatforms)))
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

        private Entities.Game gameToEdit;
        private string currentIcon;
        private List<string> screenshots = new List<string>();

        /// <summary>
        /// Загружает данные игры по указанному ID. Если игра не найдена, показывает сообщение об ошибке и закрывает форму.
        /// </summary>
        /// <param name="gameId">ID игры для загрузки.</param>
        private void LoadGame(int gameId)
        {
            gameToEdit = Logic.GetGameById(gameId);
            if (gameToEdit == null)
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
            if (gameToEdit == null) return;

            TB_GameName.Text = gameToEdit.Name;
            TB_Developer.Text = gameToEdit.Developer;
            TB_YearOfRelease.Text = gameToEdit.YearOfRelease?.ToString() ?? "";
            RTB_Description.Text = gameToEdit.Description;

            // Иконка
            currentIcon = gameToEdit.Icon ?? "";
            TB_IconPath.Text = string.IsNullOrEmpty(currentIcon) ? "Нет иконки" : currentIcon;
            if (!string.IsNullOrEmpty(currentIcon))
            {
                PIC_Game.Image = ImageLoader.GetImageFromFile(currentIcon);
            }
            else
            {
                PIC_Game.Image = Properties.Resources.No_image;
            }
            // Скриншоты
            screenshots = gameToEdit.Screenshots?.ToList() ?? new List<string>();
            TB_ScreenshotsPath.Text = $"{screenshots.Count} скриншотов";

            // Платформы
            for (int i = 0; i < CHKLTB_Platform.Items.Count; i++)
            {
                var platform = (Entities.EnumPlatforms)i;
                CHKLTB_Platform.SetItemChecked(i, gameToEdit.Platforms?.Contains(platform) == true);
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
                currentIcon = Path.GetFileNameWithoutExtension(dialog.FileName);
                TB_IconPath.Text = dialog.FileName;
                try
                {
                    PIC_Game.Image = Image.FromFile(dialog.FileName);
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
                screenshots.Clear();
                foreach (string file in dialog.FileNames)
                {
                    screenshots.Add(Path.GetFileNameWithoutExtension(file));
                }
                TB_ScreenshotsPath.Text = $"{screenshots.Count} файлов";
            }
        }

        /// <summary>
        /// Сбрасывает иконку игры к заглушке (No_image) и обновляет текстовое поле пути к иконке.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        private void BTN_Reset_Click(object sender, EventArgs e)
        {
            PIC_Game.Image = View.Properties.Resources.No_image;
            TB_IconPath.Text = "";
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
            gameToEdit.Name = TB_GameName.Text.Trim();
            gameToEdit.Developer = TB_Developer.Text.Trim();
            gameToEdit.Description = RTB_Description.Text.Trim();
            gameToEdit.Icon = Path.GetFileName(TB_IconPath.Text);
            gameToEdit.Screenshots = new List<string>(screenshots.Select(f => Path.GetFileName(f)).ToList());

            if (int.TryParse(TB_YearOfRelease.Text, out int year) && year > 1925)
            {
                gameToEdit.YearOfRelease = year;
            }
            else
            {
                MessageBox.Show("Дата релиза не может быть меньше чем 1925.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Платформы
            gameToEdit.Platforms = new List<Entities.EnumPlatforms>();
            for (int i = 0; i < CHKLTB_Platform.Items.Count; i++)
            {
                if (CHKLTB_Platform.GetItemChecked(i))
                {
                    gameToEdit.Platforms.Add((Entities.EnumPlatforms)i);
                }
            }

            bool success = Logic.UpdateGame(gameToEdit);

            if (success)
            {
                MessageBox.Show($"Игра \"{gameToEdit.Name}\" успешно обновлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
