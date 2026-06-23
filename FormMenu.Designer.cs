namespace DeLFINA_GUI
{
    partial class FormMenu
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
            lblWelcome = new Label();
            btnPendaftaran = new Button();
            btnReview = new Button();
            btnDashboard = new Button();
            btnEkspor = new Button();
            btnLogout = new Button();
            SuspendLayout();
            // 
            // lblWelcome
            // 
            lblWelcome.Location = new Point(12, 16);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(360, 15);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Welcome";
            lblWelcome.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnPendaftaran
            // 
            btnPendaftaran.Location = new Point(140, 44);
            btnPendaftaran.Name = "btnPendaftaran";
            btnPendaftaran.Size = new Size(100, 25);
            btnPendaftaran.TabIndex = 1;
            btnPendaftaran.Text = "Pendaftaran";
            btnPendaftaran.UseVisualStyleBackColor = true;
            // 
            // btnReview
            // 
            btnReview.Location = new Point(140, 106);
            btnReview.Name = "btnReview";
            btnReview.Size = new Size(100, 25);
            btnReview.TabIndex = 2;
            btnReview.Text = "Review";
            btnReview.UseVisualStyleBackColor = true;
            // 
            // btnDashboard
            // 
            btnDashboard.Location = new Point(140, 75);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Size = new Size(100, 25);
            btnDashboard.TabIndex = 3;
            btnDashboard.Text = "Dashboard";
            btnDashboard.UseVisualStyleBackColor = true;
            // 
            // btnEkspor
            // 
            btnEkspor.Location = new Point(140, 137);
            btnEkspor.Name = "btnEkspor";
            btnEkspor.Size = new Size(100, 25);
            btnEkspor.TabIndex = 4;
            btnEkspor.Text = "Ekspor";
            btnEkspor.UseVisualStyleBackColor = true;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.LightCoral;
            btnLogout.Location = new Point(140, 207);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(100, 23);
            btnLogout.TabIndex = 5;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // FormMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 261);
            Controls.Add(btnLogout);
            Controls.Add(btnEkspor);
            Controls.Add(btnDashboard);
            Controls.Add(btnReview);
            Controls.Add(btnPendaftaran);
            Controls.Add(lblWelcome);
            Name = "FormMenu";
            Text = "FormMenu";
            ResumeLayout(false);
        }

        #endregion

        private Label lblWelcome;
        private Button btnPendaftaran;
        private Button btnReview;
        private Button btnDashboard;
        private Button btnEkspor;
        private Button btnLogout;
    }
}