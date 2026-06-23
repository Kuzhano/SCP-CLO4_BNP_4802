using System;
using System.Windows.Forms;

namespace DeLFINA_GUI
{
    public partial class FormMenu : Form
    {
        private readonly UserAccount _currentUser;

        public FormMenu(UserAccount user)
        {
            InitializeComponent();
            _currentUser = user ?? throw new ArgumentNullException(nameof(user));

            // Panggil mesin Automata saat form dimuat
            ConfigureUIState();
        }

        // TEKNIK AUTOMATA: Transisi state/aksesibilitas antarmuka berdasarkan Role
        private void ConfigureUIState()
        {
            lblWelcome.Text = $"User Aktif: {_currentUser.Username} | Akses: {_currentUser.Role.ToUpper()}";

            if (_currentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                // State Admin
                btnPendaftaran.Visible = false; // Admin tidak boleh mengajukan
                btnReview.Visible = true;
                btnDashboard.Visible = true;
                btnEkspor.Visible = true;
            }
            else if (_currentUser.Role.Equals("Dosen", StringComparison.OrdinalIgnoreCase))
            {
                // State Dosen
                btnPendaftaran.Visible = true;
                btnDashboard.Visible = true;
                btnReview.Visible = false; // Dosen tidak punya akses review
                btnEkspor.Visible = false; // Dosen tidak punya akses ekspor arsip
            }
        }

        // Event Handler Tombol (Nanti disambungkan dengan Form milik teman Anda)
        private void btnPendaftaran_Click(object sender, EventArgs e)
        {
            MessageBox.Show("[Routing] Membuka antarmuka Pendaftaran...", "Modul 2");
            // Nanti diganti: new FormPendaftaran().Show();
        }

        private void btnReview_Click(object sender, EventArgs e)
        {
            MessageBox.Show("[Routing] Membuka antarmuka Review & Penilaian...", "Modul 3");
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            MessageBox.Show("[Routing] Membuka antarmuka Dashboard...", "Modul 4");
        }

        private void btnEkspor_Click(object sender, EventArgs e)
        {
            MessageBox.Show("[Routing] Membuka antarmuka Pengarsipan...", "Modul 5");
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Transisi kembali ke State awal (Login)
            FormLogin loginForm = new FormLogin(new JsonAuthService("users.json"));
            loginForm.Show();
            this.Dispose(); // Membersihkan form menu dari memori
        }

        // Pastikan aplikasi benar-benar mati jika user menekan tombol 'X' merah di pojok kanan atas
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Application.Exit();
        }
    }
}