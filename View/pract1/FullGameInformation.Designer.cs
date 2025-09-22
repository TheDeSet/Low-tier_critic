namespace View
{
    partial class FullGameInformation
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
            PIC_GameImage = new PictureBox();
            LB_GameName = new Label();
            label1 = new Label();
            LB_Developer = new Label();
            label3 = new Label();
            LB_YearOfRelease = new Label();
            label2 = new Label();
            FLP_Reviews = new FlowLayoutPanel();
            label4 = new Label();
            LB_Rating = new Label();
            BTN_MakeReview = new Button();
            label5 = new Label();
            RTB_Description = new RichTextBox();
            LTB_Platforms = new ListBox();
            PNL_Thumbnails = new Panel();
            HSCB_Thumbnails = new HScrollBar();
            ((System.ComponentModel.ISupportInitialize)PIC_GameImage).BeginInit();
            SuspendLayout();
            // 
            // PIC_GameImage
            // 
            PIC_GameImage.Location = new Point(12, 34);
            PIC_GameImage.Name = "PIC_GameImage";
            PIC_GameImage.Size = new Size(420, 202);
            PIC_GameImage.SizeMode = PictureBoxSizeMode.StretchImage;
            PIC_GameImage.TabIndex = 0;
            PIC_GameImage.TabStop = false;
            // 
            // LB_GameName
            // 
            LB_GameName.AutoSize = true;
            LB_GameName.Font = new Font("Segoe UI", 14F);
            LB_GameName.Location = new Point(12, 6);
            LB_GameName.Name = "LB_GameName";
            LB_GameName.Size = new Size(142, 25);
            LB_GameName.TabIndex = 1;
            LB_GameName.Text = "Название игры";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F);
            label1.Location = new Point(12, 332);
            label1.Name = "label1";
            label1.Size = new Size(106, 19);
            label1.TabIndex = 2;
            label1.Text = "Создатель(-ли):";
            // 
            // LB_Developer
            // 
            LB_Developer.AutoSize = true;
            LB_Developer.Font = new Font("Segoe UI", 10F);
            LB_Developer.Location = new Point(118, 332);
            LB_Developer.Name = "LB_Developer";
            LB_Developer.Size = new Size(45, 19);
            LB_Developer.TabIndex = 3;
            LB_Developer.Text = "label2";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F);
            label3.Location = new Point(12, 354);
            label3.Name = "label3";
            label3.Size = new Size(91, 19);
            label3.TabIndex = 4;
            label3.Text = "Год выпуска:";
            // 
            // LB_YearOfRelease
            // 
            LB_YearOfRelease.AutoSize = true;
            LB_YearOfRelease.Font = new Font("Segoe UI", 10F);
            LB_YearOfRelease.Location = new Point(104, 354);
            LB_YearOfRelease.Name = "LB_YearOfRelease";
            LB_YearOfRelease.Size = new Size(45, 19);
            LB_YearOfRelease.TabIndex = 5;
            LB_YearOfRelease.Text = "label4";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(12, 376);
            label2.Name = "label2";
            label2.Size = new Size(88, 19);
            label2.TabIndex = 6;
            label2.Text = "Платформы:";
            // 
            // FLP_Reviews
            // 
            FLP_Reviews.AutoScroll = true;
            FLP_Reviews.Location = new Point(12, 536);
            FLP_Reviews.Name = "FLP_Reviews";
            FLP_Reviews.Size = new Size(676, 200);
            FLP_Reviews.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 22F);
            label4.Location = new Point(465, 23);
            label4.Name = "label4";
            label4.Size = new Size(200, 41);
            label4.TabIndex = 9;
            label4.Text = "Рейтинг игры";
            // 
            // LB_Rating
            // 
            LB_Rating.AutoSize = true;
            LB_Rating.Font = new Font("Segoe UI", 22F);
            LB_Rating.Location = new Point(466, 64);
            LB_Rating.Name = "LB_Rating";
            LB_Rating.Size = new Size(97, 41);
            LB_Rating.TabIndex = 10;
            LB_Rating.Text = "label5";
            // 
            // BTN_MakeReview
            // 
            BTN_MakeReview.BackColor = SystemColors.Control;
            BTN_MakeReview.Font = new Font("Segoe UI", 20F);
            BTN_MakeReview.Location = new Point(454, 191);
            BTN_MakeReview.Name = "BTN_MakeReview";
            BTN_MakeReview.Size = new Size(216, 45);
            BTN_MakeReview.TabIndex = 11;
            BTN_MakeReview.Text = "Оставить отзыв";
            BTN_MakeReview.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F);
            label5.Location = new Point(12, 397);
            label5.Name = "label5";
            label5.Size = new Size(72, 19);
            label5.TabIndex = 12;
            label5.Text = "Описание";
            // 
            // RTB_Description
            // 
            RTB_Description.BackColor = SystemColors.ButtonHighlight;
            RTB_Description.Font = new Font("Segoe UI", 10F);
            RTB_Description.Location = new Point(12, 419);
            RTB_Description.Name = "RTB_Description";
            RTB_Description.ReadOnly = true;
            RTB_Description.Size = new Size(676, 111);
            RTB_Description.TabIndex = 14;
            RTB_Description.Text = "";
            // 
            // LTB_Platforms
            // 
            LTB_Platforms.Font = new Font("Segoe UI", 10F);
            LTB_Platforms.FormattingEnabled = true;
            LTB_Platforms.ItemHeight = 17;
            LTB_Platforms.Location = new Point(104, 376);
            LTB_Platforms.Name = "LTB_Platforms";
            LTB_Platforms.Size = new Size(582, 21);
            LTB_Platforms.TabIndex = 15;
            // 
            // PNL_Thumbnails
            // 
            PNL_Thumbnails.BorderStyle = BorderStyle.FixedSingle;
            PNL_Thumbnails.Location = new Point(12, 242);
            PNL_Thumbnails.Name = "PNL_Thumbnails";
            PNL_Thumbnails.Size = new Size(420, 63);
            PNL_Thumbnails.TabIndex = 16;
            // 
            // HSCB_Thumbnails
            // 
            HSCB_Thumbnails.LargeChange = 30;
            HSCB_Thumbnails.Location = new Point(12, 309);
            HSCB_Thumbnails.Name = "HSCB_Thumbnails";
            HSCB_Thumbnails.Size = new Size(420, 17);
            HSCB_Thumbnails.SmallChange = 10;
            HSCB_Thumbnails.TabIndex = 17;
            // 
            // FullGameInformation
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(700, 743);
            Controls.Add(HSCB_Thumbnails);
            Controls.Add(PNL_Thumbnails);
            Controls.Add(LTB_Platforms);
            Controls.Add(RTB_Description);
            Controls.Add(label5);
            Controls.Add(BTN_MakeReview);
            Controls.Add(LB_Rating);
            Controls.Add(label4);
            Controls.Add(FLP_Reviews);
            Controls.Add(label2);
            Controls.Add(LB_YearOfRelease);
            Controls.Add(label3);
            Controls.Add(LB_Developer);
            Controls.Add(label1);
            Controls.Add(LB_GameName);
            Controls.Add(PIC_GameImage);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FullGameInformation";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Информация";
            ((System.ComponentModel.ISupportInitialize)PIC_GameImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox PIC_GameImage;
        private Label LB_GameName;
        private Label label1;
        private Label LB_Developer;
        private Label label3;
        private Label LB_YearOfRelease;
        private Label label2;
        private FlowLayoutPanel FLP_Reviews;
        private Label label4;
        private Label LB_Rating;
        private Button BTN_MakeReview;
        private Label label5;
        private RichTextBox RTB_Description;
        private ListBox LTB_Platforms;
        private Panel PNL_Thumbnails;
        private HScrollBar HSCB_Thumbnails;
    }
}