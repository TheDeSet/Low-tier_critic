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
    public partial class AddReview : Form
    {
        /// <summary>
        /// Инициализирует форму добавления отзыва для конкретной игры.
        /// </summary>
        /// <param name="gameId">ID игры, к которой будет добавлен отзыв.</param>
        public AddReview(int gameId)
        {
            _gameId = gameId;
            InitializeComponent();
            BTN_Add.Click += BtnAdd_Click;
            BTN_Cancel.Click += (s, e) => this.Close();
        }
        private int _gameId;

        /// <summary>
        /// Обрабатывает событие нажатия кнопки добавления отзыва. Проверяет валидность данных, создает объект отзыва и сохраняет его через логику.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {

            if (!float.TryParse(TB_Rating.Text, out float rating) || rating < 1.0f || rating > 5.0f)
            {
                MessageBox.Show("Пожалуйста, введите рейтинг от 1.0 до 5.0 (например: 4.5)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(RTB_ReviewText.Text))
            {
                MessageBox.Show("Введите ваши впечатления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var review = new Review
            {
                Username = TB_Username.Text,
                Rating = rating,
                ReviewText = RTB_ReviewText.Text.Trim()
            };

            bool success = Logic.AddReviewToGame(_gameId, review);

            if (success)
            {
                MessageBox.Show("Отзыв успешно добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; 
                this.Close();
            }
            else
            {
                MessageBox.Show("Не удалось добавить отзыв.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
