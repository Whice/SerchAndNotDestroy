using System;
using System.Windows.Forms;

namespace MyLittleMinion

{
    public partial class FormForLogs : Form
    {
        public FormForLogs()
        {
            InitializeComponent();
        }
        public void WriteTextInLogs(string text)
        {
            if (richTextBox1.Text=="")
            {

                richTextBox1.Text += "0 " + text + "\n";
            }
            else
            {
                char prevNumberChar = richTextBox1.Lines[richTextBox1.Lines.Length - 2][0];
                ulong prevNumberUlong = Convert.ToUInt64(prevNumberChar);
                richTextBox1.Text += prevNumberUlong.ToString() + " " + text + "\n";
            }
        }
    }
}
