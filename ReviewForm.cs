using ModulReviewPenilaian;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace DeLFINA_GUI
{
    public partial class ReviewForm : Form
    {
        private readonly string _filePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\proposals.json"));

        private List<ProposalReview> _proposals = new List<ProposalReview>();

        private ReviewStateMachine _stateMachine = new ReviewStateMachine();

        public ReviewForm()
        {
            InitializeComponent();
            LoadData();
        }

        // Memisahkan fungsi Load Data agar bisa dipanggil berulang
        private void LoadData()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string json = File.ReadAllText(_filePath);
                    _proposals = JsonSerializer.Deserialize<List<ProposalReview>>(json) ?? new List<ProposalReview>();

                    // Refresh DataGridView
                    dgvProposals.DataSource = null;
                    dgvProposals.DataSource = _proposals;
                }
            }
            catch (Exception ex)
            {
                // Mencegah program crash jika format JSON rusak
                MessageBox.Show($"Gagal memuat data: {ex.Message}", "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProcessAction(AksiReview aksi)
        {
            // Validasi 1: Memastikan ada baris yang dipilih
            if (dgvProposals.CurrentRow == null)
            {
                MessageBox.Show("Silakan klik salah satu proposal di tabel terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ambil data proposal yang sedang di-klik
            var selectedProposal = (ProposalReview)dgvProposals.CurrentRow.DataBoundItem;

            // Validasi 2: Sanitasi input catatan, mencegah XSS atau input spasi kosong
            string catatan = txtCatatan.Text.Trim();
            if (string.IsNullOrWhiteSpace(catatan))
            {
                MessageBox.Show("Catatan review wajib diisi sebelum mengubah status!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // EKSEKUSI TEKNIK KONSTRUKSI (Automata & Table-Driven)
                StatusProposal statusLama = Enum.Parse<StatusProposal>(selectedProposal.StatusPenerimaan);

                StatusProposal statusBaru = _stateMachine.GetNextState(statusLama, aksi);

                selectedProposal.StatusPenerimaan = statusBaru.ToString();
                selectedProposal.CatatanReview = catatan;

                // Simpan perubahan kembali ke file JSON
                string updatedJson = JsonSerializer.Serialize(_proposals, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, updatedJson);

                MessageBox.Show($"Berhasil! Status proposal diubah menjadi: {selectedProposal.StatusPenerimaan}", "Sukses",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadData();
                txtCatatan.Clear();
            }
            catch (ArgumentException ex)
            {
                // Menangkap lemparan error transisi ilegal dari State Machine Automata
                MessageBox.Show(ex.Message, "Validasi Sistem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // Jaring pengaman untuk menangkap error tak terduga lainnya
                MessageBox.Show($"Terjadi kesalahan saat memproses data: {ex.Message}", "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // EVENT HANDLER
        private void btnTerima_Click(object sender, EventArgs e) => ProcessAction(AksiReview.TERIMA);

        private void btnTolak_Click(object sender, EventArgs e) => ProcessAction(AksiReview.TOLAK);

        private void btnRevisi_Click(object sender, EventArgs e) => ProcessAction(AksiReview.MINTA_REVISI);

        // Event Handler untuk tombol kembali, menutup form saat diklik (untuk sementara)
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
