using System.Windows.Forms;

namespace RssReader
{
    public partial class InputBoxForm : Form
    {
        private TableLayoutPanel table;
        public string Input { get; private set; }

        public InputBoxForm(string message)
        {
            InitializeComponent(message);
        }
    }
}

