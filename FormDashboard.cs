namespace DeLFINA_GUI
{
    /// FormDashboard — Presentation Layer untuk Modul 4: Pemantauan & Dashboard.
    /// Teknik Konstruksi: Code Reuse & Runtime Config.
    /// Clean Code: Form ini hanya merender state. Semua logika bisnis ada di DashboardService.
    public class FormDashboard : Form
    {
        // --- Service (Business Logic Layer) ---
        private readonly DashboardService _dashboardService;

        // --- UI Controls ---
        private Label lblJudul;
        private Label lblUpdateTerkini;

        private Label lblSemuaProposal;
        private DataGridView dgvSemuaProposal;

        private Label lblProgres;
        private DataGridView dgvProgres;

        private Label lblInfoLimit;
        private Button btnRefresh;
        private Button btnTutup;

        // Constructor
        public FormDashboard(JsonRepository<Proposal> repository, AppConfig config)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            if (config == null) throw new ArgumentNullException(nameof(config));

            _dashboardService = new DashboardService(repository, config);

            SetupUI();
            MuatData();
        }

        // InitializeComponent — layout seluruh form
        private void SetupUI()
        {
            this.Text = "DeLFINA — Dashboard Pemantauan Proposal";
            this.Size = new Size(900, 620);
            this.MinimumSize = new Size(800, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 9F);

            // Gunakan TableLayoutPanel agar layout rapi dan responsif
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 7,
                ColumnCount = 1,
                Padding = new Padding(12),
                AutoSize = false
            };

            // Baris: Judul, update, label tabel1, grid1, label tabel2, grid2, panel bawah
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));        // 0: judul
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));        // 1: update terkini
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));        // 2: label semua proposal
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));   // 3: dgv semua proposal
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));        // 4: label progres
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));   // 5: dgv progres
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));        // 6: panel bawah

            // --- Row 0: Judul ---
            lblJudul = new Label
            {
                Text = _dashboardService.GetJudulDashboard(),
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 2)
            };

            // --- Row 1: Update Terkini ---
            lblUpdateTerkini = new Label
            {
                Text = $"Update Terkini: {DateTime.Now:dd MMM yyyy HH:mm}",
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 8)
            };

            // --- Row 2: Label Tabel 1 ---
            lblSemuaProposal = new Label
            {
                Text = "Semua Proposal",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                AutoSize = true,
                Margin = new Padding(0, 4, 0, 2)
            };

            // --- Row 3: DataGridView Semua Proposal ---
            dgvSemuaProposal = BuatDataGridView();

            // --- Row 4: Label Tabel 2 ---
            lblProgres = new Label
            {
                Text = "Tabel Progres (Proposal Diterima & Menunggu Presentasi)",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                AutoSize = true,
                Margin = new Padding(0, 8, 0, 2)
            };

            // --- Row 5: DataGridView Progres ---
            dgvProgres = BuatDataGridView();

            // --- Row 6: Panel bawah (info + tombol) ---
            lblInfoLimit = new Label
            {
                Text = string.Empty,
                AutoSize = true,
                Dock = DockStyle.Left
            };

            btnRefresh = new Button
            {
                Text = "Refresh",
                Width = 90,
                Height = 30,
                Anchor = AnchorStyles.Right
            };
            btnRefresh.Click += (s, e) => MuatData();

            btnTutup = new Button
            {
                Text = "Tutup",
                Width = 90,
                Height = 30,
                Anchor = AnchorStyles.Right
            };
            btnTutup.Click += (s, e) => this.Close();

            var bottomPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                AutoSize = true,
                Margin = new Padding(0, 6, 0, 0)
            };
            bottomPanel.Controls.Add(btnTutup);
            bottomPanel.Controls.Add(btnRefresh);
            bottomPanel.Controls.Add(lblInfoLimit);

            // Tambahkan semua ke layout
            mainLayout.Controls.Add(lblJudul, 0, 0);
            mainLayout.Controls.Add(lblUpdateTerkini, 0, 1);
            mainLayout.Controls.Add(lblSemuaProposal, 0, 2);
            mainLayout.Controls.Add(dgvSemuaProposal, 0, 3);
            mainLayout.Controls.Add(lblProgres, 0, 4);
            mainLayout.Controls.Add(dgvProgres, 0, 5);
            mainLayout.Controls.Add(bottomPanel, 0, 6);

            this.Controls.Add(mainLayout);
        }

        // Teknik Code Reuse: satu fungsi pembuatan DataGridView dipakai dua kali
        private DataGridView BuatDataGridView()
        {
            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                BackgroundColor = SystemColors.Window,
                BorderStyle = BorderStyle.Fixed3D,
                RowHeadersVisible = false
            };

            // Definisi kolom sesuai DashboardHelper.CetakHeader() dari CLO2
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId",         HeaderText = "ID Proposal",        FillWeight = 15 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPengaju",    HeaderText = "Pengaju",            FillWeight = 18 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colJudul",      HeaderText = "Judul",              FillWeight = 35 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStatus",     HeaderText = "Status",             FillWeight = 14 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTglPresentasi", HeaderText = "Tgl Presentasi",  FillWeight = 18 });

            return dgv;
        }

        // MuatData — mengisi kedua DataGridView dari service
        private void MuatData()
        {
            // Update timestamp (seperti Console.WriteLine di CLO2)
            lblUpdateTerkini.Text = $"Update Terkini: {DateTime.Now:dd MMM yyyy HH:mm}";

            // --- Tabel 1: Semua Proposal ---
            IsiDataGridView(dgvSemuaProposal, _dashboardService.GetSemuaProposal());

            // --- Tabel 2: Proposal Diterima (dengan limit dari Runtime Config) ---
            var proposalDiterima = _dashboardService.GetProposalDiterima();
            IsiDataGridView(dgvProgres, proposalDiterima);

            // Info limit (padanan Console.WriteLine [Info] di CLO2)
            lblInfoLimit.Text =
                $"Total Proposal Diterima Ditampilkan: {proposalDiterima.Count}  " +
                $"(Limit: {_dashboardService.GetMaxBaris()} baris)";
        }

        // Teknik Code Reuse: satu prosedur pengisi DataGridView dipakai dua kali,
        private void IsiDataGridView(DataGridView dgv, List<Proposal> proposals)
        {
            dgv.Rows.Clear();

            if (proposals == null || proposals.Count == 0)
            {
                dgv.Rows.Add("-", "-", "Belum ada data proposal.", "-", "-");
                return;
            }

            foreach (var p in proposals)
            {
                // DbC: Null-safe check — padanan kondisi if (p == null) di DashboardHelper
                if (p == null) continue;

                string id     = p.IdProposal ?? "-";
                string pengaju = p.Pengaju ?? "-";
                // Judul panjang ditruncate — Code Reuse dari DashboardHelper.CetakBaris()
                string judul  = (p.Judul?.Length > 50)
                                    ? p.Judul.Substring(0, 47) + "..."
                                    : (p.Judul ?? "-");
                string status = p.StatusPenerimaan ?? "-";
                string tgl    = string.IsNullOrEmpty(p.TanggalPresentasi)
                                    ? "Belum Dijadwalkan"
                                    : p.TanggalPresentasi;

                dgv.Rows.Add(id, pengaju, judul, status, tgl);
            }
        }
    }
}
