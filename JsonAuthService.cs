using System.Text.Json;

namespace DeLFINA_GUI
{
    // API (Internal Contract)
    public class UserAccount
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Admin" atau "Dosen"
    }

    public interface IAuthApi
    {
        UserAccount Login(string username, string password);
    }

    public class JsonAuthService : IAuthApi
    {
        private readonly string _dbPath;

        public JsonAuthService(string dbPath)
        {
            // DbC: Pre-condition (Memastikan path tidak null/kosong)
            if (string.IsNullOrWhiteSpace(dbPath))
                throw new ArgumentException("Path database tidak boleh kosong.");
            _dbPath = dbPath;
        }

        public UserAccount Login(string username, string password)
        {
            // DbC: Pre-condition
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Username dan Password harus diisi.");

            // DbC: Jika file hilang, lempar Exception, JANGAN return null!
            if (!File.Exists(_dbPath))
                throw new FileNotFoundException($"File database '{_dbPath}' tidak ditemukan! Pastikan properties file sudah diatur ke 'Copy if newer'.");

            try
            {
                string jsonString = File.ReadAllText(_dbPath);
                var users = JsonSerializer.Deserialize<List<UserAccount>>(jsonString);

                if (users != null)
                {
                    foreach (var user in users)
                    {
                        // Pengecekan case-sensitive dan pembersihan spasi berlebih
                        if (user.Username.Trim() == username.Trim() && user.Password.Trim() == password.Trim())
                            return user; // Login berhasil
                    }
                }
            }
            catch (JsonException)
            {
                throw new InvalidOperationException("Format data pada users.json rusak atau tidak valid.");
            }

            return null; // Jika sampai di sini, barulah benar-benar karena salah password
        }
    }
}