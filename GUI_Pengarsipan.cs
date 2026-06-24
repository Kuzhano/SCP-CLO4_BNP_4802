using Modul_Pengarsipan_dan_Ekspor_Data;
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
    public partial class GUI_Pengarsipan : Form
    {
        List<ProposalProduksiKonten> listProduksiKonten = new List<ProposalProduksiKonten>();
        List<ProposalHLE> listProposalHLE = new List<ProposalHLE>();
        public GUI_Pengarsipan()
        {
            InitializeComponent();
            btnExportHLE.Click += btnExportHLE_Click;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isAdmin = (comboBox1.Text == "Admin");
            btnExportKonten.Enabled = isAdmin;
            btnExportHLE.Enabled = isAdmin;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void GUI_Pengarsipan_Load(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count == 0)
            {
                comboBox1.Items.AddRange(new string[] { "Admin", "Dosen" });
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            }

            // Tambahkan baris ini agar data dibaca saat aplikasi terbuka
            MuatDataDariJson();
        }

        private void btnExportKonten_Click(object sender, EventArgs e)
        {
            try
            {
                var service = new PengarsipDataEkspor<ProposalProduksiKonten>();
                service.EksporDataBerdasarkanKonfigurasi(listProduksiKonten, "Laporan_Konten", "CSV", comboBox1.Text);
                MessageBox.Show("Ekspor Berhasil!");
            }
            catch (Exception ex) { MessageBox.Show("Gagal: " + ex.Message); }
        }
        private void btnExportHLE_Click(object sender, EventArgs e)
        {
            try
            {
                var service = new PengarsipDataEkspor<ProposalHLE>();
                service.EksporDataBerdasarkanKonfigurasi(listProposalHLE, "Laporan_HLE", "CSV", comboBox1.Text);
                MessageBox.Show("Ekspor HLE Berhasil!");
            }
            catch (Exception ex) { MessageBox.Show("Gagal: " + ex.Message); }
        }
        private void MuatDataDariJson()
        {
            try
            {
                string path = "proposals.json";
                if (System.IO.File.Exists(path))
                {
                    string json = System.IO.File.ReadAllText(path);
                    // JSON kamu adalah List<Proposal>, bukan DataWrapper
                    var data = JsonSerializer.Deserialize<List<ProposalProduksiKonten>>(json);

                    if (data != null)
                    {
                        listProduksiKonten = data;
                        dataGridView1.DataSource = listProduksiKonten;

                        // Jika ingin menampilkan data yang sama di tabel kedua
                        listProposalHLE = JsonSerializer.Deserialize<List<ProposalHLE>>(json);
                        dataGridView2.DataSource = listProposalHLE;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error JSON: " + ex.Message); }
        }
    }
}
