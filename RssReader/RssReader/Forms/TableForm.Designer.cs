using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace RssReader
{
    partial class TableForm
    {
        private void ShowTable()
        {
            var items = rssFeed.Feed.Items.ToArray(); // to array
            for (var i = 0; i < items.Length; i++)
            {
                var title = new LinkLabel
                {
                    Text = items[i].Title.Text,
                    Dock = DockStyle.Fill,
                    BorderStyle = BorderStyle.Fixed3D
                };
                title.Links.Add(new LinkLabel.Link(0, title.Text.Length, items[i].Links[0].Uri.ToString()));
                title.LinkClicked += (sender, args) =>
                {
                    var target = args.Link.LinkData as string;
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {target}"));
                };

                var date = new Label
                {
                    Text = items[i].PublishDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    Dock = DockStyle.Fill,
                    BorderStyle = BorderStyle.Fixed3D
                };
                
                var description = new Button
                {
                    Text = "Показать",
                    Dock = DockStyle.Fill,
                    TabIndex = i
                };
                description.MouseClick += (sender, args) =>
                {
                    var y = sender as Button;
                    new Description(items[y.TabIndex].Summary.Text).Show();
                };

                table.Controls.Add(title, 0, i);
                table.Controls.Add(date, 1, i);
                table.Controls.Add(description, 2, i);
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
                    {
                        if (int.TryParse(frm.Input, out var temp))
                            timer.Interval = rssFeed.Interval = 1000 * 60 * temp;
                    }
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

            var count = rssFeed.Feed.Items.Count();
            for (int i = 0; i <= count; i++)
            {
                table.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            }

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
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
