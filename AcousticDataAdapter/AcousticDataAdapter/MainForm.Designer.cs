namespace AcousticDataAdapter
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.FileChooserButton = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(298, 80);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Hello World";
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Select a folder with WAV files";
            // 
            // FileChooserButton
            // 
            this.FileChooserButton.Location = new System.Drawing.Point(298, 326);
            this.FileChooserButton.Name = "FileChooserButton";
            this.FileChooserButton.Size = new System.Drawing.Size(100, 23);
            this.FileChooserButton.TabIndex = 1;
            this.FileChooserButton.Text = "Choose File";
            this.FileChooserButton.UseVisualStyleBackColor = true;
            this.FileChooserButton.Click += new System.EventHandler(this.FileChooserButton_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Text File|*.txt";
            this.saveFileDialog.Title = "Save an Image File";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.FileChooserButton);
            this.Controls.Add(this.textBox1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button FileChooserButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}

