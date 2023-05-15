namespace SeaBattle3
{
    partial class Form1
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            grid_user = new DataGridView();
            grid_comp = new DataGridView();
            buttonRandom = new Button();
            buttonClear = new Button();
            buttonStart = new Button();
            buttonRestart = new Button();
            ((System.ComponentModel.ISupportInitialize)grid_user).BeginInit();
            ((System.ComponentModel.ISupportInitialize)grid_comp).BeginInit();
            SuspendLayout();
            // 
            // grid_user
            // 
            grid_user.AllowUserToAddRows = false;
            grid_user.AllowUserToDeleteRows = false;
            grid_user.AllowUserToResizeColumns = false;
            grid_user.AllowUserToResizeRows = false;
            grid_user.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid_user.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            grid_user.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            grid_user.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid_user.Location = new Point(12, 27);
            grid_user.Name = "grid_user";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            grid_user.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            grid_user.RowHeadersWidth = 58;
            grid_user.RowTemplate.Height = 29;
            grid_user.ScrollBars = ScrollBars.None;
            grid_user.SelectionMode = DataGridViewSelectionMode.CellSelect;
            grid_user.ShowCellErrors = false;
            grid_user.ShowCellToolTips = false;
            grid_user.ShowEditingIcon = false;
            grid_user.ShowRowErrors = false;
            grid_user.Size = new Size(344, 351);
            grid_user.TabIndex = 0;
            grid_user.KeyDown += grid_user_KeyDown;
            grid_user.MouseUp += grid_user_MouseUp;
            // 
            // grid_comp
            // 
            grid_comp.AllowUserToAddRows = false;
            grid_comp.AllowUserToDeleteRows = false;
            grid_comp.AllowUserToResizeColumns = false;
            grid_comp.AllowUserToResizeRows = false;
            grid_comp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid_comp.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            grid_comp.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            grid_comp.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid_comp.Location = new Point(444, 27);
            grid_comp.Name = "grid_comp";
            grid_comp.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            grid_comp.RowHeadersWidth = 58;
            grid_comp.RowTemplate.Height = 29;
            grid_comp.ScrollBars = ScrollBars.None;
            grid_comp.SelectionMode = DataGridViewSelectionMode.CellSelect;
            grid_comp.ShowCellErrors = false;
            grid_comp.ShowCellToolTips = false;
            grid_comp.ShowEditingIcon = false;
            grid_comp.ShowRowErrors = false;
            grid_comp.Size = new Size(344, 351);
            grid_comp.TabIndex = 1;
            grid_comp.CellClick += grid_comp_CellClick;
            // 
            // buttonRandom
            // 
            buttonRandom.Location = new Point(12, 397);
            buttonRandom.Name = "buttonRandom";
            buttonRandom.Size = new Size(159, 45);
            buttonRandom.TabIndex = 3;
            buttonRandom.Text = "Рандом";
            buttonRandom.UseVisualStyleBackColor = true;
            buttonRandom.Click += button2_Click;
            // 
            // buttonClear
            // 
            buttonClear.Location = new Point(195, 397);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(161, 45);
            buttonClear.TabIndex = 4;
            buttonClear.Text = "Очистить";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += buttonClear_Click;
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(195, 448);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(161, 45);
            buttonStart.TabIndex = 5;
            buttonStart.Text = "В бой!";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // buttonRestart
            // 
            buttonRestart.Location = new Point(12, 448);
            buttonRestart.Name = "buttonRestart";
            buttonRestart.Size = new Size(161, 45);
            buttonRestart.TabIndex = 6;
            buttonRestart.Text = "Рестарт";
            buttonRestart.UseVisualStyleBackColor = true;
            buttonRestart.Click += buttonRestart_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(800, 496);
            Controls.Add(buttonRestart);
            Controls.Add(buttonStart);
            Controls.Add(buttonClear);
            Controls.Add(buttonRandom);
            Controls.Add(grid_comp);
            Controls.Add(grid_user);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Морской Бой";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)grid_user).EndInit();
            ((System.ComponentModel.ISupportInitialize)grid_comp).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView grid_user;
        private DataGridView grid_comp;
        private Button buttonRandom;
        private Button buttonClear;
        private Button buttonStart;
        private Button buttonRestart;
    }
}