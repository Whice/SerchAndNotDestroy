using System;
using System.Windows.Forms;

namespace MyLittleMinion
{
    public partial class InstructionFrom : Form
    {
        public InstructionFrom()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            /*WebBrowser wb = new WebBrowser();
            wb.Navigate(linkLabel1.Text);*/
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }
    }
}
