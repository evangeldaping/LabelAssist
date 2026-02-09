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

        private PrintDocument printDocument = new PrintDocument();
        private string labelToPrint = "";

        // Constructor
        public MainForm()
        {
            InitializeComponent();

            // Load all labels when form starts
            LoadLabels();

            // Attach event handler for search
            txtSearch.TextChanged += TxtSearch_TextChanged;

            printDocument.PrintPage += PrintDocument_PrintPage;
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
            // Make sure something is selected
            if (lstLabels.SelectedItem == null)
            {
                MessageBox.Show(
                    "Please select a label to edit.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Get selected label
            string oldName = lstLabels.SelectedItem.ToString();

            // Show prompt with pre-filled text
            string newName = Prompt.ShowDialog(
                "Edit Label",
                "Update label name:",
                oldName);

            // User cancelled or empty input
            if (string.IsNullOrWhiteSpace(newName) || newName == oldName)
                return;

            // Update database
            LabelRepository.Update(oldName, newName);

            // Reload list
            LoadLabels();

            // Reselect edited item
            lstLabels.SelectedItem = newName;
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
            if (lstLabels.SelectedItem == null)
            {
                MessageBox.Show(
                    "Please select a label to print.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            labelToPrint = lstLabels.SelectedItem.ToString();

            using PrintDialog dialog = new PrintDialog();
            dialog.Document = printDocument;
            dialog.AllowSomePages = false;
            dialog.AllowSelection = false;

            // This opens the native Windows print dialog
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        // Print logic with Windows dialog
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            // Label area (adjust to your sticker size)
            Size labelSize = GetLabelSizeInPixels(e.Graphics);
            Rectangle labelArea = new Rectangle(50, 50, labelSize.Width, labelSize.Height);

            // Draw border (optional)
            g.DrawRectangle(Pens.Black, labelArea);

            // Font settings
            using Font font = new Font("Arial", 16, FontStyle.Bold);
            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // Draw label text
            g.DrawString(
                labelToPrint,
                font,
                Brushes.Black,
                labelArea,
                format
            );

            e.HasMorePages = false;
        }

        private Size GetLabelSizeInPixels(Graphics g)
        {
            float dpiX = g.DpiX;
            float dpiY = g.DpiY;

            return cmbLabelSize.SelectedIndex switch
            {
                0 => new Size(
                    (int)(50 / 25.4f * dpiX),
                    (int)(25 / 25.4f * dpiY)),

                1 => new Size(
                    (int)(100 / 25.4f * dpiX),
                    (int)(50 / 25.4f * dpiY)),

                _ => g.VisibleClipBounds.Size.ToSize()
            };
        }

    }
}
