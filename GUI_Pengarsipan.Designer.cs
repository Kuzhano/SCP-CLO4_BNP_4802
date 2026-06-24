namespace DeLFINA_GUI
{
    partial class GUI_Pengarsipan
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
            comboBox1 = new ComboBox();
            dataGridView1 = new DataGridView();
            dataGridView_1 = new TabControl();
            tabPage1 = new TabPage();
            btnExportKonten = new Button();
            tabPage2 = new TabPage();
            btnExportHLE = new Button();
            dataGridView2 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            dataGridView_1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Admin", "Dosen" });
            comboBox1.Location = new Point(194, 41);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 0;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(359, 180);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // dataGridView_1
            // 
            dataGridView_1.Controls.Add(tabPage1);
            dataGridView_1.Controls.Add(tabPage2);
            dataGridView_1.Location = new Point(194, 85);
            dataGridView_1.Name = "dataGridView_1";
            dataGridView_1.SelectedIndex = 0;
            dataGridView_1.Size = new Size(373, 219);
            dataGridView_1.TabIndex = 2;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(btnExportKonten);
            tabPage1.Controls.Add(dataGridView1);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(365, 186);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Produksi Konten";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnExportKonten
            // 
            btnExportKonten.Location = new Point(0, 157);
            btnExportKonten.Name = "btnExportKonten";
            btnExportKonten.Size = new Size(175, 29);
            btnExportKonten.TabIndex = 3;
            btnExportKonten.Text = "ExportKonten";
            btnExportKonten.UseVisualStyleBackColor = true;
            btnExportKonten.Click += btnExportKonten_Click;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(btnExportHLE);
            tabPage2.Controls.Add(dataGridView2);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(365, 186);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Proposal HLE";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnExportHLE
            // 
            btnExportHLE.Location = new Point(0, 155);
            btnExportHLE.Name = "btnExportHLE";
            btnExportHLE.Size = new Size(185, 31);
            btnExportHLE.TabIndex = 1;
            btnExportHLE.Text = "ExportHLE";
            btnExportHLE.UseVisualStyleBackColor = true;
            btnExportHLE.Click += btnExportKonten_Click;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView2.Location = new Point(3, 3);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(359, 180);
            dataGridView2.TabIndex = 0;
            // 
            // GUI_Pengarsipan
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView_1);
            Controls.Add(comboBox1);
            Name = "GUI_Pengarsipan";
            Text = "GUI_Pengarsipan";
            Load += GUI_Pengarsipan_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            dataGridView_1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox comboBox1;
        private DataGridView dataGridView1;
        private TabControl dataGridView_1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private DataGridView dataGridView2;
        private Button btnExportKonten;
        private Button btnExportHLE;
    }
}