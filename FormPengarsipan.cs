using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DeLFINA_GUI
{
    public partial class FormPengarsipan : Form
    {
        private readonly JsonRepository<Proposal> _repository;
        private readonly UserAccount _currentUser;
        private readonly IArchivingServiceAPI<Proposal> _exportService;
        private List<Proposal> _dataSiapEkspor;

        // Dependency Injection dari FormMenu (Modul 1)
        public FormPengarsipan(JsonRepository<Proposal> repository, UserAccount currentUser)
        {
            InitializeComponent();

            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
            _exportService = new PengarsipDataEkspor<Proposal>();
            _dataSiapEkspor = new List<Proposal>();
        }

        // Event saat form dimuat (Didaftarkan oleh Designer teman Anda sebagai GUI_Pengarsipan_Load)
        private void GUI_Pengarsipan_Load(object sender, EventArgs e)
        {
            // 1. SECURE CODING: Sembunyikan ComboBox Role agar tidak bisa di-bypass
            comboBox1.Visible = false;

            // 2. PENYESUAIAN UI: Karena kita pakai Single Source of Truth (1 Model Proposal),
            // kita ubah teks Tab 1 dan hapus Tab 2.
            tabPage1.Text = "Semua Arsip Proposal (DITERIMA)";
            if (dataGridView_1.TabPages.Contains(tabPage2))
            {
                dataGridView_1.TabPages.Remove(tabPage2);
            }

            // Ganti teks tombol
            btnExportKonten.Text = "Ekspor Data ke CSV";

            // Rapikan bentuk tabel
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            MuatDataSelesai();
        }

        private void MuatDataSelesai()
        {
            try
            {
                // Ambil data universal dari JSON
                var semuaData = _repository.GetAllData();

                // Gunakan API Filter untuk mengambil status "DITERIMA" saja
                _dataSiapEkspor = _exportService.SaringProposalSelesai(semuaData);

                // Bind ke tabel
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = _dataSiapEkspor;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat data arsip: {ex.Message}", "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler tombol ekspor
        private void btnExportKonten_Click(object sender, EventArgs e)
        {
            try
            {
                // Tampilkan Folder Browser Dialog untuk UI/UX yang lebih baik
                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {
                    fbd.Description = "Pilih folder untuk menyimpan file arsip";

                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        string pathFolder = fbd.SelectedPath;
                        string namaFile = $"Arsip_Proposal_{DateTime.Now:yyyyMMdd_HHmm}";

                        // Eksekusi ekspor
                        _exportService.EksporDataBerdasarkanKonfigurasi(_dataSiapEkspor, pathFolder, namaFile, "CSV", _currentUser.Role);

                        MessageBox.Show($"Ekspor Berhasil!\nFile tersimpan di:\n{pathFolder}\\{namaFile}.csv", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ekspor Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ==========================================================
        // DUMMY EVENT HANDLERS (WAJIB ADA AGAR DESIGNER TIDAK ERROR)
        // ==========================================================

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Dibiarkan kosong karena komponen ini disembunyikan
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Dibiarkan kosong karena tabel di-set ke ReadOnly
        }
    }
}