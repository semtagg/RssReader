using System.Windows.Forms;

namespace RssReader
{
    partial class InputBoxForm
    {
        private void InitializeComponent(string message)
        {
            var label = new Label
            {
                Text = message,
                Dock = DockStyle.Fill
            };
            var box = new TextBox
            {
                Dock = DockStyle.Fill,
            };
            var button = new Button
            {
                Text = "Ок",
                Dock = DockStyle.Fill,
                DialogResult = DialogResult.OK
            };

            table = new TableLayoutPanel();
            table.RowStyles.Clear();
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            table.Controls.Add(new Panel(), 0, 0);
            table.Controls.Add(label, 0, 1);
            table.Controls.Add(box, 0, 2);
            table.Controls.Add(button, 0, 3);
            table.Controls.Add(new Panel(), 0, 5);

            table.Dock = DockStyle.Fill;
            Controls.Add(table);

            button.Click += (sender, args) => Input = box.Text;
        }
    }
}
