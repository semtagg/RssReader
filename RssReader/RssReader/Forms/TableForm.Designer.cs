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
        private void ShowTable()
        {
            var index = 1;
            foreach (SyndicationItem item in rssFeed.Feed.Items)
            {
                linkLabel = new LinkLabel
                {
                    Text = item.Title.Text,
                    Dock = DockStyle.Fill,
                    BorderStyle = BorderStyle.Fixed3D
                };
                linkLabel.Links.Add(new LinkLabel.Link(0,linkLabel.Text.Length,item.Links[0].Uri.ToString())); 
                
                var label1 = new Label
                {
                    Text = item.PublishDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    Dock = DockStyle.Fill,
                    BorderStyle = BorderStyle.Fixed3D
                };
                var label2 = new Button
                {
                    Text = "Показать",
                    Dock = DockStyle.Fill,
                };

                table.Controls.Add(linkLabel, 0, index);
                table.Controls.Add(label1, 1, index);
                table.Controls.Add(label2, 2, index);

                linkLabel.LinkClicked += (sender, args) =>
                {
                    var target = args.Link.LinkData as string;
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {target}"));
                };


                label2.MouseClick += (sender, args)
                    => new Description(item.Summary.Text).Show();

                index++;
            }
        }

        private void InitializeMainMenu()
        {
            mainMenu = new MenuStrip();
            mainMenu.Location = new Point(0, 0);

            var editUrl = new ToolStripMenuItem()
            {
                Text = "Изменить ленту",
                Tag = "EditUrl"
            };
            editUrl.Click += (sender, args) =>
            {
                using (var frm = new InputBoxForm("Введите ссылку:"))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                        rssFeed.Url = frm.Input;
                }
            };


            var editInterval = new ToolStripMenuItem()
            {
                Text = "Изменить частоту обновления",
                Tag = "EditInterval"
            };
            editInterval.Click += (sender, args) =>
            {
                using (var frm = new InputBoxForm("Введите новую чатоту:"))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                        timer.Interval = 1000 * int.Parse(frm.Input);
                }
            };

            var addFeed = new ToolStripMenuItem()
            {
                Text = "Добавить ленту",
                Tag = "AddFeed"
            };
            addFeed.Click += (sender, args) =>
            {
                var result = MessageBox.Show("Добавить ленту?", "", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                    new TableForm().Show();
            };

            mainMenu.Items.Add(editUrl);
            mainMenu.Items.Add(editInterval);
            mainMenu.Items.Add(addFeed);

            Controls.Add(mainMenu);
        }

        private void InitializeTable()
        {
            rssFeed = new RssFeed();

            table = new TableLayoutPanel();
            table.AutoScroll = true;
            table.RowStyles.Clear();

            for (int i = 0; i <= rssFeed.Feed.Items.Count(); i++)
            {
                table.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            }

            var fst = new ColumnStyle(SizeType.Percent, 33);
            table.ColumnStyles.Add(fst);
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));

            ShowTable();

            table.Dock = DockStyle.Fill;
            Controls.Add(table);
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = rssFeed.Interval;
            timer.Tick += (sender, args) =>
            {
                while (table.Controls.Count > 0)
                {
                    table.Controls[0].Dispose();
                }
                table.Controls.Clear();
            
                ShowTable();
            };
            timer.Start();
        }
        
        private void InitializeComponent()
        {
            InitializeMainMenu();
            InitializeTable();
            InitializeTimer();
        }
    }
}
