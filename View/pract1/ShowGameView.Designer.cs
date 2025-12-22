namespace pract1
{
    partial class ShowGameView
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
            PIC_Game = new PictureBox();
            LB_Rating_Game = new Label();
            pictureBox2 = new PictureBox();
            LB_Game_Name = new Label();
            LB_Developer = new Label();
            ((System.ComponentModel.ISupportInitialize)PIC_Game).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // PIC_Game
            // 
            PIC_Game.Location = new Point(3, 3);
            PIC_Game.Name = "PIC_Game";
            PIC_Game.Size = new Size(190, 252);
            PIC_Game.SizeMode = PictureBoxSizeMode.StretchImage;
            PIC_Game.TabIndex = 0;
            PIC_Game.TabStop = false;
            // 
            // LB_Rating_Game
            // 
            LB_Rating_Game.AutoSize = true;
            LB_Rating_Game.Font = new Font("Segoe UI", 12F);
            LB_Rating_Game.Location = new Point(75, 320);
            LB_Rating_Game.Name = "LB_Rating_Game";
            LB_Rating_Game.Size = new Size(52, 21);
            LB_Rating_Game.TabIndex = 2;
            LB_Rating_Game.Text = "label2";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = View.Properties.Resources.Rating;
            pictureBox2.Location = new Point(54, 320);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(22, 20);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // LB_Game_Name
            // 
            LB_Game_Name.AutoSize = true;
            LB_Game_Name.Font = new Font("Segoe UI", 12F);
            LB_Game_Name.Location = new Point(5, 262);
            LB_Game_Name.Name = "LB_Game_Name";
            LB_Game_Name.Size = new Size(52, 21);
            LB_Game_Name.TabIndex = 4;
            LB_Game_Name.Text = "label1";
            // 
            // LB_Developer
            // 
            LB_Developer.AutoSize = true;
            LB_Developer.Font = new Font("Segoe UI", 12F);
            LB_Developer.Location = new Point(5, 290);
            LB_Developer.Name = "LB_Developer";
            LB_Developer.Size = new Size(52, 21);
            LB_Developer.TabIndex = 5;
            LB_Developer.Text = "label1";
            // 
            // ShowGameView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(LB_Developer);
            Controls.Add(LB_Game_Name);
            Controls.Add(pictureBox2);
            Controls.Add(LB_Rating_Game);
            Controls.Add(PIC_Game);
            Name = "ShowGameView";
            Size = new Size(197, 347);
            DoubleClick += ShowGameView_DoubleClick;
            ((System.ComponentModel.ISupportInitialize)PIC_Game).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox PIC_Game;
        private Label LB_Rating_Game;
        private PictureBox pictureBox2;
        private Label LB_Game_Name;
        private Label LB_Developer;
    }
}
