using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DeLFINA_GUI
{
    // =========================================================================
    // 1. ENTITY MODEL (Menyesuaikan Atribut Aturan JSON)
    // =========================================================================
    public class Pendaftaran
    {
        public string IdProposal { get; set; }
        public string Pengaju { get; set; }
        public string Judul { get; set; }
        public string LinkPdf { get; set; }
        public string TanggalSubmisi { get; set; }
        public string StatusPenerimaan { get; set; }
        public string CatatanReview { get; set; }
        public string TanggalPresentasi { get; set; }

        public Pendaftaran(string idProposal, string pengaju, string judul, string linkPdf, string tanggalSubmisi, string statusPenerimaan)
        {
            IdProposal = idProposal;
            Pengaju = pengaju;
            Judul = judul;
            LinkPdf = linkPdf;
            TanggalSubmisi = tanggalSubmisi;
            StatusPenerimaan = statusPenerimaan;
            CatatanReview = null;
            TanggalPresentasi = null;
        }
    }

    // =========================================================================
    // 2. GUI FORM & LOGIKA SYSTEM (PendaftaranService)
    // =========================================================================
    public class PendaftaranService : Form
    {
        private readonly JsonRepository<Pendaftaran> _repository;
        private string _currentDosen = "Budi Santoso"; 

        private TextBox txtJudul, txtLinkPdf;
        private DataGridView gridProposal;
        private Button btnSimpan, btnKembali;
        private Label lblHeader, lblJudul, lblLink, lblDaftar;

        public PendaftaranService()
        {
            AppConfig config = AppConfig.LoadConfig();
            _repository = new JsonRepository<Pendaftaran>(config.ProposalFilePath);
            InitializeComponent();
            LoadDataGrid();
        }

        private void InitializeComponent()
        {
            // Atur Properti Jendela Form
            this.Text = "DeLFINA - Modul 2: Pendaftaran Proposal";
            this.Size = new Size(820, 540);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 246, 250);

            // Font Style
            Font fontLabel = new Font("Segoe UI", 10, FontStyle.Regular);
            Font fontTextBox = new Font("Segoe UI", 10, FontStyle.Regular);

            // Header
            lblHeader = new Label()
            {
                Text = "DASHBOARD PENGAJUAN PROPOSAL DOSEN",
                Location = new Point(20, 15),
                Width = 760,
                Height = 30,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(47, 54, 64)
            };

            // Input: Judul Proposal
            lblJudul = new Label() { Text = "Judul Proposal", Location = new Point(20, 65), Width = 150, Font = fontLabel };
            txtJudul = new TextBox() { Location = new Point(160, 62), Width = 600, Font = fontTextBox };

            // Input: Link PDF
            lblLink = new Label() { Text = "Link Tautan PDF", Location = new Point(20, 105), Width = 150, Font = fontLabel };
            txtLinkPdf = new TextBox() { Location = new Point(160, 102), Width = 600, Font = fontTextBox };

            // Tombol Submit
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

            // Back button
            btnKembali = new Button()
            {
                Text = "← Kembali",
                Location = new Point(325, 142), // Posisi di sebelah tombol submit
                Width = 120,
                Height = 35,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                BackColor = Color.FromArgb(210, 218, 226),
                ForeColor = Color.FromArgb(47, 54, 64),
                FlatStyle = FlatStyle.Flat
            };
            btnKembali.Click += BtnKembali_Click;

            // Judul Tabel
            lblDaftar = new Label()
            {
                Text = $"Riwayat Proposal Anda ({_currentDosen}):",
                Location = new Point(20, 195),
                Width = 400,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            // Komponen Tabel
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

            // Menambahkan Komponen Ke Kontrol
            this.Controls.AddRange(new Control[] {
                lblHeader, lblJudul, txtJudul, lblLink, txtLinkPdf, btnSimpan, btnKembali, lblDaftar, gridProposal
            });
        }

        // Mengambil dan Menyaring Data Berdasarkan Akun Dosen
        private void LoadDataGrid()
        {
            try
            {
                List<Pendaftaran> semuaProposal = _repository.GetAll();

                // Melakukan filter pencarian: Hanya mengambil proposal milik dosen yang aktif log-in
                List<Pendaftaran> proposalDosenSaya = semuaProposal.FindAll(
                    p => p.Pengaju.Equals(_currentDosen, StringComparison.OrdinalIgnoreCase)
                );

                gridProposal.DataSource = null;
                gridProposal.DataSource = proposalDosenSaya;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat histori proposal: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Logika Event Handler saat Pengajuan Baru di-Submit
        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtJudul.Text) || string.IsNullOrWhiteSpace(txtLinkPdf.Text))
                {
                    MessageBox.Show("Judul Proposal dan Link PDF wajib diisi!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string idProposal = "PROP-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string tanggalSubmisi = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string statusPenerimaan = "PENDING"; // Default awal

                Pendaftaran dataBaru = new Pendaftaran(idProposal, _currentDosen, txtJudul.Text, txtLinkPdf.Text, tanggalSubmisi, statusPenerimaan);
                _repository.Add(dataBaru);

                MessageBox.Show("Proposal baru berhasil diajukan dan disimpan ke format JSON database!", "Sukses Submisi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtJudul.Clear();
                txtLinkPdf.Clear();
                LoadDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan sistem: {ex.Message}", "Error Penyimpanan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event Handler Back Button
        private void BtnKembali_Click(object sender, EventArgs e)
        {
            // Menutup form pendaftaran 
            // Jika dipanggil dari Modul 1, akan kembali ke Menu Utama.
            this.Close();
        }
    }
}