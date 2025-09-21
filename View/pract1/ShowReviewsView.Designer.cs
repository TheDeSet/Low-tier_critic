namespace View
{
    partial class ShowReviewsView
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            PIC_UserIcon = new PictureBox();
            RTB_ReviewText = new RichTextBox();
            LB_Username = new Label();
            LB_Rating = new Label();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)PIC_UserIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // PIC_UserIcon
            // 
            PIC_UserIcon.Location = new Point(5, 3);
            PIC_UserIcon.Name = "PIC_UserIcon";
            PIC_UserIcon.Size = new Size(192, 186);
            PIC_UserIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            PIC_UserIcon.TabIndex = 0;
            PIC_UserIcon.TabStop = false;
            // 
            // RTB_ReviewText
            // 
            RTB_ReviewText.Location = new Point(200, 66);
            RTB_ReviewText.Name = "RTB_ReviewText";
            RTB_ReviewText.ReadOnly = true;
            RTB_ReviewText.Size = new Size(453, 120);
            RTB_ReviewText.TabIndex = 1;
            RTB_ReviewText.Text = "";
            // 
            // LB_Username
            // 
            LB_Username.AutoSize = true;
            LB_Username.Font = new Font("Segoe UI", 14F);
            LB_Username.Location = new Point(199, 6);
            LB_Username.Name = "LB_Username";
            LB_Username.Size = new Size(134, 25);
            LB_Username.TabIndex = 2;
            LB_Username.Text = "Пользователь";
            // 
            // LB_Rating
            // 
            LB_Rating.AutoSize = true;
            LB_Rating.Font = new Font("Segoe UI", 14F);
            LB_Rating.Location = new Point(233, 36);
            LB_Rating.Name = "LB_Rating";
            LB_Rating.Size = new Size(81, 25);
            LB_Rating.TabIndex = 3;
            LB_Rating.Text = "Рейтинг";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Rating;
            pictureBox2.Location = new Point(203, 36);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(26, 26);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            // 
            // ShowReviewsView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pictureBox2);
            Controls.Add(LB_Rating);
            Controls.Add(LB_Username);
            Controls.Add(RTB_ReviewText);
            Controls.Add(PIC_UserIcon);
            Name = "ShowReviewsView";
            Size = new Size(656, 192);
            ((System.ComponentModel.ISupportInitialize)PIC_UserIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox PIC_UserIcon;
        private RichTextBox RTB_ReviewText;
        private Label LB_Username;
        private Label LB_Rating;
        private PictureBox pictureBox2;
    }
}
