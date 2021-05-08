using Autocinema.bdConexion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Autocinema.forms
{
    public partial class FormInsertTicket : Syncfusion.Windows.Forms.Tools.RibbonForm
    {
        Dictionary<int, string> IdClienteDic = new Dictionary<int, string>();
        ConexionBoletos conexion = new ConexionBoletos();




        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();



        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr one, int two, int three, int four);


        public FormInsertTicket()
        {
            InitializeComponent();
            LoadCombos();
        }

         void LoadCombos()
        {
            ConexionClientes conexionClientes = new ConexionClientes();
            ConexionHorarios conexionHorarios = new ConexionHorarios();

            int[] idsClientes = conexionClientes.ConsultIds();
           
            for(int i=0;i<idsClientes.Length;i++)
                cBoxClientes.Items.Add(idsClientes[i]);

            int[] idsHorarios= conexionHorarios.ConsultIds();
            for (int i = 0; i < idsHorarios.Length; i++)
                comboBoxHorario.Items.Add(idsHorarios[i]);
        }
    
        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxFolio.Text, out int result) && int.TryParse(cBoxClientes.Text, out result) && int.TryParse(cBoxClientes.Text, out result))
            {
                conexion.Insert(int.Parse(textBoxFolio.Text), int.Parse(cBoxClientes.Text), int.Parse(comboBoxHorario.Text));
                this.Dispose();
            }
            else MessageBox.Show("Por favor rellene todos los campos usando números enteros. ");
            
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void panelMainBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }
    }
}
