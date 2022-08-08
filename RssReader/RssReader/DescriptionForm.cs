using System.Windows.Forms;

namespace RssReader
{
    public partial class Description : Form
    {
        private WebBrowser webBrowser1;

        public Description(string text)
        {
            InitializeComponent(text);
        }
    }
}

