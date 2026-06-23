namespace DeLFINA_GUI
{
    partial class ReviewForm
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
            dgvProposals = new DataGridView();
            txtCatatan = new TextBox();
            btnTerima = new Button();
            btnTolak = new Button();
            btnRevisi = new Button();
            lblCatatan = new Label();
            lblListProposal = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvProposals).BeginInit();
            SuspendLayout();
            // 
            // dgvProposals
            // 
            dgvProposals.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProposals.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProposals.Location = new Point(124, 41);
            dgvProposals.Name = "dgvProposals";
            dgvProposals.Size = new Size(555, 150);
            dgvProposals.TabIndex = 0;
            // 
            // txtCatatan
            // 
            txtCatatan.Location = new Point(124, 259);
            txtCatatan.Multiline = true;
            txtCatatan.Name = "txtCatatan";
            txtCatatan.Size = new Size(330, 80);
            txtCatatan.TabIndex = 1;
            // 
            // btnTerima
            // 
            btnTerima.BackColor = Color.Lime;
            btnTerima.Location = new Point(425, 211);
            btnTerima.Name = "btnTerima";
            btnTerima.Size = new Size(75, 23);
            btnTerima.TabIndex = 2;
            btnTerima.Text = "TERIMA";
            btnTerima.UseVisualStyleBackColor = false;
            btnTerima.Click += btnTerima_Click;
            // 
            // btnTolak
            // 
            btnTolak.BackColor = Color.Red;
            btnTolak.Location = new Point(514, 211);
            btnTolak.Name = "btnTolak";
            btnTolak.Size = new Size(75, 23);
            btnTolak.TabIndex = 3;
            btnTolak.Text = "TOLAK";
            btnTolak.UseVisualStyleBackColor = false;
            btnTolak.Click += btnTolak_Click;
            // 
            // btnRevisi
            // 
            btnRevisi.BackColor = Color.Yellow;
            btnRevisi.Location = new Point(604, 211);
            btnRevisi.Name = "btnRevisi";
            btnRevisi.Size = new Size(75, 23);
            btnRevisi.TabIndex = 4;
            btnRevisi.Text = "REVISI";
            btnRevisi.UseVisualStyleBackColor = false;
            btnRevisi.Click += btnRevisi_Click;
            // 
            // lblCatatan
            // 
            lblCatatan.AutoSize = true;
            lblCatatan.Location = new Point(124, 241);
            lblCatatan.Name = "lblCatatan";
            lblCatatan.Size = new Size(48, 15);
            lblCatatan.TabIndex = 5;
            lblCatatan.Text = "Catatan";
            // 
            // lblListProposal
            // 
            lblListProposal.AutoSize = true;
            lblListProposal.Location = new Point(124, 18);
            lblListProposal.Name = "lblListProposal";
            lblListProposal.Size = new Size(91, 15);
            lblListProposal.TabIndex = 6;
            lblListProposal.Text = "LIST PROPOSAL";
            // 
            // ReviewForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblListProposal);
            Controls.Add(lblCatatan);
            Controls.Add(btnRevisi);
            Controls.Add(btnTolak);
            Controls.Add(btnTerima);
            Controls.Add(txtCatatan);
            Controls.Add(dgvProposals);
            Name = "ReviewForm";
            Text = "Modul Review & Penilaian Proposal";
            ((System.ComponentModel.ISupportInitialize)dgvProposals).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvProposals;
        private TextBox txtCatatan;
        private Button btnTerima;
        private Button btnTolak;
        private Button btnRevisi;
        private Label lblCatatan;
        private Label lblListProposal;
    }
}