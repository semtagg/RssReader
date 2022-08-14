using System.Windows.Forms;

namespace RssReader
{
    public partial class Description : Form
    {
        private WebBrowser webBrowser;
        private TextBox textBox;
        private MenuStrip mainMenu;

        public Description(string text)
        {
            InitializeComponent(text);
        }
    }
}

