using Autocinema.bdConexion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Autocinema.forms
{
    public partial class FormProducts : Syncfusion.Windows.Forms.Tools.RibbonForm
    {
        ConexionProductosComestibles conexion;
        public FormProducts()
        {
            InitializeComponent();
            RefreshDataGrid();
        }
        private void RefreshDataGrid()
        {
            conexion = new ConexionProductosComestibles();
            DataTable table = conexion.ConsultAll();
            dataGridView1.DataSource = table;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            FormInsertProduct vent = new FormInsertProduct();
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
                int idProducto = (int)dataGridView1.CurrentRow.Cells["id"].Value;
                conexion.Delete(idProducto);
                MessageBox.Show("Eliminado correctamente");
                RefreshDataGrid();
            }
            else
                MessageBox.Show("seleccione una fila por favor");
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int idProdutco = (int)dataGridView1.Rows[e.RowIndex].Cells["id"].Value;
            string nombre = dataGridView1.Rows[e.RowIndex].Cells["nombre"].Value.ToString();
            double precio = (double)dataGridView1.Rows[e.RowIndex].Cells["precio"].Value;
         
            
            conexion.Update(idProdutco, nombre, (float)precio);
            RefreshDataGrid();
            MessageBox.Show("Actualizado");
        }
    }
}
