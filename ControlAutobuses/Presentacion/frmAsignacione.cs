using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ControlAutobuses.Presentacion
{
    public partial class frmAsignaciones : Form
    {
        public frmAsignaciones()
        {
            InitializeComponent();
            this.Text = "Asignación de Rutas";
            this.Size = new Size(900, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(236, 240, 241);
            ConfigureForm();
        }

        private void ConfigureForm()
        {
            // Panel contenedor
            Panel panel = new Panel();
            panel.BackColor = Color.White;
            panel.Dock = DockStyle.Fill;
            panel.Padding = new Padding(20);
            this.Controls.Add(panel);

            // Título
            Label lblTitulo = new Label();
            lblTitulo.Text = "🔗 Asignación de Rutas";
            lblTitulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(0, 0);
            panel.Controls.Add(lblTitulo);

            // Línea divisoria
            Panel divider = new Panel();
            divider.BackColor = Color.FromArgb(44, 62, 80);
            divider.Height = 2;
            divider.Width = panel.Width - 40;
            divider.Location = new Point(0, 40);
            panel.Controls.Add(divider);

            // Panel de información
            Panel infoPanel = new Panel();
            infoPanel.BackColor = Color.FromArgb(249, 249, 249);
            infoPanel.BorderStyle = BorderStyle.None;
            infoPanel.Size = new Size(panel.Width - 60, 60);
            infoPanel.Location = new Point(0, 50);
            infoPanel.Padding = new Padding(10);
            panel.Controls.Add(infoPanel);

            Label lblInfo = new Label();
            lblInfo.Text = "Nota: Solo se muestran choferes, autobuses y rutas disponibles. " +
                           "Un chofer o autobús no puede tener múltiples asignaciones activas simultáneamente.";
            lblInfo.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            lblInfo.ForeColor = Color.FromArgb(44, 62, 80);
            lblInfo.AutoSize = false;
            lblInfo.Size = new Size(infoPanel.Width - 20, 40);
            infoPanel.Controls.Add(lblInfo);

            // Campos del formulario
            int topPosition = 120;
            string[] labels = { "Chofer:", "Autobús:", "Ruta:" };
            for (int i = 0; i < labels.Length; i++)
            {
                Label lbl = new Label();
                lbl.Text = labels[i];
                lbl.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                lbl.ForeColor = Color.FromArgb(44, 62, 80);
                lbl.AutoSize = true;
                lbl.Location = new Point(20, topPosition);
                panel.Controls.Add(lbl);

                ComboBox combo = new ComboBox();
                combo.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                combo.DropDownStyle = ComboBoxStyle.DropDownList;
                combo.Size = new Size(400, 30);
                combo.Location = new Point(20, topPosition + 25);

                // Datos de ejemplo
                if (i == 0) // Choferes
                {
                    combo.Items.Add("Seleccione un chofer");
                    combo.Items.Add("Juan Pérez (Disponible)");
                    combo.Items.Add("María González (Disponible)");
                }
                else if (i == 1) // Autobuses
                {
                    combo.Items.Add("Seleccione un autobús");
                    combo.Items.Add("Toyota Coaster - ABC-123 (Disponible)");
                    combo.Items.Add("Mercedes Sprinter - DEF-456 (Disponible)");
                }
                else // Rutas
                {
                    combo.Items.Add("Seleccione una ruta");
                    combo.Items.Add("Villa Mella (Disponible)");
                    combo.Items.Add("Sabana (Disponible)");
                    combo.Items.Add("La Charle (Disponible)");
                    combo.Items.Add("La Churchill (Ocupada)");
                }

                combo.SelectedIndex = 0;
                panel.Controls.Add(combo);

                topPosition += 70;
            }

            // Botones
            topPosition += 20;
            Button btnAsignar = CreateButton("Asignar", Color.FromArgb(44, 62, 80), 20, topPosition);
            btnAsignar.Click += BtnAsignar_Click;
            panel.Controls.Add(btnAsignar);

            Button btnLimpiar = CreateButton("Limpiar", Color.FromArgb(149, 165, 166), 120, topPosition);
            btnLimpiar.Click += BtnLimpiar_Click;
            panel.Controls.Add(btnLimpiar);

            Button btnVerAsignaciones = CreateButton("Ver Todas", Color.FromArgb(39, 174, 96), 220, topPosition);
            btnVerAsignaciones.Click += BtnVerAsignaciones_Click;
            panel.Controls.Add(btnVerAsignaciones);

            // Grid de asignaciones
            topPosition += 60;
            Label lblGrid = new Label();
            lblGrid.Text = "Asignaciones Activas";
            lblGrid.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblGrid.ForeColor = Color.FromArgb(44, 62, 80);
            lblGrid.AutoSize = true;
            lblGrid.Location = new Point(20, topPosition);
            panel.Controls.Add(lblGrid);

            topPosition += 30;
            DataGridView grid = new DataGridView();
            grid.Size = new Size(panel.Width - 60, 200);
            grid.Location = new Point(20, topPosition);
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.RowHeadersVisible = false;
            grid.AllowUserToAddRows = false;
            grid.ReadOnly = true;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Columnas
            grid.Columns.Add("Chofer", "Chofer");
            grid.Columns.Add("Autobus", "Autobús");
            grid.Columns.Add("Ruta", "Ruta");
            grid.Columns.Add("Fecha", "Fecha Asignación");
            grid.Columns.Add("Acciones", "Acciones");

            // Datos de ejemplo
            grid.Rows.Add("Carlos Rodríguez", "Hyundai County - GHI-789", "Puente Juan Carlos", "10/05/2023 08:30", "Finalizar");
            grid.Rows.Add("Ana Martínez", "Nissan Civilian - JKL-012", "La Churchill", "11/05/2023 09:15", "Finalizar");

            panel.Controls.Add(grid);
        }

        private Button CreateButton(string text, Color color, int x, int y)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Size = new Size(120, 35);
            btn.Location = new Point(x, y);
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            return btn;
        }

        private void BtnAsignar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Asignación realizada correctamente");
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Formulario limpiado");
        }

        private void BtnVerAsignaciones_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Mostrando todas las asignaciones...");
        }
    }
}
