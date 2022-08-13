using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Diagnostics;

namespace RssReader
{
    partial class Description
    {
        private void InitializeComponent(string text)
        {
            //var index = 0;
            var button = new Button()
            {
                Text = "Изменить стиль отображения"
            };
            Controls.Add(button);
            
            webBrowser = new WebBrowser();
            this.SuspendLayout();
            webBrowser.Dock = DockStyle.Fill;
            webBrowser.Location = new Point(0, 0);
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
    }
}
