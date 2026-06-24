using System.Text.Json;

namespace DeLFINA_GUI
{
    // Teknik: Generics (Parameterization)
    public class JsonRepository<T> where T : class
    {
        private readonly string _filePath;

        public JsonRepository(string filePath)
        {
            // DbC: Pre-condition
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Path file database tidak boleh kosong.");

            _filePath = filePath;
        }

        public List<T> GetAllData()
        {
            if (!File.Exists(_filePath)) return new List<T>();

            try
            {
                string jsonString = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<T>>(jsonString) ?? new List<T>();
            }
            catch (JsonException)
            {
                // DbC: Defensive Programming - cegah crash karena JSON invalid
                throw new InvalidOperationException($"Format JSON pada {_filePath} tidak valid.");
            }
        }

        public void SaveData(T newData)
        {
            // DbC: Pre-condition
            if (newData == null) throw new ArgumentNullException(nameof(newData), "Data tidak boleh null.");

            List<T> currentData = GetAllData();
            currentData.Add(newData);

            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_filePath, JsonSerializer.Serialize(currentData, options));
        }

        public void UpdateAll(List<T> allData)
        {
            // DbC: Pre-condition
            if (allData == null) throw new ArgumentNullException(nameof(allData), "Data tidak boleh null.");

            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_filePath, JsonSerializer.Serialize(allData, options));
        }
    }
}