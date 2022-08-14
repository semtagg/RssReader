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
            this.SuspendLayout();
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

            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 311);
            Controls.Add(this.webBrowser);
            Name = "Description";
            Text = "Description";
            ResumeLayout(false);
        }

        private void ShowBox(string text)
        {
            this.textBox = new System.Windows.Forms.TextBox();
            textBox.Location = new Point(0, 30);
            this.SuspendLayout();
            this.textBox.AcceptsReturn = true;
            this.textBox.AcceptsTab = true;
            this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox.Multiline = true;
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            textBox.ReadOnly = true;
            textBox.Text = text;
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Controls.Add(this.textBox);
            this.Text = "TextBox Example";
            this.ResumeLayout(false);
            this.PerformLayout();
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
                ShowWeb(text);
            };

            var notFormated = new ToolStripMenuItem()
            {
                Text = "Без форматирования",
                Tag = "NotFormated"
            };
            notFormated.Click += (sender, args) =>
            {
                ShowBox(text);
            };

            mainMenu.Items.Add(formated);
            mainMenu.Items.Add(notFormated);
            Controls.Add(mainMenu);
        }

        private void InitializeComponent(string text)
        {
            InitializeMainMenu(text);
            ShowBox(text);
        }
    }
}
