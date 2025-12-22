namespace View
{
    partial class AddNewGame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            CHKLTB_Platform = new CheckedListBox();
            label5 = new Label();
            RTB_Description = new RichTextBox();
            TB_GameName = new TextBox();
            TB_Developer = new TextBox();
            TB_YearOfRelease = new MaskedTextBox();
            label6 = new Label();
            TB_IconPath = new TextBox();
            PIC_Game = new PictureBox();
            BTN_Reset = new Button();
            label7 = new Label();
            TB_ScreenshotsPath = new TextBox();
            label8 = new Label();
            BTN_AddIconImage = new Button();
            BTN_AddScreenshotImage = new Button();
            BTN_Add = new Button();
            ((System.ComponentModel.ISupportInitialize)PIC_Game).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(142, 25);
            label1.TabIndex = 0;
            label1.Text = "Название игры";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F);
            label2.Location = new Point(12, 41);
            label2.Name = "label2";
            label2.Size = new Size(123, 25);
            label2.TabIndex = 1;
            label2.Text = "Разработчик";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14F);
            label3.Location = new Point(12, 66);
            label3.Name = "label3";
            label3.Size = new Size(118, 25);
            label3.TabIndex = 2;
            label3.Text = "Год выпуска";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14F);
            label4.Location = new Point(12, 93);
            label4.Name = "label4";
            label4.Size = new Size(115, 25);
            label4.TabIndex = 3;
            label4.Text = "Платформы";
            // 
            // CHKLTB_Platform
            // 
            CHKLTB_Platform.Font = new Font("Segoe UI", 10F);
            CHKLTB_Platform.FormattingEnabled = true;
            CHKLTB_Platform.Location = new Point(133, 101);
            CHKLTB_Platform.Name = "CHKLTB_Platform";
            CHKLTB_Platform.Size = new Size(144, 124);
            CHKLTB_Platform.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14F);
            label5.Location = new Point(12, 230);
            label5.Name = "label5";
            label5.Size = new Size(99, 25);
            label5.TabIndex = 5;
            label5.Text = "Описание";
            // 
            // RTB_Description
            // 
            RTB_Description.BorderStyle = BorderStyle.FixedSingle;
            RTB_Description.Location = new Point(12, 258);
            RTB_Description.Name = "RTB_Description";
            RTB_Description.Size = new Size(412, 108);
            RTB_Description.TabIndex = 6;
            RTB_Description.Text = "";
            // 
            // TB_GameName
            // 
            TB_GameName.Location = new Point(156, 11);
            TB_GameName.Name = "TB_GameName";
            TB_GameName.Size = new Size(183, 23);
            TB_GameName.TabIndex = 7;
            // 
            // TB_Developer
            // 
            TB_Developer.Location = new Point(156, 41);
            TB_Developer.Name = "TB_Developer";
            TB_Developer.Size = new Size(183, 23);
            TB_Developer.TabIndex = 8;
            // 
            // TB_YearOfRelease
            // 
            TB_YearOfRelease.Location = new Point(156, 72);
            TB_YearOfRelease.Mask = "0000";
            TB_YearOfRelease.Name = "TB_YearOfRelease";
            TB_YearOfRelease.Size = new Size(183, 23);
            TB_YearOfRelease.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14F);
            label6.Location = new Point(12, 380);
            label6.Name = "label6";
            label6.Size = new Size(206, 25);
            label6.TabIndex = 10;
            label6.Text = "Главное изображение";
            // 
            // TB_IconPath
            // 
            TB_IconPath.Location = new Point(12, 408);
            TB_IconPath.Name = "TB_IconPath";
            TB_IconPath.ReadOnly = true;
            TB_IconPath.Size = new Size(265, 23);
            TB_IconPath.TabIndex = 11;
            // 
            // PIC_Game
            // 
            PIC_Game.Location = new Point(429, 35);
            PIC_Game.Name = "PIC_Game";
            PIC_Game.Size = new Size(190, 252);
            PIC_Game.SizeMode = PictureBoxSizeMode.StretchImage;
            PIC_Game.TabIndex = 12;
            PIC_Game.TabStop = false;
            // 
            // BTN_Reset
            // 
            BTN_Reset.Location = new Point(462, 293);
            BTN_Reset.Name = "BTN_Reset";
            BTN_Reset.Size = new Size(132, 23);
            BTN_Reset.TabIndex = 13;
            BTN_Reset.Text = "сбросить картинку";
            BTN_Reset.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14F);
            label7.Location = new Point(12, 434);
            label7.Name = "label7";
            label7.Size = new Size(184, 25);
            label7.TabIndex = 14;
            label7.Text = "Скриншоты из игры";
            // 
            // TB_ScreenshotsPath
            // 
            TB_ScreenshotsPath.Location = new Point(12, 462);
            TB_ScreenshotsPath.Name = "TB_ScreenshotsPath";
            TB_ScreenshotsPath.ReadOnly = true;
            TB_ScreenshotsPath.Size = new Size(265, 23);
            TB_ScreenshotsPath.TabIndex = 15;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 14F);
            label8.Location = new Point(420, 6);
            label8.Name = "label8";
            label8.Size = new Size(206, 25);
            label8.TabIndex = 16;
            label8.Text = "Главное изображение";
            // 
            // BTN_AddIconImage
            // 
            BTN_AddIconImage.Location = new Point(283, 408);
            BTN_AddIconImage.Name = "BTN_AddIconImage";
            BTN_AddIconImage.Size = new Size(27, 23);
            BTN_AddIconImage.TabIndex = 17;
            BTN_AddIconImage.Text = "...";
            BTN_AddIconImage.UseVisualStyleBackColor = true;
            // 
            // BTN_AddScreenshotImage
            // 
            BTN_AddScreenshotImage.Location = new Point(283, 461);
            BTN_AddScreenshotImage.Name = "BTN_AddScreenshotImage";
            BTN_AddScreenshotImage.Size = new Size(27, 23);
            BTN_AddScreenshotImage.TabIndex = 18;
            BTN_AddScreenshotImage.Text = "...";
            BTN_AddScreenshotImage.UseVisualStyleBackColor = true;
            // 
            // BTN_Add
            // 
            BTN_Add.Font = new Font("Segoe UI", 14F);
            BTN_Add.Location = new Point(462, 452);
            BTN_Add.Name = "BTN_Add";
            BTN_Add.Size = new Size(122, 35);
            BTN_Add.TabIndex = 19;
            BTN_Add.Text = "Добавить";
            BTN_Add.UseVisualStyleBackColor = true;
            // 
            // AddNewGame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(625, 501);
            Controls.Add(BTN_Add);
            Controls.Add(BTN_AddScreenshotImage);
            Controls.Add(BTN_AddIconImage);
            Controls.Add(label8);
            Controls.Add(TB_ScreenshotsPath);
            Controls.Add(label7);
            Controls.Add(BTN_Reset);
            Controls.Add(PIC_Game);
            Controls.Add(TB_IconPath);
            Controls.Add(label6);
            Controls.Add(TB_YearOfRelease);
            Controls.Add(TB_Developer);
            Controls.Add(TB_GameName);
            Controls.Add(RTB_Description);
            Controls.Add(label5);
            Controls.Add(CHKLTB_Platform);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "AddNewGame";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Добавить новую игру";
            ((System.ComponentModel.ISupportInitialize)PIC_Game).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private CheckedListBox CHKLTB_Platform;
        private Label label5;
        private RichTextBox RTB_Description;
        private TextBox TB_GameName;
        private TextBox TB_Developer;
        private MaskedTextBox TB_YearOfRelease;
        private Label label6;
        private TextBox TB_IconPath;
        private PictureBox PIC_Game;
        private Button BTN_Reset;
        private Label label7;
        private TextBox TB_ScreenshotsPath;
        private Label label8;
        private Button BTN_AddIconImage;
        private Button BTN_AddScreenshotImage;
        private Button BTN_Add;
    }
}