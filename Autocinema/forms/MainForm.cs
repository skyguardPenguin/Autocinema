using Autocinema.forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Autocinema
{
    public partial class MainForm : Syncfusion.Windows.Forms.Tools.RibbonForm
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();



        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr one, int two, int three, int four);
        public MainForm()
        {
            InitializeComponent();
            OpenChildForm(new FormSchedule());
        }

        private void panelMainBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);

        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private Form activeForm = null;
        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childFormPanel.Controls.Add(childForm);
            childFormPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();


        }

        private void buttonSchedule_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormSchedule());
        }

        private void button2_Click(object sender, EventArgs e)
        {

            OpenChildForm(new FormMovies());
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormTickets());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormProducts());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormCustomers());
        }
    }
}
