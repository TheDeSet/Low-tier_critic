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
        public ShowGameView()
        {
            InitializeComponent();
            LoadInformationOfGame();
        }
        private void LoadInformationOfGame()
        {
            // Добавляем клик по всей плитке
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

        // Свойства для привязки данных
        public Game GameData { get; private set; }
        public bool IsSelected { get; private set; }
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
        // Метод для выделения/снятия выделения
        public void SetSelected(bool selected)
        {
            IsSelected = selected;
            this.BackColor = selected ? Color.LightBlue : Color.White; // визуальная подсветка
            this.BorderStyle = selected ? BorderStyle.FixedSingle : BorderStyle.None;
        }
        // При клике на плитку — сообщаем родителю, что она выбрана
        private void ShowGameView_Click(object sender, EventArgs e)
        {
            // Снимаем выделение со всех плиток в родительском контейнере
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

            // Выделяем эту плитку
            SetSelected(true);
        }
        //открывает окно с полной информацией
        private void ShowGameView_DoubleClick(object sender, EventArgs e)
        {
            if (GameData?.ID > 0)
            {
                var detailsForm = new FullGameInformation(GameData.ID);
                detailsForm.ShowDialog(); // Модальное окно
            }
        }
    }
}
