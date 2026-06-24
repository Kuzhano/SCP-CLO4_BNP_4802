using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DeLFINA_GUI
{
    public class FormPendaftaran : Form
    {
        private readonly JsonRepository<Proposal> _repository;
        private readonly UserAccount _currentUser;

        private TextBox txtJudul, txtLinkPdf;
        private DataGridView gridProposal;
        private Button btnSimpan, btnKembali;
        private Label lblHeader, lblJudul, lblLink, lblDaftar;

        public FormPendaftaran(UserAccount loggedInUser)
        {
            // DbC: Invariant Check
            _currentUser = loggedInUser ?? throw new ArgumentNullException(nameof(loggedInUser));

            // Teknik Runtime Config & Generics
            AppConfig config = AppConfig.LoadConfiguration();
            _repository = new JsonRepository<Proposal>(config.ProposalFilePath);

            InitializeComponent();
            LoadDataGrid();
        }

        private void InitializeComponent()
        {
            this.Text = "DeLFINA - Modul 2: Pendaftaran Proposal";
            this.Size = new Size(820, 540);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 246, 250);

            Font fontLabel = new Font("Segoe UI", 10, FontStyle.Regular);
            Font fontTextBox = new Font("Segoe UI", 10, FontStyle.Regular);

            lblHeader = new Label()
            {
                Text = "DASHBOARD PENGAJUAN PROPOSAL DOSEN",
                Location = new Point(20, 15),
                Width = 760,
                Height = 30,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(47, 54, 64)
            };

            lblJudul = new Label() { Text = "Judul Proposal", Location = new Point(20, 65), Width = 125, Font = fontLabel };
            txtJudul = new TextBox() { Location = new Point(160, 62), Width = 600, Font = fontTextBox };

            lblLink = new Label() { Text = "Link Tautan PDF", Location = new Point(20, 105), Width = 125, Font = fontLabel };
            txtLinkPdf = new TextBox() { Location = new Point(160, 102), Width = 600, Font = fontTextBox };

            btnSimpan = new Button()
            {
                Text = "Submit Proposal",
                Location = new Point(160, 142),
                Width = 150,
                Height = 35,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(76, 209, 55),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSimpan.Click += BtnSimpan_Click;

            btnKembali = new Button()
            {
                Text = "← Kembali",
                Location = new Point(325, 142),
                Width = 120,
                Height = 35,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                BackColor = Color.FromArgb(210, 218, 226),
                ForeColor = Color.FromArgb(47, 54, 64),
                FlatStyle = FlatStyle.Flat
            };
            btnKembali.Click += BtnKembali_Click;

            lblDaftar = new Label()
            {
                // Menampilkan nama asli dari user yang login
                Text = $"Riwayat Proposal Anda ({_currentUser.Username}):",
                Location = new Point(20, 195),
                Width = 400,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            gridProposal = new DataGridView()
            {
                Location = new Point(20, 225),
                Size = new Size(760, 250),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false
            };

            this.Controls.AddRange(new Control[] {
                lblHeader, lblJudul, txtJudul, lblLink, txtLinkPdf, btnSimpan, btnKembali, lblDaftar, gridProposal
            });
        }

        private void LoadDataGrid()
        {
            try
            {
                // Perbaikan: Menggunakan nama method yang benar (GetAllData)
                List<Proposal> semuaProposal = _repository.GetAllData();

                // Perbaikan: Filter menggunakan _currentUser.Username
                List<Proposal> proposalDosenSaya = semuaProposal.FindAll(
                    p => p.Pengaju != null && p.Pengaju.Equals(_currentUser.Username, StringComparison.OrdinalIgnoreCase)
                );

                gridProposal.DataSource = null;
                gridProposal.DataSource = proposalDosenSaya;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat histori: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                // Secure Coding: Trim input pengguna
                string judul = txtJudul.Text.Trim();
                string link = txtLinkPdf.Text.Trim();

                if (string.IsNullOrWhiteSpace(judul) || string.IsNullOrWhiteSpace(link))
                {
                    MessageBox.Show("Judul Proposal dan Link PDF wajib diisi!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Menggunakan model Proposal yang benar
                Proposal dataBaru = new Proposal
                {
                    IdProposal = "PROP-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Pengaju = _currentUser.Username,
                    Judul = judul,
                    LinkPdf = link,
                    TanggalSubmisi = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    StatusPenerimaan = "PENDING",
                    CatatanReview = null,
                    TanggalPresentasi = null
                };

                // Perbaikan: Menggunakan nama method yang benar (SaveData)
                _repository.SaveData(dataBaru);

                MessageBox.Show("Proposal berhasil diajukan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtJudul.Clear();
                txtLinkPdf.Clear();
                LoadDataGrid(); // Refresh tabel setelah insert
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan sistem: {ex.Message}", "Error Penyimpanan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}