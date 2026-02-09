using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace LabelAssist
{
    public partial class MainForm : Form
    {
        // Constructor
        public MainForm()
        {
            InitializeComponent();

            // Load all labels when form starts
            LoadLabels();

            // Attach event handler for search
            txtSearch.TextChanged += TxtSearch_TextChanged;
        }

        // Load all labels into the listbox
        private void LoadLabels()
        {
            lstLabels.Items.Clear();
            List<string> labels = LabelRepository.GetAll();

            // Apply search filter if there is text
            string filter = txtSearch.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(filter))
            {
                labels = labels.Where(l => l.ToLower().Contains(filter)).ToList();
            }

            lstLabels.Items.AddRange(labels.ToArray());
        }

        // Update label preview when selection changes
        private void lstLabels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstLabels.SelectedItem != null)
                lblPreview.Text = lstLabels.SelectedItem.ToString();
            else
                lblPreview.Text = "Preview";
        }

        // Search text changed
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadLabels();
        }

        // Create new label
        private void btnCreate_Click(object sender, EventArgs e)
        {
            string name = Prompt.ShowDialog("Enter label name:", "New Label");
            if (!string.IsNullOrWhiteSpace(name))
            {
                LabelRepository.Add(name.Trim());
                LoadLabels();
            }
        }

        // Edit selected label
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lstLabels.SelectedItem == null) return;

            string current = lstLabels.SelectedItem.ToString();
            string name = Prompt.ShowDialog("Edit label name:", "Edit Label", current);

            if (!string.IsNullOrWhiteSpace(name))
            {
                LabelRepository.Update(current, name.Trim());
                LoadLabels();
            }
        }

        // Delete selected label
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstLabels.SelectedItem == null) return;

            var result = MessageBox.Show(
                "Are you sure you want to delete this label?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                LabelRepository.Delete(lstLabels.SelectedItem.ToString());
                LoadLabels();
                lblPreview.Text = "Preview";
            }
        }

        // Print selected label
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (lstLabels.SelectedItem == null) return;

            PrintLabel(lstLabels.SelectedItem.ToString());
        }

        // Print logic with Windows dialog
        private void PrintLabel(string text)
        {
            PrintDocument pd = new PrintDocument();

            // Set custom label size: 50mm x 30mm
            pd.DefaultPageSettings.PaperSize = new PaperSize("Label", 197, 118); // hundredths of inch
            pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

            pd.PrintPage += (s, e) =>
            {
                using Font font = new Font("Arial", 14, FontStyle.Bold);
                StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                e.Graphics.DrawString(text.ToUpper(), font, Brushes.Black, e.PageBounds, format);
            };

            using PrintDialog dlg = new PrintDialog
            {
                Document = pd,
                UseEXDialog = true
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pd.Print();
            }
        }
    }
}
