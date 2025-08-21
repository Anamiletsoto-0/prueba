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
    public partial class frmRutas : Form
    {
        private RutaBL rutaBL;

        public frmRutas()
        {
            InitializeComponent();
            rutaBL = new RutaBL();
            ConfigurarInterfaz();
            CargarRutas();
        }

        private void ConfigurarInterfaz()
        {
            this.Text = "Gestión de Rutas";
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
            lblTitulo.Text = "🗺️ Gestión de Rutas";
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

            // Campo de nombre de ruta
            Label lblNombre = new Label();
            lblNombre.Text = "Nombre de la Ruta:";
            lblNombre.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblNombre.ForeColor = Color.FromArgb(44, 62, 80);
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(20, topPosition);
            panel.Controls.Add(lblNombre);

            TextBox txtNombre = new TextBox();
            txtNombre.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            txtNombre.BorderStyle = BorderStyle.FixedSingle;
            txtNombre.Size = new Size(300, 30);
            txtNombre.Location = new Point(20, topPosition + 25);
            txtNombre.Name = "txtNombre";
            panel.Controls.Add(txtNombre);

            // Botones
            int buttonTop = topPosition + 70;
            Button btnGuardar = CreateButton("Guardar", Color.FromArgb(44, 62, 80), 20, buttonTop);
            btnGuardar.Click += BtnGuardar_Click;
            btnGuardar.Name = "btnGuardar";
            panel.Controls.Add(btnGuardar);

            Button btnLimpiar = CreateButton("Limpiar", Color.FromArgb(149, 165, 166), 120, buttonTop);
            btnLimpiar.Click += BtnLimpiar_Click;
            panel.Controls.Add(btnLimpiar);

            Button btnBuscar = CreateButton("Buscar", Color.FromArgb(39, 174, 96), 220, buttonTop);
            btnBuscar.Click += BtnBuscar_Click;
            panel.Controls.Add(btnBuscar);

            // Grid de rutas
            buttonTop += 60;
            Label lblGrid = new Label();
            lblGrid.Text = "Rutas Registradas";
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
            grid.Name = "dgvRutas";
            grid.CellClick += dgvRutas_CellClick;

            // Columnas
            grid.Columns.Add("Id", "ID");
            grid.Columns.Add("Nombre", "Nombre de la Ruta");
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

        private void CargarRutas()
        {
            try
            {
                DataGridView dgv = this.Controls.Find("dgvRutas", true)[0] as DataGridView;
                dgv.Rows.Clear();

                List<Ruta> rutas = rutaBL.ObtenerRutas();

                foreach (var ruta in rutas)
                {
                    string disponible = ruta.Disponible ? "Sí" : "No";
                    dgv.Rows.Add(
                        ruta.Id,
                        ruta.Nombre,
                        disponible,
                        "Editar | Eliminar"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar rutas: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = this.Controls.Find("txtNombre", true)[0].Text;

                Ruta nuevaRuta = new Ruta
                {
                    Nombre = nombre
                };

                Button btn = sender as Button;

                if (btn.Text == "Guardar")
                {
                    bool resultado = rutaBL.CrearRuta(nuevaRuta);

                    if (resultado)
                    {
                        MessageBox.Show("Ruta registrada correctamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarFormulario();
                        CargarRutas();
                    }
                }
                else if (btn.Text == "Actualizar")
                {
                    int id = (int)btn.Tag;
                    nuevaRuta.Id = id;

                    bool resultado = rutaBL.ActualizarRuta(nuevaRuta);

                    if (resultado)
                    {
                        MessageBox.Show("Ruta actualizada correctamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarFormulario();
                        CargarRutas();

                        // Restaurar botón a estado normal
                        btn.Text = "Guardar";
                        btn.Tag = null;
                    }
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

            // Restaurar botón Guardar si estaba en modo Actualizar
            Button btnGuardar = this.Controls.Find("btnGuardar", true)[0] as Button;
            if (btnGuardar.Text == "Actualizar")
            {
                btnGuardar.Text = "Guardar";
                btnGuardar.Tag = null;
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            // Implementar búsqueda si es necesario
            CargarRutas();
        }

        private void LimpiarFormulario()
        {
            this.Controls.Find("txtNombre", true)[0].Text = "";
        }

        private void dgvRutas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridView dgv = sender as DataGridView;

                if (e.ColumnIndex == dgv.Columns["Acciones"].Index)
                {
                    string accion = dgv.Rows[e.RowIndex].Cells["Acciones"].Value.ToString();
                    int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["Id"].Value);

                    if (accion.Contains("Editar"))
                    {
                        EditarRuta(id);
                    }
                    else if (accion.Contains("Eliminar"))
                    {
                        EliminarRuta(id);
                    }
                }
            }
        }

        private void EditarRuta(int id)
        {
            try
            {
                Ruta ruta = rutaBL.ObtenerRutaPorId(id);

                if (ruta != null)
                {
                    this.Controls.Find("txtNombre", true)[0].Text = ruta.Nombre;

                    // Cambiar el botón Guardar para que actualice
                    Button btnGuardar = this.Controls.Find("btnGuardar", true)[0] as Button;
                    btnGuardar.Text = "Actualizar";
                    btnGuardar.Tag = id; // Guardar el ID para la actualización
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ruta: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminarRuta(int id)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "¿Está seguro de que desea eliminar esta ruta?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    bool eliminada = rutaBL.EliminarRuta(id);

                    if (eliminada)
                    {
                        MessageBox.Show("Ruta eliminada correctamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarRutas();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar ruta: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para cargar rutas disponibles en un ComboBox (útil para asignaciones)
        public static void CargarRutasEnComboBox(ComboBox comboBox)
        {
            try
            {
                RutaBL rutaBL = new RutaBL();
                List<Ruta> rutas = rutaBL.ObtenerRutasDisponibles();

                comboBox.Items.Clear();
                comboBox.DisplayMember = "Nombre";
                comboBox.ValueMember = "Id";

                foreach (var ruta in rutas)
                {
                    comboBox.Items.Add(ruta);
                }

                if (comboBox.Items.Count > 0)
                {
                    comboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar rutas: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}