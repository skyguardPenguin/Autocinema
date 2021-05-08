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
    public partial class FormTickets : Syncfusion.Windows.Forms.Tools.RibbonForm
    {
        ConexionBoletos conexion = new ConexionBoletos();
        public FormTickets()
        {
            InitializeComponent();
            conexion = new ConexionBoletos();
            RefreshDataGrid();
        }
        private void RefreshDataGrid()
        {
            conexion = new ConexionBoletos();
            DataTable table = conexion.ConsultAll();
            dataGridView1.DataSource = table;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            FormInsertTicket vent = new FormInsertTicket();
            vent.Show();
            vent.Disposed += ChildForm_Disposed;
        }
        private void ChildForm_Disposed(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name.Contains("idHorario"))
            {
                DataGridViewComboBoxCell comboBP = new DataGridViewComboBoxCell();
                dataGridView1[e.ColumnIndex, e.RowIndex] = comboBP;
                comboBP.Style.BackColor = SystemColors.Control;
                comboBP.Style.SelectionBackColor = SystemColors.Control;
                comboBP.Style.SelectionForeColor = SystemColors.Control;

                ConexionHorarios conexionH = new ConexionHorarios();
                int[] ids;
                DataTable table = new DataTable();
                table.Columns.Add("idHorario", typeof(int));
                ids = conexionH.ConsultIds();
                foreach (int i in ids)
                    table.Rows.Add(i);
                comboBP.ValueMember = "idHorario";
                comboBP.DisplayMember = "idHorario";
                comboBP.DataSource = table;

            }
            else if(dataGridView1.Columns[e.ColumnIndex].Name.Contains("idCliente"))
            {
                DataGridViewComboBoxCell comboBP = new DataGridViewComboBoxCell();
                dataGridView1[e.ColumnIndex, e.RowIndex] = comboBP;
                comboBP.Style.BackColor = SystemColors.Control;
                comboBP.Style.SelectionBackColor = SystemColors.Control;
                comboBP.Style.SelectionForeColor = SystemColors.Control;

                ConexionClientes conexionC = new ConexionClientes();
                int[] ids;
                DataTable table = new DataTable();
                table.Columns.Add("idCliente", typeof(int));
                ids = conexionC.ConsultIds();
                foreach (int i in ids)
                    table.Rows.Add(i);
                comboBP.ValueMember = "idCliente";
                comboBP.DisplayMember = "idCliente";
                comboBP.DataSource = table;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int folio = (int)dataGridView1.Rows[e.RowIndex].Cells["folio"].Value;
            int idCliente = (int)dataGridView1.Rows[e.RowIndex].Cells["idCliente"].Value;
            int idHorario = (int)dataGridView1.Rows[e.RowIndex].Cells["idHorario"].Value;
            
            conexion.Update(folio,idCliente,idHorario);
            RefreshDataGrid();
            MessageBox.Show("Actualizado");
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int idBoleto = (int)dataGridView1.CurrentRow.Cells["folio"].Value;
                conexion.Delete(idBoleto);
                MessageBox.Show("Eliminado correctamente");
                RefreshDataGrid();
            }
            else
                MessageBox.Show("seleccione una fila por favor");
        }
    }
}
