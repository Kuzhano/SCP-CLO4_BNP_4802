using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DeLFINA_GUI
{
    public class AppConfig
    {
        public string ProposalFilePath { get; set; } = "proposals.json";

        public static AppConfig LoadConfig()
        {
            string configPath = "appconfig.json";
            if (File.Exists(configPath))
            {
                try
                {
                    string jsonString = File.ReadAllText(configPath);
                    return JsonSerializer.Deserialize<AppConfig>(jsonString) ?? new AppConfig();
                }
                catch { /* Fallback ke default jika gagal membaca */ }
            }
            return new AppConfig();
        }
    }

    // Generic Repository untuk penanganan file JSON secara dinamis
    public class JsonRepository<T> where T : class
    {
        private readonly string _filePath;

        public JsonRepository(string filePath)
        {
            _filePath = filePath;
        }

        // Membaca seluruh data dari file JSON
        public List<T> GetAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<T>();
            }

            try
            {
                string jsonString = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<T>>(jsonString) ?? new List<T>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal membaca repositori: {ex.Message}");
            }
        }

        // Menambahkan data baru dan menyimpannya kembali ke JSON
        public void Add(T entity)
        {
            List<T> data = GetAll();
            data.Add(entity);

            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(data, options);
                File.WriteAllText(_filePath, jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal menyimpan data ke repositori: {ex.Message}");
            }
        }
    }
}