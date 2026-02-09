using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace LabelAssist
{
    public partial class MainForm : Form
    {
        private TextBox txtSearch;
        private ListBox lstLabels;
        private Panel pnlPreview;
        private Label lblPreview;
        private Button btnCreate;
        private Button btnDelete;
        private Button btnPrint;

        public MainForm()
        {
            InitializeComponent();
            LoadLabels();
        }

        // Load all labels from database
        private void LoadLabels()
        {
            lstLabels.Items.Clear();
            lstLabels.Items.AddRange(
                LabelRepository.GetAll().ToArray()
            );
        }

        // Update preview when selection changes
        private void lstLabels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstLabels.SelectedItem != null)
                lblPreview.Text = lstLabels.SelectedItem.ToString();
        }

        // Create new label
        private void btnCreate_Click(object sender, EventArgs e)
        {
            string name = Prompt.ShowDialog("Enter label name:", "New Label");

            if (!string.IsNullOrWhiteSpace(name))
            {
                LabelRepository.Add(name);
                LoadLabels();
            }
        }

        // Delete selected label
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstLabels.SelectedItem == null) return;

            LabelRepository.Delete(lstLabels.SelectedItem.ToString());
            LoadLabels();
            lblPreview.Text = "";
        }

        // Print selected label
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (lstLabels.SelectedItem == null) return;

            PrintLabel(lstLabels.SelectedItem.ToString());
        }

        // Print with Windows dialog (Google Docs style)
        private void PrintLabel(string text)
        {
            PrintDocument pd = new PrintDocument();

            // 50mm x 30mm label size
            pd.DefaultPageSettings.PaperSize =
                new PaperSize("Label", 197, 118);

            pd.DefaultPageSettings.Margins =
                new Margins(0, 0, 0, 0);

            pd.PrintPage += (s, e) =>
            {
                using Font font = new Font("Arial", 14, FontStyle.Bold);
                StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                e.Graphics.DrawString(
                    text.ToUpper(),
                    font,
                    Brushes.Black,
                    e.PageBounds,
                    format
                );
            };

            using PrintDialog dlg = new PrintDialog();
            dlg.Document = pd;
            dlg.UseEXDialog = true;

            if (dlg.ShowDialog() == DialogResult.OK)
                pd.Print();
        }
    }
}
