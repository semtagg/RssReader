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
            //this.Load += new System.EventHandler(this.Form1_Load);
            ResumeLayout(false);
        }
        private void webBrowser1_Navigating(object sender, 
            WebBrowserNavigatingEventArgs e)
        {
            System.Windows.Forms.HtmlDocument document =
                this.webBrowser.Document;

            if (document != null && document.All["userName"] != null && 
                String.IsNullOrEmpty(
                    document.All["userName"].GetAttribute("value")))
            {
                e.Cancel = true;
                System.Windows.Forms.MessageBox.Show(
                    "You must enter your name before you can navigate to " +
                    e.Url.ToString());
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string html = "<font face='arial' size='3'>Hello,<br />My name is ";
            html += "<b style = 'color:red'>Mudassar Khan</b>.</font>";
            html += "<hr /><img src='https://www.aspsnippets.com/images/authors/Mudassar.png' />";
            webBrowser.DocumentText = html;
        }
    }
}

