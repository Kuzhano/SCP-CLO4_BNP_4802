using System;
using System.Windows.Forms;

namespace DeLFINA_GUI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // 1. Inisialisasi API Otentikasi (Database lokal pengguna)
            string dbUsers = "users.json";
            IAuthApi authService = new JsonAuthService(dbUsers);

            // 2. Entry Point Tunggal
            Application.Run(new FormLogin(authService));
        }
    }
}