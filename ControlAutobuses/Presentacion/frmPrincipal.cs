using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ControlAutobuses.Presentacion
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
            this.Text = "Sistema de Control de Autobuses";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(236, 240, 241);
            ConfigureMenu();
            ConfigureHeader();
            ConfigureDashboard();
        }

        private void ConfigureHeader()
        {
            // Panel de header
            Panel header = new Panel();
            header.BackColor = Color.FromArgb(44, 62, 80);
            header.Dock = DockStyle.Top;
            header.Height = 60;
            this.Controls.Add(header);

            // Título
            Label lblTitulo = new Label();
            lblTitulo.Text = "🚌 Sistema de Control de Autobuses";
            lblTitulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(20, 15);
            header.Controls.Add(lblTitulo);

            // Info de usuario
            Label lblUsuario = new Label();
            lblUsuario.Text = "Administrador | Cerrar Sesión";
            lblUsuario.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblUsuario.ForeColor = Color.White;
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(header.Width - lblUsuario.Width - 20, 20);
            header.Controls.Add(lblUsuario);

            // Barra de estado
            Panel statusBar = new Panel();
            statusBar.BackColor = Color.White;
            statusBar.Dock = DockStyle.Top;
            statusBar.Height = 40;
            statusBar.Top = header.Height;
            statusBar.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(statusBar);

            Label lblBienvenido = new Label();
            lblBienvenido.Text = "Bienvenido, Admin";
            lblBienvenido.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblBienvenido.AutoSize = true;
            lblBienvenido.Location = new Point(20, 10);
            statusBar.Controls.Add(lblBienvenido);

            Label lblFecha = new Label();
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy | HH:mm");
            lblFecha.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(statusBar.Width - lblFecha.Width - 20, 10);
            statusBar.Controls.Add(lblFecha);
        }

        private void ConfigureMenu()
        {
            // Panel de menú
            Panel menuPanel = new Panel();
            menuPanel.BackColor = Color.FromArgb(52, 73, 94);
            menuPanel.Dock = DockStyle.Left;
            menuPanel.Width = 200;
            this.Controls.Add(menuPanel);

            // Elementos del menú
            string[] menuItems = { "Choferes", "Autobuses", "Rutas", "Asignaciones", "Reportes" };
            int topPosition = 20;

            foreach (string item in menuItems)
            {
                Button btnMenuItem = new Button();
                btnMenuItem.Text = item;
                btnMenuItem.BackColor = Color.Transparent;
                btnMenuItem.ForeColor = Color.White;
                btnMenuItem.FlatStyle = FlatStyle.Flat;
                btnMenuItem.FlatAppearance.BorderSize = 0;
                btnMenuItem.FlatAppearance.MouseOverBackColor = Color.FromArgb(44, 62, 80);
                btnMenuItem.TextAlign = ContentAlignment.MiddleLeft;
                btnMenuItem.Size = new Size(200, 40);
                btnMenuItem.Location = new Point(0, topPosition);
                btnMenuItem.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                btnMenuItem.Click += MenuItem_Click;
                menuPanel.Controls.Add(btnMenuItem);

                topPosition += 45;
            }
        }

        private void ConfigureDashboard()
        {
            // Panel de contenido
            Panel contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = Color.FromArgb(236, 240, 241);
            contentPanel.Padding = new Padding(20);
            this.Controls.Add(contentPanel);
            contentPanel.BringToFront();

            // Título del dashboard
            Label lblDashboard = new Label();
            lblDashboard.Text = "Panel Principal";
            lblDashboard.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblDashboard.ForeColor = Color.FromArgb(44, 62, 80);
            lblDashboard.AutoSize = true;
            lblDashboard.Location = new Point(20, 20);
            contentPanel.Controls.Add(lblDashboard);

            // Tarjetas de dashboard
            string[] cards = { "Choferes Registrados", "Autobuses Activos", "Rutas Configuradas" };
            string[] icons = { "👨‍💼", "🚌", "🗺️" };
            string[] values = { "24", "15", "8" };
            int leftPosition = 20;
            int topPosition = 70;

            for (int i = 0; i < cards.Length; i++)
            {
                Panel card = new Panel();
                card.BackColor = Color.White;
                card.Size = new Size(250, 150);
                card.Location = new Point(leftPosition, topPosition);
                card.BorderStyle = BorderStyle.None;
                contentPanel.Controls.Add(card);

                Label icon = new Label();
                icon.Text = icons[i];
                icon.Font = new Font("Segoe UI", 30, FontStyle.Regular);
                icon.AutoSize = true;
                icon.Location = new Point((card.Width - icon.Width) / 2, 20);
                card.Controls.Add(icon);

                Label title = new Label();
                title.Text = cards[i];
                title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                title.ForeColor = Color.FromArgb(44, 62, 80);
                title.AutoSize = true;
                title.Location = new Point((card.Width - title.Width) / 2, 80);
                card.Controls.Add(title);

                Label value = new Label();
                value.Text = values[i];
                value.Font = new Font("Segoe UI", 20, FontStyle.Bold);
                value.ForeColor = Color.FromArgb(39, 174, 96);
                value.AutoSize = true;
                value.Location = new Point((card.Width - value.Width) / 2, 105);
                card.Controls.Add(value);

                leftPosition += 270;
            }
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string formName = "frm" + clickedButton.Text;

            // Aquí implementarías la lógica para abrir el formulario correspondiente
            MessageBox.Show($"Abriendo: {formName}");
        }
    }
}

