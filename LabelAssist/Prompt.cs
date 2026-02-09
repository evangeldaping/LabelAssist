using System.Windows.Forms;

namespace LabelAssist
{
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 150,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            Label lblText = new Label()
            {
                Left = 20,
                Top = 20,
                Text = text,
                AutoSize = true
            };

            TextBox input = new TextBox()
            {
                Left = 20,
                Top = 50,
                Width = 340
            };

            Button ok = new Button()
            {
                Text = "OK",
                Left = 280,
                Width = 80,
                Top = 80,
                DialogResult = DialogResult.OK
            };

            prompt.Controls.Add(lblText);
            prompt.Controls.Add(input);
            prompt.Controls.Add(ok);
            prompt.AcceptButton = ok;

            return prompt.ShowDialog() == DialogResult.OK
                ? input.Text
                : "";
        }
    }
}
