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
            richTextBox1.Text += text + "\n";
        }
    }
}
