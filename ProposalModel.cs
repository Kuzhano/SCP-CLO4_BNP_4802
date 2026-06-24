using System;
using System.Collections.Generic;
using System.Text.Json.Serialization; // Wajib ditambahkan

namespace Modul_Pengarsipan_dan_Ekspor_Data
{
    // Kelas Dasar yang menyesuaikan dengan field JSON
    public class ProposalDasar
    {
        [JsonPropertyName("IdProposal")]
        public string IdProposal { get; set; }

        [JsonPropertyName("Pengaju")]
        public string Pengaju { get; set; }

        [JsonPropertyName("Judul")]
        public string Judul { get; set; }

        [JsonPropertyName("LinkPdf")]
        public string LinkPdf { get; set; }

        [JsonPropertyName("TanggalSubmisi")]
        public string TanggalSubmisi { get; set; }

        [JsonPropertyName("StatusPenerimaan")]
        public string StatusPenerimaan { get; set; }

        [JsonPropertyName("CatatanReview")]
        public string CatatanReview { get; set; }

        [JsonPropertyName("TanggalPresentasi")]
        public string TanggalPresentasi { get; set; }
    }

    // Kelas untuk Produksi Konten (bisa ditambah field unik jika perlu)
    public class ProposalProduksiKonten : ProposalDasar
    {
        public ProposalProduksiKonten() : base() { }
    }

    // Kelas untuk Proposal HLE
    public class ProposalHLE : ProposalDasar
    {
        public ProposalHLE() : base() { }
    }

    // Wrapper agar bisa menangkap list dari file JSON
    public class DataWrapper
    {
        // Sesuaikan dengan kebutuhan jika JSON-mu adalah array langsung, 
        // ini hanya dipakai jika JSON memiliki objek pembungkus.
        public List<ProposalProduksiKonten> ProposalProduksiKonten { get; set; }
        public List<ProposalHLE> ProposalHLE { get; set; }
    }
}