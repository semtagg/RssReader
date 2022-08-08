using System.Windows.Forms;

namespace RssReader
{
    public partial class InputBoxForm : Form
    {
        public string Input { get; set; }

        public InputBoxForm(string message)
        {
            InitializeComponent(message);
        }
    }
}

