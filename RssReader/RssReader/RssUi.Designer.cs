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
    partial class RssUi
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        private DataGridView dataGridView;
        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DoubleBuffered = true;
            /*var table = new TableLayoutPanel();
            table.RowStyles.Clear();*/

            dataGridView = new DataGridView();
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells; 
            
            
            dataGridView.Columns.Add("Title", "Заголовок статьи"); 
            dataGridView.Columns.Add("Date", "Дата публикации");
            dataGridView.Columns.Add("Discription", "Описание статьи");
            dataGridView.Columns.Add(new DataGridViewLinkColumn()
                {
                    HeaderText = "Link",
                    Name = "https://www.youtube.com/watch?v=R_5LA7ihasQ&t=539s&ab_channel=ProgrammingWizardsTV",
                    Text = "Fuck",
                    UseColumnTextForLinkValue = true
                });
            dataGridView.CellContentClick += dataGridView_CellContentClick;
            
            dataGridView.Columns["Title"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView.Columns["Title"].Width = ClientSize.Width / 2;
            dataGridView.Columns["Date"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView.Columns["Date"].Width = ClientSize.Width / 10;

            string url = "https://habr.com/ru/rss/interesting/";
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            foreach (SyndicationItem item in feed.Items)
            {
                /*var dataGridViewRow = new DataGridViewRow();
                dataGridViewRow.CreateCells(dataGridView);
                dataGridViewRow.Cells[0] = new DataGridViewLinkCell()
                {
                    Value = item.Links
                };*/
                /*item.PublishDate.ToString("yy-MM-dd HH:mm:ss")*/
                dataGridView.Rows.Add( item.Title.Text, "https://habr.com/ru/rss/interesting/");
                
            }
            
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                linkCell.Value = row.Cells[1].Value;
                row.Cells[1] = linkCell;
            }

            var label = new Label
            {
                Text = "Enter a number",
                Dock = DockStyle.Fill
            };
            var box = new TextBox
            {
                Dock = DockStyle.Fill,
            };
            var button = new Button
            {
                Text = "Increment!",
                Dock = DockStyle.Fill
            };

            /*table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            table.Controls.Add(new Panel(), 0, 0);
            table.Controls.Add(label, 0, 1);
            table.Controls.Add(box, 0, 2);
            table.Controls.Add(button, 0, 3);
            table.Controls.Add(new Panel(), 0, 4);*/

            dataGridView.Dock = DockStyle.Fill;
            Controls.Add(dataGridView);

           //button.Click += (sender, args) => box.Text = (int.Parse(box.Text) + 1).ToString();
            
            SizeChanged += (sender, args) =>
            {
                dataGridView.Columns["Title"].Width = ClientSize.Width / 2;
                dataGridView.Columns["Date"].Width = ClientSize.Width / 3;

            };

            Load += (sender, args) => OnSizeChanged(EventArgs.Empty);
        }
        
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e) 
        {
            Process.Start("explorer", "http://google.com");
        }
    }
}
