using System.Windows.Forms;

namespace RssReader
{
    public partial class Description : Form
    {
        private WebBrowser webBrowser;

        public Description(string text)
        {
            InitializeComponent(text);
        }
    }
}

