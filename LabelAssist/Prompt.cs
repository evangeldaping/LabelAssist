using System;
using System.Drawing;
using System.Windows.Forms;

namespace LabelAssist
{
    public static class Prompt
    {
        /// <summary>
        /// Shows an input dialog with optional pre-filled text.
        /// Returns the entered string or null if cancelled.
        /// </summary>
        public static string ShowDialog(
            string title,
            string labelText,
            string defaultText = "")
        {
            // Create form
            Form prompt = new Form()
            {
                Width = 400,
                Height = 160,
                Text = title,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            // Label text
            Label textLabel = new Label()
            {
                Left = 10,
                Top = 15,
                Width = 360,
                Text = labelText
            };

            // Input textbox (pre-filled)
            TextBox inputBox = new TextBox()
            {
                Left = 10,
                Top = 40,
                Width = 360,
                Text = defaultText
            };

            // OK button
            Button confirmation = new Button()
            {
                Text = "OK",
                Left = 210,
                Width = 75,
                Top = 80,
                DialogResult = DialogResult.OK
            };

            // Cancel button
            Button cancel = new Button()
            {
                Text = "Cancel",
                Left = 295,
                Width = 75,
                Top = 80,
                DialogResult = DialogResult.Cancel
            };

            // Add controls
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);

            // Default buttons
            prompt.AcceptButton = confirmation;
            prompt.CancelButton = cancel;

            // Focus textbox and select text
            prompt.Shown += (sender, e) =>
            {
                inputBox.Focus();
                inputBox.SelectAll();
            };

            // Show dialog
            return prompt.ShowDialog() == DialogResult.OK
                ? inputBox.Text.Trim()
                : null;
        }
    }
}
