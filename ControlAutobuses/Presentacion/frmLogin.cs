using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ControlAutobuses.Presentacion
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            this.Text = "Sistema de Control de Autobuses - Login";
            this.Size = new Size(400, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(236, 240, 241);
            ConfigureControls();
        }

        private void ConfigureControls()
        {
            // Panel contenedor
            Panel panel = new Panel();
            panel.BackColor = Color.White;
            panel.Size = new Size(350, 250);
            panel.Location = new Point((this.ClientSize.Width - panel.Width) / 2, 50);
            panel.BorderStyle = BorderStyle.None;
            this.Controls.Add(panel);

            // Logo/título
            Label lblTitulo = new Label();
            lblTitulo.Text = "🚌 Control de Autobuses";
            lblTitulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point((panel.Width - lblTitulo.Width) / 2, 20);
            panel.Controls.Add(lblTitulo);

            // Campo de usuario
            Label lblUsuario = new Label();
            lblUsuario.Text = "Usuario:";
            lblUsuario.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblUsuario.ForeColor = Color.FromArgb(44, 62, 80);
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(25, 70);
            panel.Controls.Add(lblUsuario);

            TextBox txtUsuario = new TextBox();
            txtUsuario.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            txtUsuario.BorderStyle = BorderStyle.FixedSingle;
            txtUsuario.Size = new Size(300, 30);
            txtUsuario.Location = new Point(25, 95);
            panel.Controls.Add(txtUsuario);

            // Campo de contraseña
            Label lblPassword = new Label();
            lblPassword.Text = "Contraseña:";
            lblPassword.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblPassword.ForeColor = Color.FromArgb(44, 62, 80);
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(25, 130);
            panel.Controls.Add(lblPassword);

            TextBox txtPassword = new TextBox();
            txtPassword.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Size = new Size(300, 30);
            txtPassword.Location = new Point(25, 155);
            txtPassword.UseSystemPasswordChar = true;
            panel.Controls.Add(txtPassword);

            // Botones
            Button btnLogin = new Button();
            btnLogin.Text = "Iniciar Sesión";
            btnLogin.BackColor = Color.FromArgb(44, 62, 80);
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Size = new Size(300, 35);
            btnLogin.Location = new Point(25, 200);
            btnLogin.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnLogin.Click += BtnLogin_Click;
            panel.Controls.Add(btnLogin);

            Button btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.BackColor = Color.FromArgb(149, 165, 166);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Size = new Size(300, 35);
            btnCancelar.Location = new Point(25, 245);
            btnCancelar.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnCancelar.Click += BtnCancelar_Click;
            panel.Controls.Add(btnCancelar);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            // Lógica de autenticación
            MessageBox.Show("Login exitoso");
            this.Hide();
            frmPrincipal principal = new frmPrincipal();
            principal.Show();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
