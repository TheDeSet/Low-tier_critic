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
        /// <summary>
        /// Инициализирует форму отображения информации об игре. Подписывает события для управления миниатюрами и кнопкой создания отзыва,
        /// а также загружает данные игры по указанному ID.
        /// </summary>
        /// <param name="gameId">ID игры для отображения.</param>
        public FullGameInformation(int gameId)
        {
            InitializeComponent();
            HSCB_Thumbnails.Scroll += (s, e) => UpdateThumbnailsPosition();
            BTN_MakeReview.Click += (s, e) => BTN_MakeReview_Click(gameId);
            LoadGameData(gameId);
        }

        private List<Image> allScreenshots = new List<Image>(); // Icon + Screenshots
        private List<PictureBox> thumbnailBoxes = new List<PictureBox>();
        private int thumbnailWidth = 80;  // ширина миниатюры
        private int thumbnailHeight = 60;
        private int spacing = 5;

        /// <summary>
        /// Обновляет позицию миниатюр скриншотов при скролле горизонтального ползунка.
        /// </summary>
        private void UpdateThumbnailsPosition()
        {
            int offset = HSCB_Thumbnails.Value;
            foreach (var thumb in thumbnailBoxes)
            {
                int index = (int)thumb.Tag;
                thumb.Left = index * (thumbnailWidth + spacing) - offset;
            }
        }

        /// <summary>
        /// Выделяет выбранную миниатюру скриншота, изменяя стиль рамки.
        /// </summary>
        /// <param name="selectedIndex">Индекс выбранной миниатюры.</param>
        private void HighlightSelectedThumbnail(int selectedIndex)
        {
            foreach (var thumb in thumbnailBoxes)
            {
                thumb.BorderStyle = (int)thumb.Tag == selectedIndex
                    ? BorderStyle.Fixed3D  // подсвеченная рамка
                    : BorderStyle.FixedSingle; // обычная рамка
            }
        }

        /// <summary>
        /// Загружает данные игры по указанному ID и заполняет элементы управления формой: название, разработчика, год выпуска, рейтинг, описание, 
        /// платформы, отзывы и миниатюры скриншотов. Обрабатывает случаи отсутствия данных.
        /// </summary>
        /// <param name="gameId">ID игры для загрузки.</param>
        private void LoadGameData(int gameId)
        {
            var game = Logic.GetGameById(gameId);

            if (game == null)
            {
                MessageBox.Show("Игра не найдена.");
                this.Close();
                return;
            }

            LB_GameName.Text = game.Name;
            LB_Developer.Text = $"{game.Developer}";
            LB_YearOfRelease.Text = game.YearOfRelease.HasValue ? $"{game.YearOfRelease}" : "не указан";
            LB_Rating.Text = game.Rating.HasValue ? $"{game.Rating:F1}" : "не указан";
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

            FLP_Reviews.Controls.Clear();

            if (game.Reviews != null && game.Reviews.Count > 0)
            {
                foreach (var review in game.Reviews)
                {
                    var reviewControl = new ShowReviewsView();
                    reviewControl.SetReview(review);
                    reviewControl.Dock = DockStyle.Top;
                    reviewControl.Margin = new Padding(0, 0, 0, 5);

                    FLP_Reviews.Controls.Add(reviewControl);
                }
            }
            else
            {
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

            PNL_Thumbnails.Controls.Clear();
            thumbnailBoxes.Clear();
            allScreenshots.Clear();

            if (game.Screenshots != null)
                allScreenshots.AddRange(game.Screenshots);

            if (allScreenshots.Count > 0)
            {
                PIC_GameImage.Image = allScreenshots[0];

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
                        Tag = i 
                    };

                    thumb.Click += (s, e) =>
                    {
                        var index = (int)((PictureBox)s).Tag;
                        PIC_GameImage.Image = allScreenshots[index];
                        
                        HighlightSelectedThumbnail(index);
                    };

                    PNL_Thumbnails.Controls.Add(thumb);
                    thumbnailBoxes.Add(thumb);
                    totalWidth += thumbnailWidth + spacing;
                }

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

        /// <summary>
        /// Обрабатывает событие нажатия кнопки "Создать отзыв". Открывает форму AddReview для добавления отзыва к текущей игре и обновляет данные 
        /// после успешного добавления.
        /// </summary>
        /// <param name="gameId">ID игры, к которой добавляется отзыв.</param>
        private void BTN_MakeReview_Click(int gameId)
        {
            var game = Logic.GetGameById(gameId);

            var addReviewForm = new AddReview(game.ID);

            if (addReviewForm.ShowDialog() == DialogResult.OK)
            {
                LoadGameData(game.ID);
            }
        }
    }
}
