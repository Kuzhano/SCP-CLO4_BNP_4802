using System;
using System.Collections.Generic;
using System.Windows.Forms;
// Pastikan using DeLFINA_GUI; jika class Proposal ada di luar folder ini

namespace DeLFINA_GUI // Sesuaikan namespace
{
    public partial class FormReview: Form
    {
        // Menggunakan JsonRepository (Single Source of Truth) dari Modul 2
        private readonly JsonRepository<Proposal> _repository;
        private readonly ReviewStateMachine _stateMachine;
        private readonly UserAccount _currentUser;

        // Dependency Injection: Form menerima Repository dan User dari Modul 1
        public FormReview(JsonRepository<Proposal> repository, UserAccount loggedInAdmin)
        {
            InitializeComponent();

            // DbC: Memastikan komponen krusial tidak null
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _currentUser = loggedInAdmin ?? throw new ArgumentNullException(nameof(loggedInAdmin));

            _stateMachine = new ReviewStateMachine();

            // Setup DataGridView agar rapi
            dgvProposals.AutoGenerateColumns = true;
            dgvProposals.ReadOnly = true;
            dgvProposals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Menggunakan Repository bersama
                List<Proposal> semuaProposal = _repository.GetAllData();

                dgvProposals.DataSource = null;
                dgvProposals.DataSource = semuaProposal;

                // Menyembunyikan kolom LinkPdf agar tabel tidak terlalu lebar (Opsional)
                if (dgvProposals.Columns["LinkPdf"] != null) dgvProposals.Columns["LinkPdf"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat data: {ex.Message}", "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProcessAction(AksiReview aksi)
        {
            if (dgvProposals.CurrentRow == null)
            {
                MessageBox.Show("Silakan klik salah satu proposal di tabel terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Casting ke model Proposal universal
            var selectedProposal = (Proposal)dgvProposals.CurrentRow.DataBoundItem;

            string catatan = txtCatatan.Text.Trim();
            if (string.IsNullOrWhiteSpace(catatan))
            {
                MessageBox.Show("Catatan review wajib diisi sebelum mengubah status!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Parse status lama
                StatusProposal statusLama = ReviewStateMachine.ParseStatus(selectedProposal.StatusPenerimaan);

                // Automata: Dapatkan status baru
                StatusProposal statusBaru = _stateMachine.GetNextState(statusLama, aksi);

                selectedProposal.StatusPenerimaan = statusBaru.ToString();
                selectedProposal.CatatanReview = $"[{_currentUser.Username}] - {catatan}";

                List<Proposal> semuaData = _repository.GetAllData();
                int index = semuaData.FindIndex(p => p.IdProposal == selectedProposal.IdProposal);

                if (index != -1)
                {
                    semuaData[index] = selectedProposal;
                    _repository.UpdateAll(semuaData);
                }

                MessageBox.Show($"Berhasil! Status proposal diubah menjadi: {statusBaru}", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadData();
                txtCatatan.Clear();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Transisi Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTerima_Click(object sender, EventArgs e) => ProcessAction(AksiReview.TERIMA);
        private void btnTolak_Click(object sender, EventArgs e) => ProcessAction(AksiReview.TOLAK);
        private void btnRevisi_Click(object sender, EventArgs e) => ProcessAction(AksiReview.MINTA_REVISI);

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}