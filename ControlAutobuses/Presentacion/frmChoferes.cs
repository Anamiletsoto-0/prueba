using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ControlAutobuses.Presentacion
{
    public partial class frmChoferes : Form
    {
        public frmChoferes()
        {
            InitializeComponent();
            this.Text = "Gestión de Choferes";
            this.Size = new Size(900, 600);
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
            lblTitulo.Text = "👨‍💼 Registro de Choferes";
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

            // Formulario de registro
            int topPosition = 60;

            // Campos del formulario
            string[] labels = { "Nombre:", "Apellido:", "Cédula:", "Fecha de Nacimiento:" };
            for (int i = 0; i < labels.Length; i++)
            {
                Label lbl = new Label();
                lbl.Text = labels[i];
                lbl.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                lbl.ForeColor = Color.FromArgb(44, 62, 80);
                lbl.AutoSize = true;
                lbl.Location = new Point(20, topPosition);
                panel.Controls.Add(lbl);

                Control inputControl;
                if (i == 3) // Fecha de nacimiento
                {
                    inputControl = new DateTimePicker();
                    ((DateTimePicker)inputControl).Format = DateTimePickerFormat.Short;
                }
                else
                {
                    inputControl = new TextBox();
                    ((TextBox)inputControl).BorderStyle = BorderStyle.FixedSingle;
                }

                inputControl.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                inputControl.Size = new Size(300, 30);
                inputControl.Location = new Point(20, topPosition + 25);
                panel.Controls.Add(inputControl);

                topPosition += 70;
            }

            // Botones
            int buttonTop = topPosition + 20;
            Button btnGuardar = CreateButton("Guardar", Color.FromArgb(44, 62, 80), 20, buttonTop);
            btnGuardar.Click += BtnGuardar_Click;
            panel.Controls.Add(btnGuardar);

            Button btnLimpiar = CreateButton("Limpiar", Color.FromArgb(149, 165, 166), 120, buttonTop);
            btnLimpiar.Click += BtnLimpiar_Click;
            panel.Controls.Add(btnLimpiar);

            Button btnBuscar = CreateButton("Buscar", Color.FromArgb(39, 174, 96), 220, buttonTop);
            btnBuscar.Click += BtnBuscar_Click;
            panel.Controls.Add(btnBuscar);

            // Grid de choferes
            buttonTop += 60;
            Label lblGrid = new Label();
            lblGrid.Text = "Choferes Registrados";
            lblGrid.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblGrid.ForeColor = Color.FromArgb(44, 62, 80);
            lblGrid.AutoSize = true;
            lblGrid.Location = new Point(20, buttonTop);
            panel.Controls.Add(lblGrid);

            buttonTop += 30;
            DataGridView grid = new DataGridView();
            grid.Size = new Size(panel.Width - 60, 250);
            grid.Location = new Point(20, buttonTop);
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.RowHeadersVisible = false;
            grid.AllowUserToAddRows = false;
            grid.ReadOnly = true;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Columnas
            grid.Columns.Add("Nombre", "Nombre");
            grid.Columns.Add("Apellido", "Apellido");
            grid.Columns.Add("Cedula", "Cédula");
            grid.Columns.Add("FechaNacimiento", "Fecha Nac.");
            grid.Columns.Add("Acciones", "Acciones");

            // Datos de ejemplo
            grid.Rows.Add("Juan", "Pérez", "001-1234567-8", "15/03/1985", "Editar | Eliminar");
            grid.Rows.Add("María", "González", "002-7654321-1", "22/07/1990", "Editar | Eliminar");

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
            btn.Size = new Size(90, 35);
            btn.Location = new Point(x, y);
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            return btn;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chofer guardado correctamente");
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Formulario limpiado");
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Buscando choferes...");
        }
    }
}
