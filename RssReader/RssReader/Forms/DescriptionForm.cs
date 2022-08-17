using System.Drawing;
using System.Windows.Forms;

namespace RssReader
{
    public partial class Description : Form
    {
        private WebBrowser webBrowser;
        private TextBox textBox;
        private MenuStrip mainMenu;
        private TableLayoutPanel table;

        public Description(string text)
        {
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 311);
            Text = "Description";
            InitializeComponent(text);
        }
    }
}

