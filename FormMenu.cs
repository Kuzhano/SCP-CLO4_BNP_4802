using System;
using System.Windows.Forms;

namespace DeLFINA_GUI
{
    public partial class FormMenu : Form
    {
        private readonly UserAccount _currentUser;
        private readonly JsonRepository<Proposal> _repository;
        private readonly AppConfig _config;

        public FormMenu(UserAccount user)
        {
            InitializeComponent();
            _currentUser = user ?? throw new ArgumentNullException(nameof(user));

            _config = AppConfig.LoadConfiguration();

            _repository = new JsonRepository<Proposal>(_config.ProposalFilePath);

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

        private void btnPendaftaran_Click(object sender, EventArgs e)
        {
            FormPendaftaran pendaftaran = new FormPendaftaran(_currentUser);
            this.Hide();

            pendaftaran.FormClosed += (s, args) => this.Show();
            pendaftaran.Show();
        }

        private void btnReview_Click(object sender, EventArgs e)
        {
            FormReview review = new FormReview(_repository, _currentUser);

            this.Hide();

            review.FormClosed += (s, args) => this.Show();
            review.Show();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            FormDashboard dashboard = new FormDashboard(_repository, _config);

            this.Hide();

            dashboard.FormClosed += (s, args) => this.Show();
            dashboard.Show();
        }

        private void btnEkspor_Click(object sender, EventArgs e)
        {
            FormPengarsipan Pengersipan = new FormPengarsipan(_repository, _currentUser);

            this.Hide();

            Pengersipan.FormClosed += (s, args) => this.Show();
            Pengersipan.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            FormLogin loginForm = new FormLogin(new JsonAuthService("users.json"));

            loginForm.Show();
            this.Dispose();
        }

        // Pastikan aplikasi benar-benar mati jika user menekan tombol 'X' merah di pojok kanan atas
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Application.Exit();
        }
    }
}