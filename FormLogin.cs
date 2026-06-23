using DeLFINA_GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DeLFINA_GUI
{
    public partial class FormLogin : Form
    {
        private readonly IAuthApi _authService;

        public FormLogin(IAuthApi authService)
        {
            InitializeComponent();

            // Design by Contract (DbC): Mencegah Form dirender jika API Authentication mati/null
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));

            // Secure Coding: Masking karakter password agar tidak terlihat di layar
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Secure Coding: Sanitasi input dari spasi yang tidak disengaja (Trim)
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            try
            {
                // Memanggil Teknik API
                var user = _authService.Login(username, password);

                if (user != null)
                {
                    MessageBox.Show($"Otentikasi Berhasil. Selamat datang, {user.Role}!", "Login Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Transisi Automata: Berpindah State ke Menu Utama
                    FormMenu menuForm = new FormMenu(user);
                    menuForm.Show();
                    this.Hide(); // Sembunyikan Form Login
                }
                else
                {
                    MessageBox.Show("Username atau Password tidak ditemukan/salah.", "Otentikasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Menangkap error dari backend agar aplikasi tidak crash
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
