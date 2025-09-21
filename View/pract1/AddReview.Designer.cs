namespace View
{
    partial class AddReview
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
            RTB_ReviewText = new RichTextBox();
            TB_Rating = new MaskedTextBox();
            TB_Username = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(128, 20);
            label1.TabIndex = 0;
            label1.Text = "Укажите ваш ник";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F);
            label2.Location = new Point(12, 41);
            label2.Name = "label2";
            label2.Size = new Size(177, 20);
            label2.TabIndex = 1;
            label2.Text = "Выставите рейтинг игре";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F);
            label3.Location = new Point(12, 71);
            label3.Name = "label3";
            label3.Size = new Size(207, 20);
            label3.TabIndex = 2;
            label3.Text = "Опишите ваши впечатления";
            // 
            // RTB_ReviewText
            // 
            RTB_ReviewText.BorderStyle = BorderStyle.FixedSingle;
            RTB_ReviewText.Font = new Font("Segoe UI", 11F);
            RTB_ReviewText.Location = new Point(12, 95);
            RTB_ReviewText.Name = "RTB_ReviewText";
            RTB_ReviewText.Size = new Size(551, 132);
            RTB_ReviewText.TabIndex = 3;
            RTB_ReviewText.Text = "";
            // 
            // TB_Rating
            // 
            TB_Rating.Font = new Font("Segoe UI", 11F);
            TB_Rating.Location = new Point(195, 41);
            TB_Rating.Mask = "0.0";
            TB_Rating.Name = "TB_Rating";
            TB_Rating.Size = new Size(368, 27);
            TB_Rating.TabIndex = 4;
            TB_Rating.ValidatingType = typeof(int);
            // 
            // TB_Username
            // 
            TB_Username.Font = new Font("Segoe UI", 11F);
            TB_Username.Location = new Point(146, 7);
            TB_Username.Name = "TB_Username";
            TB_Username.Size = new Size(417, 27);
            TB_Username.TabIndex = 5;
            // 
            // AddReview
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(575, 238);
            Controls.Add(TB_Username);
            Controls.Add(TB_Rating);
            Controls.Add(RTB_ReviewText);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "AddReview";
            Text = "Напишите свою рецензию";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private RichTextBox RTB_ReviewText;
        private MaskedTextBox TB_Rating;
        private TextBox TB_Username;
    }
}