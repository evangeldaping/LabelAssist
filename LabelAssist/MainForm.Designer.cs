namespace LabelAssist
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtSearch = new TextBox();
            lstLabels = new ListBox();
            pnlPreview = new Panel();
            lblPreview = new Label();
            btnCreate = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnPrint = new Button();
            pnlPreview.SuspendLayout();
            SuspendLayout();
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearch.Location = new Point(20, 20);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search...";
            txtSearch.Size = new Size(746, 31);
            txtSearch.TabIndex = 0;
            // 
            // lstLabels
            // 
            lstLabels.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lstLabels.FormattingEnabled = true;
            lstLabels.ItemHeight = 25;
            lstLabels.Location = new Point(20, 60);
            lstLabels.Name = "lstLabels";
            lstLabels.Size = new Size(432, 304);
            lstLabels.TabIndex = 1;
            lstLabels.SelectedIndexChanged += lstLabels_SelectedIndexChanged_1;
            // 
            // pnlPreview
            // 
            pnlPreview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            pnlPreview.BorderStyle = BorderStyle.FixedSingle;
            pnlPreview.Controls.Add(lblPreview);
            pnlPreview.Location = new Point(464, 60);
            pnlPreview.Name = "pnlPreview";
            pnlPreview.Size = new Size(302, 364);
            pnlPreview.TabIndex = 2;
            // 
            // lblPreview
            // 
            lblPreview.AutoSize = true;
            lblPreview.Dock = DockStyle.Fill;
            lblPreview.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPreview.Location = new Point(0, 0);
            lblPreview.Name = "lblPreview";
            lblPreview.Size = new Size(154, 43);
            lblPreview.TabIndex = 0;
            lblPreview.Text = "Preview";
            lblPreview.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(20, 390);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(112, 34);
            btnCreate.TabIndex = 1;
            btnCreate.Text = "+ Create";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += this.button1_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(120, 390);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(112, 34);
            btnEdit.TabIndex = 2;
            btnEdit.Text = "✏ Edit";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(220, 390);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(112, 34);
            btnDelete.TabIndex = 1;
            btnDelete.Text = "🗑 Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += this.btnDelete_Click_1;
            // 
            // btnPrint
            // 
            btnPrint.Location = new Point(340, 390);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(112, 34);
            btnPrint.TabIndex = 1;
            btnPrint.Text = "🖨 Print";
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.Click += this.btnPrint_Click_1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(778, 454);
            Controls.Add(btnPrint);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(pnlPreview);
            Controls.Add(btnCreate);
            Controls.Add(lstLabels);
            Controls.Add(txtSearch);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LabelAssist - Product Label Printing";
            pnlPreview.ResumeLayout(false);
            pnlPreview.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ListBox lstLabels;
        private System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnPrint;
    }
}
