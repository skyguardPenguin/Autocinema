using Autocinema.bdConexion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Autocinema.forms
{
    public partial class FormInsertSchedule : Syncfusion.Windows.Forms.Tools.RibbonForm
    {
       
        Dictionary<int, string> IdNameDic = new Dictionary<int, string>();
        ConexionHorarios conexionHorarios = new ConexionHorarios();


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr one, int two, int three, int four);

        public FormInsertSchedule()
        {
            InitializeComponent();
            LoadCBoxPeliculas();
        }
        private void LoadCBoxPeliculas()
        {
            ConexionPeliculas conexionPeliculas = new ConexionPeliculas();
            IdNameDic = conexionPeliculas.ConsultIdName();
            
            foreach(string i in IdNameDic.Values)
            {
                cBox.Items.Add(i);
            }
            
        }


        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panelMainBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        
        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            int idPelicula=0;
            foreach (int i in IdNameDic.Keys)
                if (IdNameDic[i] == cBox.SelectedItem.ToString())
                    idPelicula = i;

            string[] dateArray = dateTime.DateTimeText.Split('/');  
           conexionHorarios.Insert(idPelicula, dateArray[2]+"-"+dateArray[1]+"-"+dateArray[0], cBoxHora.SelectedItem + ":" + cBoxMinuto.SelectedItem + ":00");
            MessageBox.Show("Insertado");
            this.Dispose();
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
