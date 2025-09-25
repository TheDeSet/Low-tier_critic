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
using View;

namespace pract1
{
    public partial class ShowGameView : UserControl
    {

        /// <summary>
        /// Инициализирует новый экземпляр класса ShowGameView и настраивает обработчики событий для кликов и двойных кликов.
        /// </summary>
        public ShowGameView()
        {
            InitializeComponent();
            LoadInformationOfGame();
        }

        /// <summary>
        /// Настраивает обработчики событий для элементов управления внутри плитки (клики и двойные клики).
        /// </summary>
        private void LoadInformationOfGame()
        {
            this.DoubleClick += ShowGameView_DoubleClick;
            PIC_Game.DoubleClick += ShowGameView_DoubleClick;
            LB_Game_Name.DoubleClick += ShowGameView_DoubleClick;
            LB_Rating_Game.DoubleClick += ShowGameView_DoubleClick;
            LB_Developer.DoubleClick += ShowGameView_DoubleClick;
            this.Click += ShowGameView_Click;
            PIC_Game.Click += ShowGameView_Click;
            LB_Game_Name.Click += ShowGameView_Click;
            LB_Rating_Game.Click += ShowGameView_Click;
            LB_Developer.Click += ShowGameView_Click;
        }

        public Game GameData { get; private set; }
        public bool IsSelected { get; private set; }

        /// <summary>
        /// Устанавливает данные игры в элементы управления плитки (название, разработчик, рейтинг, иконка).
        /// </summary>
        /// <param name="game">Объект игры для отображения.</param>
        public void SetGame(Game game) 
        {
            GameData = game; 
            if (game == null) return;

            LB_Game_Name.Text = game.Name ?? "Без названия";
            LB_Developer.Text = game.Developer ?? "Неизвестен";
            LB_Rating_Game.Text = game.Rating.HasValue
                ? $"Рейтинг: {game.Rating:F1}"
                : "Рейтинг: —";


            PIC_Game.Image = game.Icon ?? View.Properties.Resources.No_image; // Заглушка
            

        }
        /// <summary>
        /// Обновляет визуальное состояние плитки при выделении/снятии выделения (цвет фона и стиль рамки).
        /// </summary>
        /// <param name="selected">Флаг выделения (true - выделена, false - не выделена).</param>
        public void SetSelected(bool selected)
        {
            IsSelected = selected;
            this.BackColor = selected ? Color.LightBlue : Color.White; // визуальная подсветка
            this.BorderStyle = selected ? BorderStyle.FixedSingle : BorderStyle.None;
        }
        /// <summary>
        /// Обрабатывает событие клика по плитке. Снимает выделение со всех других плиток в родительском контейнере 
        /// и выделяет текущую плитку.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        private void ShowGameView_Click(object sender, EventArgs e)
        {
            if (this.Parent is FlowLayoutPanel panel)
            {
                foreach (Control ctrl in panel.Controls)
                {
                    if (ctrl is ShowGameView tile)
                    {
                        tile.SetSelected(false);
                    }
                }
            }
            SetSelected(true);
        }
        /// <summary>
        /// Событие, возникающее при обновлении данных игры (например, после добавления отзыва).
        /// </summary>
        public event EventHandler GameUpdated;
        /// <summary>
        /// Обрабатывает событие двойного клика по плитке. Открывает форму с детальной информацией об игре и 
        /// подписывается на закрытие формы для обновления данных через событие GameUpdated.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        private void ShowGameView_DoubleClick(object sender, EventArgs e)
        {
            if (GameData?.ID > 0)
            {
                var detailsForm = new FullGameInformation(GameData.ID);
                detailsForm.FormClosed += (s, args) =>
                {
                    GameUpdated?.Invoke(this, EventArgs.Empty);
                };
                detailsForm.ShowDialog();
            }
        }
    }
}
