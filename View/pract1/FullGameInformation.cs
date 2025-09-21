using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab_3._1;

namespace View
{
    public partial class FullGameInformation : Form
    {
        public FullGameInformation(int gameId)
        {
            InitializeComponent();
            HSCB_Thumbnails.Scroll += (s, e) => UpdateThumbnailsPosition();
            LoadGameData(gameId);
        }

        private List<Image> allScreenshots = new List<Image>(); // Icon + Screenshots
        private List<PictureBox> thumbnailBoxes = new List<PictureBox>();
        private int thumbnailWidth = 80;  // ширина миниатюры
        private int thumbnailHeight = 60;
        private int spacing = 5;

        private void UpdateThumbnailsPosition()
        {
            int offset = HSCB_Thumbnails.Value;
            foreach (var thumb in thumbnailBoxes)
            {
                int index = (int)thumb.Tag;
                thumb.Left = index * (thumbnailWidth + spacing) - offset;
            }
        }

        private void HighlightSelectedThumbnail(int selectedIndex)
        {
            foreach (var thumb in thumbnailBoxes)
            {
                thumb.BorderStyle = (int)thumb.Tag == selectedIndex
                    ? BorderStyle.Fixed3D  // подсвеченная рамка
                    : BorderStyle.FixedSingle; // обычная рамка
            }
        }

        private void LoadGameData(int gameId)
        {
            // Обращаемся к Logic для получения игры по ID
            var game = Logic.GetGameById(gameId); // ← Этот метод нужно реализовать в классе Logic!

            if (game == null)
            {
                MessageBox.Show("Игра не найдена.");
                this.Close();
                return;
            }

            // Заполняем данные
            LB_GameName.Text = game.Name;
            LB_Developer.Text = $"{game.Developer}";
            LB_YearOfRelease.Text = game.YearOfRelease.HasValue ? $"{game.YearOfRelease}" : "Год выпуска: не указан";
            LB_Rating.Text = game.Rating.HasValue ? $"{game.Rating:F1}" : "Рейтинг: не указан";
            RTB_Description.Text = game.Description ?? "Описание отсутствует.";

            PIC_GameImage.Image = game.Icon ?? View.Properties.Resources.No_image;

            // Платформы Игнат добавь label мне лень
            if (game.Platforms != null)
            {
                foreach (var platform in game.Platforms)
                {
                    LTB_Platforms.Items.Add(platform);
                }    
                    
            }

            // Рецензии
            // Очистим контейнер (на случай повторного открытия)
            FLP_Reviews.Controls.Clear();

            if (game.Reviews != null && game.Reviews.Count > 0)
            {
                foreach (var review in game.Reviews)
                {
                    var reviewControl = new ShowReviewsView();
                    reviewControl.SetReview(review); // Передаём данные в UserControl
                    reviewControl.Dock = DockStyle.Top;
                    reviewControl.Margin = new Padding(0, 0, 0, 5); // отступ снизу

                    FLP_Reviews.Controls.Add(reviewControl);
                }
            }
            else
            {
                // Если рецензий нет — показывает заглушку
                var noReviewsLabel = new Label
                {
                    Text = "Рецензии отсутствуют.",
                    Font = new Font("Segoe UI", 9, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Margin = new Padding(10)
                };
                FLP_Reviews.Controls.Add(noReviewsLabel);
            }

            // Скриншоты
            // Очистка
            PNL_Thumbnails.Controls.Clear();
            thumbnailBoxes.Clear();
            allScreenshots.Clear();

            // 1. Добавляем главную иконку
            if (game.Icon != null)
                allScreenshots.Add(game.Icon);

            // 2. Добавляем скриншоты
            if (game.Screenshots != null)
                allScreenshots.AddRange(game.Screenshots);

            // 3. Если есть хотя бы одно изображение — показываем первое как главное
            if (allScreenshots.Count > 0)
            {
                PIC_GameImage.Image = allScreenshots[0];

                // Создаём миниатюры
                int totalWidth = 0;
                for (int i = 0; i < allScreenshots.Count; i++)
                {
                    var thumb = new PictureBox
                    {
                        Size = new Size(thumbnailWidth, thumbnailHeight),
                        Location = new Point(totalWidth, spacing),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        BorderStyle = BorderStyle.FixedSingle,
                        Image = allScreenshots[i],
                        Tag = i // запоминаем индекс!
                    };

                    thumb.Click += (s, e) =>
                    {
                        var index = (int)((PictureBox)s).Tag;
                        PIC_GameImage.Image = allScreenshots[index];
                        // Можно добавить рамку для активного
                        HighlightSelectedThumbnail(index);
                    };

                    PNL_Thumbnails.Controls.Add(thumb);
                    thumbnailBoxes.Add(thumb);
                    totalWidth += thumbnailWidth + spacing;
                }

                // Настраиваем скроллбар
                PNL_Thumbnails.Width = 420; // фиксированная ширина видимой области
                PNL_Thumbnails.AutoScroll = false;

                if (totalWidth > PNL_Thumbnails.Width)
                {
                    HSCB_Thumbnails.Maximum = totalWidth - PNL_Thumbnails.Width;
                    HSCB_Thumbnails.Enabled = true;
                }
                else
                {
                    HSCB_Thumbnails.Value = 0;
                    HSCB_Thumbnails.Enabled = false;
                }

                UpdateThumbnailsPosition();
            }
            else
            {
                PIC_GameImage.Image = View.Properties.Resources.No_image;
                HSCB_Thumbnails.Enabled = false;
            }
        }
    }
}
