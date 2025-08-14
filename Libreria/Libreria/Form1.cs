using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp4;

namespace Libreria
{
    public partial class Form1 : Form
    {
        public int id {  get; set; }
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.id = 0;    
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbTodos.Checked = true;

            string sql;
            DataTable dt = new DataTable();

            sql = $"select * from Editoriales order by Editorial asc";
            dt = Libreriaa.Recuperar(sql);
            cmbEditoriales.DataSource = dt ;
            cmbEditoriales.ValueMember = "IdEditorial";
            cmbEditoriales.DisplayMember = "Editorial";
            cmbEditoriales.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEditoriales.SelectedIndex = 0;
        }

        private void CargarGrilla()
        {
            string sql;
            DataTable dt = new DataTable();

            if (cbTodos.Checked)
            {
                sql = $@"select 
                            IdLibro,
                            Titulo,
                            Autores.Apellido + ', ' + Autores.Nombre as Autor,
                            Editoriales.Editorial,
                            FechaEdicion as FEd,
                            Disponible,
                            Precio
                        from Libros
                        join Autores on Libros.IdAutor = Autores.IdAutor
                        join Editoriales on Libros.IdEditorial = Editoriales.IdEditorial
                        order by Titulo asc";
            }
            else
            {
                sql = $@"select 
                            IdLibro,
                            Titulo,
                            Autores.Apellido + ', ' + Autores.Nombre as Autor,
                            Editoriales.Editorial,
                            FechaEdicion as FEd,
                            Disponible,
                            Precio
                        from Libros
                        join Autores on Libros.IdAutor = Autores.IdAutor
                        join Editoriales on Libros.IdEditorial = Editoriales.IdEditorial
                        where Libros.IdEditorial = {cmbEditoriales.SelectedValue}
                        order by Titulo asc";
            }

            dt = Libreriaa.Recuperar(sql);
            dgvLibros.DataSource = dt;
            dgvLibros.ReadOnly = true;
            dgvLibros.AllowUserToAddRows = false;
            dgvLibros.AllowUserToDeleteRows = false;
            dgvLibros.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLibros.Columns["IdLibro"].Visible = false;

            lblCantidad.Text = $"Cant. de Libros = {dgvLibros.RowCount}";

        }
        private void cbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTodos.Checked)
            {
                cmbEditoriales.Enabled = false;
            }
            else
            {
                cmbEditoriales.Enabled = true;
            }
            CargarGrilla();
        }

        private void cmbEditoriales_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void cambiarDisponibilidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql;
            int id = int.Parse(dgvLibros.CurrentRow.Cells["IdLibro"].Value.ToString());
            bool disponible = Convert.ToBoolean(dgvLibros.CurrentRow.Cells["Disponible"].Value.ToString());

            if (dgvLibros.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar una fila", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (disponible)
                {
                    sql = $"update Libros set Disponible = {0} where IdLibro = '{id}'";
                }
                else
                {
                    sql = $"update Libros set Disponible = {1} where IdLibro = '{id}'";
                }
                Libreriaa.Ejecutar(sql);
            }
            CargarGrilla();
        }

        private void exportarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string cadena;
            FileStream archivo = new FileStream("Archivo.csv", FileMode.Create);
            StreamWriter contenido = new StreamWriter(archivo);

            foreach (DataGridViewRow fila in dgvLibros.Rows)
            {
                cadena = string.Empty;
                foreach (DataGridViewCell celda in fila.Cells)
                {
                    cadena += celda.Value.ToString() + ";";
                }
                contenido.WriteLine(cadena);
            }
            contenido.Close();
            archivo.Close();
            MessageBox.Show("Se exporto correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
