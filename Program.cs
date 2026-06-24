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

            // Inisialisasi API Otentikasi (Database lokal)
            string dbProposals = "proposals.json";
            string dbUsers = "users.json";

            IAuthApi authService = new JsonAuthService(dbUsers);

            // Dependency Injection
            Application.Run(new FormLogin(authService));
            Application.Run(new ReviewForm());
        }
    }
}