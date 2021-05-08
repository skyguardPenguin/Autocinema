using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Autocinema.bdConexion;

namespace Autocinema.forms
{
    public partial class FormMovies : Syncfusion.Windows.Forms.Tools.RibbonForm
    {
        ConexionPeliculas conexion;
        public FormMovies()
        {
            InitializeComponent();
            conexion = new ConexionPeliculas();
            RefreshDataGrid();
            
        }

        
        private void RefreshDataGrid()
        {
            conexion = new ConexionPeliculas();
            DataTable table = conexion.ConsultAll();
            dataGridView1.DataSource = table;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            FormInsertMovie vent = new FormInsertMovie();
            vent.Show();
            vent.Disposed += ChildForm_Disposed;

        }
        private void ChildForm_Disposed(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int idPelicula = (int)dataGridView1.CurrentRow.Cells["Id"].Value;
                conexion.Delete(idPelicula);
                MessageBox.Show("Eliminado correctamente");
                RefreshDataGrid();
            }
            else
                MessageBox.Show("seleccione una fila por favor");
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int id= (int)dataGridView1.Rows[e.RowIndex].Cells["id"].Value;
            string nombre = dataGridView1.Rows[e.RowIndex].Cells["nombre"].Value.ToString();
            int duracion = (int)dataGridView1.Rows[e.RowIndex].Cells["duracion"].Value;
            string clasificacion = dataGridView1.Rows[e.RowIndex].Cells["clasificacion"].Value.ToString();


          
            conexion.Update(id,nombre,duracion,clasificacion);
            RefreshDataGrid();
            MessageBox.Show("Actualizado");
        }
    }
}
