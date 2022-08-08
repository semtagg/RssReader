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
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent(string text)
        {
            /*this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Description";*/
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(334, 311);
            this.webBrowser1.TabIndex = 1;
            this.webBrowser1.DocumentText = text;
            webBrowser1.Navigating += (sender, args) =>
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {args.Url.ToString()}"));
                args.Cancel = true;
            };
            // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 311);
            this.Controls.Add(this.webBrowser1);
            this.Name = "Form1";
            this.Text = "Form1";
            //this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
        }
        private void webBrowser1_Navigating(object sender, 
            WebBrowserNavigatingEventArgs e)
        {
            System.Windows.Forms.HtmlDocument document =
                this.webBrowser1.Document;

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
            webBrowser1.DocumentText = html;
        }
        #endregion
    }
}

