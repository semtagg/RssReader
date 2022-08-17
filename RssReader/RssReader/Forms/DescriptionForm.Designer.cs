using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace RssReader
{
    partial class Description
    {
        private void ShowWeb(string text)
        {
            webBrowser = new WebBrowser();
            webBrowser.Dock = DockStyle.Fill;
            webBrowser.Location = new Point(0, 30);
            webBrowser.MinimumSize = new Size(20, 20);
            webBrowser.Name = "webBrowser";
            webBrowser.Size = new Size(334, 311);
            webBrowser.TabIndex = 1;
            webBrowser.DocumentText = text;
            webBrowser.Navigating += (sender, args) =>
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {args.Url.ToString()}"));
                args.Cancel = true;
            };
            
            table.Controls.Add(webBrowser, 0, 1);
        }

        private void ShowBox(string text)
        {
            this.textBox = new TextBox();
            textBox.AcceptsReturn = true;
            textBox.AcceptsTab = true;
            textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox.Multiline = true;
            textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            textBox.ReadOnly = true;
            textBox.Text = text;
            table.Controls.Add(textBox, 0, 1);
        }

        private void InitializeMainMenu(string text)
        {
            mainMenu = new MenuStrip();
            mainMenu.Location = new Point(0, 0);
            mainMenu.Height = 30;

            var formated = new ToolStripMenuItem()
            {
                Text = "С форматирования",
                Tag = "Formated"
            };
            formated.Click += (sender, args) =>
            {
                while (table.Controls.Count > 0)
                {
                    table.Controls[0].Dispose();
                }
                table.Controls.Clear();
                ShowWeb(text);
            };

            var notFormated = new ToolStripMenuItem()
            {
                Text = "Без форматирования",
                Tag = "NotFormated"
            };
            notFormated.Click += (sender, args) =>
            {
                while (table.Controls.Count > 0)
                {
                    table.Controls[0].Dispose();
                }
                table.Controls.Clear();
                ShowBox(text);
            };

            mainMenu.Items.Add(formated);
            mainMenu.Items.Add(notFormated);
            Controls.Add(mainMenu);
        }

        private void InitializeComponent(string text)
        {
            InitializeMainMenu(text);
            table = new TableLayoutPanel();
            table.AutoScroll = true;
            table.RowStyles.Clear(); 
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            ShowBox(text);
            table.Dock = DockStyle.Fill;
            Controls.Add(table);
        }
    }
}
