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
    public partial class FormInsertMovie : Syncfusion.Windows.Forms.Tools.RibbonForm
    {
        ConexionPeliculas conexion;


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();



        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr one, int two, int three, int four);


        public FormInsertMovie()
        {
            InitializeComponent();
            conexion = new ConexionPeliculas();
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
            if (int.TryParse(textBoxDuracion.Text, out int result))
            {
                conexion.Insert(textBoxNom.Text, int.Parse(textBoxDuracion.Text), textBoxClas.Text);
                this.Dispose();
            }
            else
                MessageBox.Show("Por favor introduzca un valor entero en duración (minutos)");
        }

        private void panelMainBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }
    }
}
