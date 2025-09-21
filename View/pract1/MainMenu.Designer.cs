namespace pract1
{
    partial class MainMenu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            FLP_GamesView = new FlowLayoutPanel();
            panel1 = new Panel();
            BTN_Reset = new Button();
            CMB_GamesFromSeries = new ComboBox();
            label3 = new Label();
            CMB_Filter = new ComboBox();
            label2 = new Label();
            CMB_Sort = new ComboBox();
            label1 = new Label();
            TB_Search = new TextBox();
            BTN_Menu = new Button();
            PNL_Menu = new Panel();
            BTN_Delete = new Button();
            BTN_Update = new Button();
            BTN_Add = new Button();
            panel1.SuspendLayout();
            PNL_Menu.SuspendLayout();
            SuspendLayout();
            // 
            // FLP_GamesView
            // 
            FLP_GamesView.AutoScroll = true;
            FLP_GamesView.BackColor = SystemColors.ButtonHighlight;
            FLP_GamesView.Location = new Point(10, 88);
            FLP_GamesView.Name = "FLP_GamesView";
            FLP_GamesView.Padding = new Padding(10);
            FLP_GamesView.Size = new Size(1000, 458);
            FLP_GamesView.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.DimGray;
            panel1.Controls.Add(BTN_Reset);
            panel1.Controls.Add(CMB_GamesFromSeries);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(CMB_Filter);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(CMB_Sort);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(TB_Search);
            panel1.Controls.Add(BTN_Menu);
            panel1.Location = new Point(-2, -1);
            panel1.Name = "panel1";
            panel1.Size = new Size(1021, 72);
            panel1.TabIndex = 1;
            // 
            // BTN_Reset
            // 
            BTN_Reset.Font = new Font("Segoe UI", 11F);
            BTN_Reset.Location = new Point(567, 7);
            BTN_Reset.Name = "BTN_Reset";
            BTN_Reset.Size = new Size(94, 28);
            BTN_Reset.TabIndex = 8;
            BTN_Reset.Text = "Сбросить";
            BTN_Reset.UseVisualStyleBackColor = true;
            // 
            // CMB_GamesFromSeries
            // 
            CMB_GamesFromSeries.DropDownStyle = ComboBoxStyle.DropDownList;
            CMB_GamesFromSeries.Font = new Font("Segoe UI", 11F);
            CMB_GamesFromSeries.FormattingEnabled = true;
            CMB_GamesFromSeries.Items.AddRange(new object[] { "все серии" });
            CMB_GamesFromSeries.Location = new Point(805, 38);
            CMB_GamesFromSeries.Name = "CMB_GamesFromSeries";
            CMB_GamesFromSeries.Size = new Size(207, 28);
            CMB_GamesFromSeries.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13F);
            label3.ForeColor = SystemColors.ButtonFace;
            label3.Location = new Point(566, 38);
            label3.Name = "label3";
            label3.Size = new Size(236, 25);
            label3.TabIndex = 6;
            label3.Text = "Сгруппировать игры серии";
            // 
            // CMB_Filter
            // 
            CMB_Filter.DropDownStyle = ComboBoxStyle.DropDownList;
            CMB_Filter.Font = new Font("Segoe UI", 11F);
            CMB_Filter.FormattingEnabled = true;
            CMB_Filter.Items.AddRange(new object[] { "названию", "разработчику", "искать по всему" });
            CMB_Filter.Location = new Point(215, 25);
            CMB_Filter.Name = "CMB_Filter";
            CMB_Filter.Size = new Size(121, 28);
            CMB_Filter.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13F);
            label2.ForeColor = SystemColors.ButtonFace;
            label2.Location = new Point(661, 7);
            label2.Name = "label2";
            label2.Size = new Size(138, 25);
            label2.TabIndex = 4;
            label2.Text = "Сортировка по";
            // 
            // CMB_Sort
            // 
            CMB_Sort.DropDownStyle = ComboBoxStyle.DropDownList;
            CMB_Sort.Font = new Font("Segoe UI", 11F);
            CMB_Sort.FormattingEnabled = true;
            CMB_Sort.Items.AddRange(new object[] { "без сортировки", "возрастанию (рейтинг)", "убыванию (рейтинг)" });
            CMB_Sort.Location = new Point(805, 7);
            CMB_Sort.Name = "CMB_Sort";
            CMB_Sort.Size = new Size(207, 28);
            CMB_Sort.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13F);
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(125, 26);
            label1.Name = "label1";
            label1.Size = new Size(89, 25);
            label1.TabIndex = 2;
            label1.Text = "Поиск по";
            // 
            // TB_Search
            // 
            TB_Search.Font = new Font("Segoe UI", 13F);
            TB_Search.Location = new Point(342, 23);
            TB_Search.Name = "TB_Search";
            TB_Search.Size = new Size(218, 31);
            TB_Search.TabIndex = 1;
            // 
            // BTN_Menu
            // 
            BTN_Menu.Font = new Font("Segoe UI", 13F);
            BTN_Menu.Location = new Point(12, 19);
            BTN_Menu.Name = "BTN_Menu";
            BTN_Menu.Size = new Size(108, 40);
            BTN_Menu.TabIndex = 0;
            BTN_Menu.Text = "Меню";
            BTN_Menu.UseVisualStyleBackColor = true;
            // 
            // PNL_Menu
            // 
            PNL_Menu.BackColor = SystemColors.ControlDarkDark;
            PNL_Menu.BorderStyle = BorderStyle.FixedSingle;
            PNL_Menu.Controls.Add(BTN_Delete);
            PNL_Menu.Controls.Add(BTN_Update);
            PNL_Menu.Controls.Add(BTN_Add);
            PNL_Menu.Location = new Point(10, 64);
            PNL_Menu.Name = "PNL_Menu";
            PNL_Menu.Size = new Size(108, 108);
            PNL_Menu.TabIndex = 2;
            PNL_Menu.Visible = false;
            // 
            // BTN_Delete
            // 
            BTN_Delete.Location = new Point(16, 69);
            BTN_Delete.Name = "BTN_Delete";
            BTN_Delete.Size = new Size(75, 23);
            BTN_Delete.TabIndex = 2;
            BTN_Delete.Text = "Удалить";
            BTN_Delete.UseVisualStyleBackColor = true;
            // 
            // BTN_Update
            // 
            BTN_Update.Location = new Point(16, 40);
            BTN_Update.Name = "BTN_Update";
            BTN_Update.Size = new Size(75, 23);
            BTN_Update.TabIndex = 1;
            BTN_Update.Text = "Изменить";
            BTN_Update.UseVisualStyleBackColor = true;
            // 
            // BTN_Add
            // 
            BTN_Add.Location = new Point(17, 11);
            BTN_Add.Name = "BTN_Add";
            BTN_Add.Size = new Size(75, 23);
            BTN_Add.TabIndex = 0;
            BTN_Add.Text = "Добавить";
            BTN_Add.UseVisualStyleBackColor = true;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(1018, 558);
            Controls.Add(PNL_Menu);
            Controls.Add(panel1);
            Controls.Add(FLP_GamesView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Меню";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            PNL_Menu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel FLP_GamesView;
        private Panel panel1;
        private Button BTN_Menu;
        private Label label2;
        private ComboBox CMB_Sort;
        private Label label1;
        private TextBox TB_Search;
        private ComboBox CMB_Filter;
        private ComboBox CMB_GamesFromSeries;
        private Label label3;
        private Button BTN_Reset;
        private Panel PNL_Menu;
        private Button BTN_Delete;
        private Button BTN_Update;
        private Button BTN_Add;
    }
}
