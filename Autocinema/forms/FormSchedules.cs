using Autocinema.bdConexion;
using Autocinema.forms;
using Syncfusion.WinForms.DataGrid.Interactivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Autocinema
{
    public partial class FormSchedule : Syncfusion.Windows.Forms.Tools.RibbonForm
    {
        ConexionHorarios conexion;
       
        public FormSchedule()
        {
            InitializeComponent();
            conexion= new ConexionHorarios();
            RefreshDataGrid();
        }
        private void RefreshDataGrid()
        {

            DataTable table = new DataTable();
            conexion = new ConexionHorarios();
            table=conexion.ConsultAll();
       
            dataGridView1.DataSource = table;
         
        }
       
        

        private void addButton_Click(object sender, EventArgs e)
        {
            FormInsertSchedule vent = new FormInsertSchedule();
            vent.Show();
            vent.Disposed += childForm_Disposed;
       
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
         
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int idHorario = (int)dataGridView1.CurrentRow.Cells["Id"].Value;
                conexion.Delete(idHorario);
                MessageBox.Show("Eliminado correctamente");
                RefreshDataGrid();
            }
            else
                MessageBox.Show("seleccione una fila por favor");

        }


        private void childForm_Disposed(object sender , EventArgs e)
        {
            RefreshDataGrid();
        }
     

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           int  idHorario=(int)dataGridView1.Rows[e.RowIndex].Cells["id"].Value;
            int idPelicula = (int)dataGridView1.Rows[e.RowIndex].Cells["idPelicula"].Value;
            string hora = dataGridView1.Rows[e.RowIndex].Cells["hora"].Value.ToString();
            string fecha=dataGridView1.Rows[e.RowIndex].Cells["fecha"].Value.ToString();

            
            fecha= fecha.Substring(0, 10);
            string[] dateArray = fecha.Split('/');
            conexion.Update(idHorario, idPelicula, fecha, hora);
            RefreshDataGrid();
            MessageBox.Show("Actualizado");
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name.Contains("idPelicula"))
            {
                DataGridViewComboBoxCell comboBP = new DataGridViewComboBoxCell();
                dataGridView1[e.ColumnIndex,e.RowIndex] = comboBP;
                comboBP.Style.BackColor = SystemColors.Control;
                comboBP.Style.SelectionBackColor = SystemColors.Control;
                comboBP.Style.SelectionForeColor = SystemColors.Control;
                
                ConexionPeliculas conexionP = new ConexionPeliculas();
                Dictionary<int, string> dic = new Dictionary<int, string>();
                DataTable table = new DataTable();
                table.Columns.Add("idPelicula", typeof(int));
                dic = conexionP.ConsultIdName();
                foreach (int i in dic.Keys)
                    table.Rows.Add(i);
                comboBP.ValueMember = "idPelicula";
                comboBP.DisplayMember = "idPelicula";
                comboBP.DataSource = table;
                
            }
        }

    }
}
