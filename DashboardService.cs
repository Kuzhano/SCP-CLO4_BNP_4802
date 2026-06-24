namespace DeLFINA_GUI
{
    public class DashboardService
    {
        private readonly JsonRepository<Proposal> _repository;
        private readonly AppConfig _config;

        public DashboardService(JsonRepository<Proposal> repository, AppConfig config)
        {
            // DbC: Invariant Checks
            _repository = repository ?? throw new ArgumentNullException(nameof(repository), "Repository tidak boleh null.");
            _config = config ?? throw new ArgumentNullException(nameof(config), "Konfigurasi tidak boleh null.");
        }

        /// Mengambil semua proposal dari repository.
        /// DbC: Post-condition — tidak pernah return null.
        public List<Proposal> GetSemuaProposal()
        {
            return _repository.GetAllData() ?? new List<Proposal>();
        }

        /// Mengambil proposal berstatus DITERIMA, dibatasi oleh DashboardMaxBaris dari config.
        /// Teknik: Runtime Config — limit diambil dari AppConfig, bukan hardcoded.
        public List<Proposal> GetProposalDiterima()
        {
            var semuaProposal = GetSemuaProposal();
            var result = new List<Proposal>();

            foreach (var p in semuaProposal)
            {
                if (p.StatusPenerimaan == "DITERIMA")
                {
                    if (result.Count >= _config.DashboardMaxBaris) break; // Limit dari Runtime Config
                    result.Add(p);
                }
            }

            return result;
        }

        public string GetJudulDashboard() => _config.DashboardJudul;
        public int GetMaxBaris() => _config.DashboardMaxBaris;
    }
}
