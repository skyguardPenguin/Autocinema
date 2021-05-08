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
    public partial class FormCustomers : Syncfusion.Windows.Forms.Tools.RibbonForm
    {
        ConexionClientes conexion;
        public FormCustomers()
        {
            InitializeComponent();
            RefreshDataGrid();
        }
        private void RefreshDataGrid()
        {
            conexion = new ConexionClientes();
            dataGridView1.DataSource = conexion.ConsultAll();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            FormInsertCustomer vent = new FormInsertCustomer();
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
                int idCliente = (int)dataGridView1.CurrentRow.Cells["id"].Value;
                conexion.Delete(idCliente);
                MessageBox.Show("Eliminado correctamente");
                RefreshDataGrid();
            }
            else
                MessageBox.Show("seleccione una fila por favor");
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int id = (int)dataGridView1.Rows[e.RowIndex].Cells["id"].Value;
            string matriculaAuto = dataGridView1.Rows[e.RowIndex].Cells["matriculaAuto"].Value.ToString();
            string marcaAuto = dataGridView1.Rows[e.RowIndex].Cells["marcaAuto"].Value.ToString();


            conexion.Update(id,matriculaAuto,marcaAuto);
            RefreshDataGrid();
            MessageBox.Show("Actualizado");
        }
    }
}
