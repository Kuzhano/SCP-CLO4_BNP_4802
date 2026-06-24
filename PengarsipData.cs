using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DeLFINA_GUI
{
    // TEKNIK 1: Antarmuka API
    public interface IArchivingServiceAPI<T> where T : class
    {
        List<T> SaringProposalSelesai(List<T> daftarProposal);
        void EksporDataBerdasarkanKonfigurasi(List<T> dataTerpilih, string pathFolder, string namaFileTanpaEkstensi, string formatKonfigurasi, string peranPengguna);
    }

    // TEKNIK 2: Parameterization / Generics
    public class PengarsipDataEkspor<T> : IArchivingServiceAPI<T> where T : Proposal
    {
        public List<T> SaringProposalSelesai(List<T> daftarProposal)
        {
            if (daftarProposal == null) throw new ArgumentNullException(nameof(daftarProposal), "Data tidak boleh null!");

            // Filter hanya proposal yang statusnya DITERIMA
            return daftarProposal.FindAll(p =>
                p.StatusPenerimaan != null &&
                p.StatusPenerimaan.Equals("DITERIMA", StringComparison.OrdinalIgnoreCase));
        }

        public void EksporDataBerdasarkanKonfigurasi(List<T> dataTerpilih, string pathFolder, string namaFileTanpaEkstensi, string formatKonfigurasi, string peranPengguna)
        {
            // DbC: Otorisasi Keamanan
            if (!peranPengguna.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException($"Akses Ditolak! Peran '{peranPengguna}' tidak diizinkan mengekspor data.");
            }

            if (dataTerpilih == null || dataTerpilih.Count == 0)
            {
                throw new InvalidOperationException("Tidak ada data proposal berstatus 'DITERIMA' untuk diekspor.");
            }

            StringBuilder kontenFile = new StringBuilder();
            string namaFileLengkap = $"{namaFileTanpaEkstensi}.{formatKonfigurasi.ToLower()}";
            string pathLengkap = Path.Combine(pathFolder, namaFileLengkap);

            if (formatKonfigurasi.Equals("CSV", StringComparison.OrdinalIgnoreCase))
            {
                kontenFile.AppendLine("ID_Proposal,Pengaju,Judul,Status_Penerimaan,Tgl_Submisi,Tgl_Presentasi");
                foreach (var item in dataTerpilih)
                {
                    kontenFile.AppendLine($@"""{item.IdProposal}"",""{item.Pengaju}"",""{item.Judul}"",""{item.StatusPenerimaan}"",""{item.TanggalSubmisi}"",""{item.TanggalPresentasi}""");
                }
            }
            else if (formatKonfigurasi.Equals("TXT", StringComparison.OrdinalIgnoreCase))
            {
                kontenFile.AppendLine("=== REKAPITULASI ARSIP PROPOSAL ===");
                foreach (var item in dataTerpilih)
                {
                    kontenFile.AppendLine($"ID: {item.IdProposal} | Pengaju: {item.Pengaju} | Judul: {item.Judul} | Status: {item.StatusPenerimaan}");
                }
            }
            else
            {
                throw new ArgumentException($"Format konfigurasi '{formatKonfigurasi}' tidak didukung.");
            }

            File.WriteAllText(pathLengkap, kontenFile.ToString());
        }
    }
}