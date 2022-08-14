using System.Drawing;
using System.Windows.Forms;

namespace RssReader
{
    public partial class TableForm : Form
    {
        private TableLayoutPanel table;
        private MenuStrip mainMenu;
        private RssFeed rssFeed;
        private Timer timer;

        public TableForm()
        {
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Text = "RssReader";
            InitializeComponent();
        }
    }
}
