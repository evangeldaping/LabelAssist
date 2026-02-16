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
        private int copiesRemaining = 1;

        public MainForm()
        {
            InitializeComponent();

            LoadLabels();

            txtSearch.TextChanged += TxtSearch_TextChanged;
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        private void LoadLabels()
        {
            lstLabels.Items.Clear();
            List<string> labels = LabelRepository.GetAll();

            string filter = txtSearch.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(filter))
            {
                labels = labels.Where(l => l.ToLower().Contains(filter)).ToList();
            }

            lstLabels.Items.AddRange(labels.ToArray());
        }

        private void lstLabels_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPreview.Text = lstLabels.SelectedItem?.ToString() ?? "Preview";
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadLabels();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string name = Prompt.ShowDialog("New Label", "Enter label name:");
            if (!string.IsNullOrWhiteSpace(name))
            {
                LabelRepository.Add(name.Trim());
                LoadLabels();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lstLabels.SelectedItem == null)
            {
                MessageBox.Show("Please select a label to edit.");
                return;
            }

            string oldName = lstLabels.SelectedItem.ToString();

            string newName = Prompt.ShowDialog(
                "Edit Label",
                "Update label name:",
                oldName);

            if (string.IsNullOrWhiteSpace(newName) || newName == oldName)
                return;

            LabelRepository.Update(oldName, newName);
            LoadLabels();
            lstLabels.SelectedItem = newName;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstLabels.SelectedItem == null) return;

            if (MessageBox.Show(
                "Are you sure you want to delete this label?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                LabelRepository.Delete(lstLabels.SelectedItem.ToString());
                LoadLabels();
                lblPreview.Text = "Preview";
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (lstLabels.SelectedItem == null)
            {
                MessageBox.Show("Please select a label to print.");
                return;
            }

            labelToPrint = lstLabels.SelectedItem.ToString();
            copiesRemaining = (int)numCopies.Value;

            // CRITICAL for Brother QL
            printDocument.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
            printDocument.OriginAtMargins = false;

            using PrintDialog dialog = new PrintDialog
            {
                Document = printDocument
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            Size labelSize = GetLabelSizeInPixels(g);

            Rectangle labelArea = new Rectangle(
                0,
                0,
                labelSize.Width,
                labelSize.Height);

            using Font font = new Font("Arial", 16, FontStyle.Bold);
            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            g.DrawString(
                labelToPrint,
                font,
                Brushes.Black,
                labelArea,
                format);

            copiesRemaining--;
            e.HasMorePages = copiesRemaining > 0;
        }

        private Size GetLabelSizeInPixels(Graphics g)
        {
            float dpiX = g.DpiX;
            float dpiY = g.DpiY;

            // Brother QL-570 62mm tape (dynamic height)
            if (cmbLabelSize.SelectedIndex == 0)
            {
                int widthPx = (int)(62 / 25.4f * dpiX);

                using Font font = new Font("Arial", 16, FontStyle.Bold);
                SizeF textSize = g.MeasureString(labelToPrint, font);

                int heightPx = (int)textSize.Height + 40;

                return new Size(widthPx, heightPx);
            }

            // 50 x 25 mm
            if (cmbLabelSize.SelectedIndex == 1)
            {
                return new Size(
                    (int)(50 / 25.4f * dpiX),
                    (int)(25 / 25.4f * dpiY));
            }

            // 100 x 50 mm
            return new Size(
                (int)(100 / 25.4f * dpiX),
                (int)(50 / 25.4f * dpiY));
        }

        private void pnlPreview_Paint(object sender, PaintEventArgs e)
        {
            if (lstLabels.SelectedItem == null) return;

            Graphics g = e.Graphics;
            g.Clear(Color.White);

            Rectangle rect = new Rectangle(10, 10, pnlPreview.Width - 20, pnlPreview.Height - 20);
            g.DrawRectangle(Pens.Black, rect);

            using Font font = new Font("Arial", 12, FontStyle.Bold);
            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            g.DrawString(
                lstLabels.SelectedItem.ToString(),
                font,
                Brushes.Black,
                rect,
                format);
        }

    }
}
