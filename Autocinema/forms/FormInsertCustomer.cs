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
    public partial class FormInsertCustomer : Syncfusion.Windows.Forms.Tools.RibbonForm
    {

        ConexionClientes conexion;


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();



        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr one, int two, int three, int four);
        public FormInsertCustomer()
        {
            InitializeComponent();
            conexion = new ConexionClientes();
        }

        private void panelMainBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            if (textBoxMarca.Text != "" && textBoxMat.Text != "")
            {
                conexion.Insert(textBoxMat.Text, textBoxMarca.Text);
                this.Dispose();
            }
            else MessageBox.Show("Por favor rellene los campos.");
        }
    }
}
