using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using ControlAutobuses.Entidades;
using ControlAutobuses.Negocio;

namespace ControlAutobuses.Presentacion
{
    public partial class frmAutobuses : Form
    {
        private AutobusBL autobusBL;

        public frmAutobuses()
        {
            InitializeComponent();
            autobusBL = new AutobusBL();
            ConfigurarInterfaz();
            CargarAutobuses();
        }

        private void ConfigurarInterfaz()
        {
            this.Text = "Gestión de Autobuses";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(236, 240, 241);

            // Panel contenedor
            Panel panel = new Panel();
            panel.BackColor = Color.White;
            panel.Dock = DockStyle.Fill;
            panel.Padding = new Padding(20);
            this.Controls.Add(panel);

            // Título
            Label lblTitulo = new Label();
            lblTitulo.Text = "🚌 Gestión de Autobuses";
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
            string[] labels = { "Marca:", "Modelo:", "Placa:", "Color:", "Año:" };
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
                if (i == 4) // Año
                {
                    NumericUpDown numeric = new NumericUpDown();
                    numeric.Minimum = 1990;
                    numeric.Maximum = DateTime.Now.Year + 1;
                    numeric.Value = DateTime.Now.Year;
                    numeric.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                    numeric.Size = new Size(300, 30);
                    numeric.Location = new Point(20, topPosition + 25);
                    numeric.Name = "txtAnio";
                    inputControl = numeric;
                }
                else
                {
                    TextBox txt = new TextBox();
                    txt.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    txt.Size = new Size(300, 30);
                    txt.Location = new Point(20, topPosition + 25);

                    switch (i)
                    {
                        case 0: txt.Name = "txtMarca"; break;
                        case 1: txt.Name = "txtModelo"; break;
                        case 2: txt.Name = "txtPlaca"; break;
                        case 3: txt.Name = "txtColor"; break;
                    }

                    inputControl = txt;
                }

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

            // Grid de autobuses
            buttonTop += 60;
            Label lblGrid = new Label();
            lblGrid.Text = "Autobuses Registrados";
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
            grid.Name = "dgvAutobuses";

            // Columnas
            grid.Columns.Add("Id", "ID");
            grid.Columns.Add("Marca", "Marca");
            grid.Columns.Add("Modelo", "Modelo");
            grid.Columns.Add("Placa", "Placa");
            grid.Columns.Add("Color", "Color");
            grid.Columns.Add("Anio", "Año");
            grid.Columns.Add("Disponible", "Disponible");
            grid.Columns.Add("Acciones", "Acciones");

            // Ocultar columna ID
            grid.Columns["Id"].Visible = false;

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

        private void CargarAutobuses()
        {
            try
            {
                DataGridView dgv = this.Controls.Find("dgvAutobuses", true)[0] as DataGridView;
                dgv.Rows.Clear();

                List<Autobus> autobuses = autobusBL.ObtenerAutobuses();

                foreach (var autobus in autobuses)
                {
                    string disponible = autobus.Disponible ? "Sí" : "No";
                    dgv.Rows.Add(
                        autobus.Id,
                        autobus.Marca,
                        autobus.Modelo,
                        autobus.Placa,
                        autobus.Color,
                        autobus.Anio,
                        disponible,
                        "Editar | Eliminar"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar autobuses: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string marca = this.Controls.Find("txtMarca", true)[0].Text;
                string modelo = this.Controls.Find("txtModelo", true)[0].Text;
                string placa = this.Controls.Find("txtPlaca", true)[0].Text;
                string color = this.Controls.Find("txtColor", true)[0].Text;
                int anio = (int)(this.Controls.Find("txtAnio", true)[0] as NumericUpDown).Value;

                Autobus nuevoAutobus = new Autobus
                {
                    Marca = marca,
                    Modelo = modelo,
                    Placa = placa,
                    Color = color,
                    Anio = anio
                };

                bool resultado = autobusBL.CrearAutobus(nuevoAutobus);

                if (resultado)
                {
                    MessageBox.Show("Autobús registrado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    CargarAutobuses();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            // Implementar búsqueda si es necesario
            CargarAutobuses();
        }

        private void LimpiarFormulario()
        {
            this.Controls.Find("txtMarca", true)[0].Text = "";
            this.Controls.Find("txtModelo", true)[0].Text = "";
            this.Controls.Find("txtPlaca", true)[0].Text = "";
            this.Controls.Find("txtColor", true)[0].Text = "";
            (this.Controls.Find("txtAnio", true)[0] as NumericUpDown).Value = DateTime.Now.Year;
        }

        // Manejar eventos del DataGridView
        private void dgvAutobuses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridView dgv = sender as DataGridView;
                string accion = dgv.Rows[e.RowIndex].Cells["Acciones"].Value.ToString();

                if (accion.Contains("Editar"))
                {
                    int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["Id"].Value);
                    EditarAutobus(id);
                }
                else if (accion.Contains("Eliminar"))
                {
                    int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["Id"].Value);
                    EliminarAutobus(id);
                }
            }
        }

        private void EditarAutobus(int id)
        {
            try
            {
                Autobus autobus = autobusBL.ObtenerAutobusPorId(id);

                if (autobus != null)
                {
                    this.Controls.Find("txtMarca", true)[0].Text = autobus.Marca;
                    this.Controls.Find("txtModelo", true)[0].Text = autobus.Modelo;
                    this.Controls.Find("txtPlaca", true)[0].Text = autobus.Placa;
                    this.Controls.Find("txtColor", true)[0].Text = autobus.Color;
                    (this.Controls.Find("txtAnio", true)[0] as NumericUpDown).Value = autobus.Anio;

                    // Cambiar el botón Guardar para que actualice
                    Button btnGuardar = this.Controls.Find("btnGuardar", true)[0] as Button;
                    btnGuardar.Text = "Actualizar";
                    btnGuardar.Tag = id; // Guardar el ID para la actualización
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar autobús: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminarAutobus(int id)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "¿Está seguro de que desea eliminar este autobús?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    bool eliminado = autobusBL.EliminarAutobus(id);

                    if (eliminado)
                    {
                        MessageBox.Show("Autobús eliminado correctamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarAutobuses();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar autobús: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}