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
using System.IO;
using System.Text.Json;

namespace RssReader
{
    partial class TableForm
    {
        string url = "https://habr.com/ru/rss/interesting/"; //https://www.nasa.gov/rss/dyn/breaking_news.rss
        private void InitializeComponent()
        {
            var jsonString = File.ReadAllText("config.json");
            configurationModel = JsonSerializer.Deserialize<ConfigurationModel>(jsonString);

            var interval = configurationModel.RefreshTimeInMinutes * 1000;
           
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            var count = feed.Items.Count();


            mainMenu = new MenuStrip();
            mainMenu.Location = new Point(0,0);
            var menuItem1 = new ToolStripMenuItem()
            {
                Text = "Изменить ленту",
                Tag = "Edit Feed"
            };
            menuItem1.Click += (sender, args) =>
            {
                using (var frm = new InputBoxForm("Введите ссылку:"))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                        url = frm.Input;
                }
            };


            var menuItem2 = new ToolStripMenuItem()
            {
                Text = "Изменить частоту обновления",
                Tag = "Edit Refresh Time"
            };
            

            mainMenu.Items.Add(menuItem1);
            mainMenu.Items.Add(menuItem2);

            table = new TableLayoutPanel();
            table.AutoScroll = true;
            table.RowStyles.Clear();

            for (int i = 0; i <= count; i++)
            {
                table.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            }

            var fst = new ColumnStyle(SizeType.Percent, 33);
            table.ColumnStyles.Add(fst);
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));

            var index = 1;
            foreach (SyndicationItem item in feed.Items)
            {
                linkLabel1 = new LinkLabel
                {
                    Text = item.Title.Text,
                    Dock = DockStyle.Fill,
                    BorderStyle = BorderStyle.Fixed3D
                };
                linkLabel1.Links.Add(new LinkLabel.Link(0,linkLabel1.Text.Length,item.Links[0].Uri.ToString())); 
                
                var label1 = new Label
                {
                    Text = item.PublishDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    Dock = DockStyle.Fill,
                    BorderStyle = BorderStyle.Fixed3D
                };
                label2 = new Button
                {
                    Text = "Показать",
                    Dock = DockStyle.Fill,
                };

                table.Controls.Add(linkLabel1, 0, index);
                table.Controls.Add(label1, 1, index);
                table.Controls.Add(label2, 2, index);
                
                linkLabel1.LinkClicked += linkLabel1_LinkClicked;
                
                
                /*label2.MouseClick += (sender, args) 
                    => MessageBox.Show($"{item.Summary.Text}", "Подробное описание");*/
                label2.MouseClick += (sender, args)
                    => new Description(item.Summary.Text).Show();

                index++;
            }
           
            /*table.Controls.Add(new Panel(), 0, 0);
           table.Controls.Add(label, 0, 1);
           table.Controls.Add(box, 0, 2);
           table.Controls.Add(button, 0, 3);
           table.Controls.Add(new Panel(), 0, 4);*/

           
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = interval;
            timer1.Tick += timer1_Tick;
            timer1.Start();
           
            menuItem2.Click += (sender, args) =>
            {
                using (var frm = new InputBoxForm("Введите новую чатоту:"))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                        timer1.Interval = 1000 * int.Parse(frm.Input);
                }
            };
            table.Dock = DockStyle.Fill;
            Controls.Add(table);
            Controls.Add(mainMenu);
           
          // button.Click += (sender, args) => box.Text = (int.Parse(box.Text) + 1).ToString();
          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            while (table.Controls.Count > 0)
            {
                table.Controls[0].Dispose();
            }
            table.Controls.Clear();
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            var count = feed.Items.Count();
            var index = 1;
            foreach (SyndicationItem item in feed.Items)
            {
                linkLabel1 = new LinkLabel
                {
                    Text = item.Title.Text,
                    Dock = DockStyle.Fill,
                    BorderStyle = BorderStyle.Fixed3D
                };
                linkLabel1.Links.Add(new LinkLabel.Link(0,linkLabel1.Text.Length,item.Links[0].Uri.ToString())); 
                
                var label1 = new Label
                {
                    Text = item.PublishDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    Dock = DockStyle.Fill,
                    BorderStyle = BorderStyle.Fixed3D
                };
                label2 = new Button
                {
                    Text = "Показать",
                    Dock = DockStyle.Fill,
                };

                table.Controls.Add(linkLabel1, 0, index);
                table.Controls.Add(label1, 1, index);
                table.Controls.Add(label2, 2, index);
                
                linkLabel1.LinkClicked += linkLabel1_LinkClicked;
                
                
                /*label2.MouseClick += (sender, args) 
                    => MessageBox.Show($"{item.Summary.Text}", "Подробное описание");*/
                label2.MouseClick += (sender, args)
                    => new Description(item.Summary.Text).Show();

                index++;
            }
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var target = e.Link.LinkData as string;
            Process.Start(new ProcessStartInfo("cmd", $"/c start {target}"));
        }
        
        private void linkLabel1_LinkClicked(object sender, MouseEventArgs e)
        {
           
        }
    }
}
