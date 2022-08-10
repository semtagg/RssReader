using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RssReader
{
    public partial class TableForm : Form
    {
        private LinkLabel linkLabel;
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
