namespace View
{
    partial class UpdateGame
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
            BTN_Add = new Button();
            BTN_AddScreenshotImage = new Button();
            BTN_AddIconImage = new Button();
            label8 = new Label();
            TB_ScreenshotsPath = new TextBox();
            label7 = new Label();
            BTN_Reset = new Button();
            PIC_Game = new PictureBox();
            TB_IconPath = new TextBox();
            label6 = new Label();
            TB_YearOfRelease = new MaskedTextBox();
            TB_Developer = new TextBox();
            TB_GameName = new TextBox();
            RTB_Description = new RichTextBox();
            label5 = new Label();
            CHKLTB_Platform = new CheckedListBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)PIC_Game).BeginInit();
            SuspendLayout();
            // 
            // BTN_Add
            // 
            BTN_Add.Font = new Font("Segoe UI", 14F);
            BTN_Add.Location = new Point(458, 452);
            BTN_Add.Name = "BTN_Add";
            BTN_Add.Size = new Size(122, 35);
            BTN_Add.TabIndex = 39;
            BTN_Add.Text = "Изменить";
            BTN_Add.UseVisualStyleBackColor = true;
            // 
            // BTN_AddScreenshotImage
            // 
            BTN_AddScreenshotImage.Location = new Point(279, 461);
            BTN_AddScreenshotImage.Name = "BTN_AddScreenshotImage";
            BTN_AddScreenshotImage.Size = new Size(27, 23);
            BTN_AddScreenshotImage.TabIndex = 38;
            BTN_AddScreenshotImage.Text = "...";
            BTN_AddScreenshotImage.UseVisualStyleBackColor = true;
            // 
            // BTN_AddIconImage
            // 
            BTN_AddIconImage.Location = new Point(279, 408);
            BTN_AddIconImage.Name = "BTN_AddIconImage";
            BTN_AddIconImage.Size = new Size(27, 23);
            BTN_AddIconImage.TabIndex = 37;
            BTN_AddIconImage.Text = "...";
            BTN_AddIconImage.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 14F);
            label8.Location = new Point(416, 6);
            label8.Name = "label8";
            label8.Size = new Size(206, 25);
            label8.TabIndex = 36;
            label8.Text = "Главное изображение";
            // 
            // TB_ScreenshotsPath
            // 
            TB_ScreenshotsPath.Location = new Point(8, 462);
            TB_ScreenshotsPath.Name = "TB_ScreenshotsPath";
            TB_ScreenshotsPath.ReadOnly = true;
            TB_ScreenshotsPath.Size = new Size(265, 23);
            TB_ScreenshotsPath.TabIndex = 35;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14F);
            label7.Location = new Point(8, 434);
            label7.Name = "label7";
            label7.Size = new Size(184, 25);
            label7.TabIndex = 34;
            label7.Text = "Скриншоты из игры";
            // 
            // BTN_Reset
            // 
            BTN_Reset.Location = new Point(458, 293);
            BTN_Reset.Name = "BTN_Reset";
            BTN_Reset.Size = new Size(132, 23);
            BTN_Reset.TabIndex = 33;
            BTN_Reset.Text = "сбросить картинку";
            BTN_Reset.UseVisualStyleBackColor = true;
            // 
            // PIC_Game
            // 
            PIC_Game.Location = new Point(425, 35);
            PIC_Game.Name = "PIC_Game";
            PIC_Game.Size = new Size(190, 252);
            PIC_Game.SizeMode = PictureBoxSizeMode.StretchImage;
            PIC_Game.TabIndex = 32;
            PIC_Game.TabStop = false;
            // 
            // TB_IconPath
            // 
            TB_IconPath.Location = new Point(8, 408);
            TB_IconPath.Name = "TB_IconPath";
            TB_IconPath.ReadOnly = true;
            TB_IconPath.Size = new Size(265, 23);
            TB_IconPath.TabIndex = 31;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14F);
            label6.Location = new Point(8, 380);
            label6.Name = "label6";
            label6.Size = new Size(206, 25);
            label6.TabIndex = 30;
            label6.Text = "Главное изображение";
            // 
            // TB_YearOfRelease
            // 
            TB_YearOfRelease.Location = new Point(152, 72);
            TB_YearOfRelease.Mask = "0000";
            TB_YearOfRelease.Name = "TB_YearOfRelease";
            TB_YearOfRelease.Size = new Size(183, 23);
            TB_YearOfRelease.TabIndex = 29;
            // 
            // TB_Developer
            // 
            TB_Developer.Location = new Point(152, 41);
            TB_Developer.Name = "TB_Developer";
            TB_Developer.Size = new Size(183, 23);
            TB_Developer.TabIndex = 28;
            // 
            // TB_GameName
            // 
            TB_GameName.Location = new Point(152, 11);
            TB_GameName.Name = "TB_GameName";
            TB_GameName.Size = new Size(183, 23);
            TB_GameName.TabIndex = 27;
            // 
            // RTB_Description
            // 
            RTB_Description.BorderStyle = BorderStyle.FixedSingle;
            RTB_Description.Location = new Point(8, 258);
            RTB_Description.Name = "RTB_Description";
            RTB_Description.Size = new Size(412, 108);
            RTB_Description.TabIndex = 26;
            RTB_Description.Text = "";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14F);
            label5.Location = new Point(8, 230);
            label5.Name = "label5";
            label5.Size = new Size(99, 25);
            label5.TabIndex = 25;
            label5.Text = "Описание";
            // 
            // CHKLTB_Platform
            // 
            CHKLTB_Platform.Font = new Font("Segoe UI", 10F);
            CHKLTB_Platform.FormattingEnabled = true;
            CHKLTB_Platform.Location = new Point(129, 101);
            CHKLTB_Platform.Name = "CHKLTB_Platform";
            CHKLTB_Platform.Size = new Size(206, 124);
            CHKLTB_Platform.TabIndex = 24;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14F);
            label4.Location = new Point(8, 93);
            label4.Name = "label4";
            label4.Size = new Size(115, 25);
            label4.TabIndex = 23;
            label4.Text = "Платформы";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14F);
            label3.Location = new Point(8, 66);
            label3.Name = "label3";
            label3.Size = new Size(118, 25);
            label3.TabIndex = 22;
            label3.Text = "Год выпуска";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F);
            label2.Location = new Point(8, 41);
            label2.Name = "label2";
            label2.Size = new Size(123, 25);
            label2.TabIndex = 21;
            label2.Text = "Разработчик";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F);
            label1.Location = new Point(8, 9);
            label1.Name = "label1";
            label1.Size = new Size(142, 25);
            label1.TabIndex = 20;
            label1.Text = "Название игры";
            // 
            // UpdateGame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(628, 502);
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
            Name = "UpdateGame";
            Text = "Изменить информацию об игре";
            ((System.ComponentModel.ISupportInitialize)PIC_Game).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BTN_Add;
        private Button BTN_AddScreenshotImage;
        private Button BTN_AddIconImage;
        private Label label8;
        private TextBox TB_ScreenshotsPath;
        private Label label7;
        private Button BTN_Reset;
        private PictureBox PIC_Game;
        private TextBox TB_IconPath;
        private Label label6;
        private MaskedTextBox TB_YearOfRelease;
        private TextBox TB_Developer;
        private TextBox TB_GameName;
        private RichTextBox RTB_Description;
        private Label label5;
        private CheckedListBox CHKLTB_Platform;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}