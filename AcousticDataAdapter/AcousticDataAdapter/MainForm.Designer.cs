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
            this.FileChooserButton = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.channel0CheckBox = new System.Windows.Forms.CheckBox();
            this.channel1CheckBox = new System.Windows.Forms.CheckBox();
            this.channel2CheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
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
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Select a folder with WAV files";
            // 
            // channel0CheckBox
            // 
            this.channel0CheckBox.AutoSize = true;
            this.channel0CheckBox.Location = new System.Drawing.Point(12, 60);
            this.channel0CheckBox.Name = "channel0CheckBox";
            this.channel0CheckBox.Size = new System.Drawing.Size(106, 17);
            this.channel0CheckBox.TabIndex = 2;
            this.channel0CheckBox.Text = "Enable channel0";
            this.channel0CheckBox.UseVisualStyleBackColor = true;
            this.channel0CheckBox.CheckedChanged += new System.EventHandler(this.channel0CheckBox_CheckedChanged);
            // 
            // channel1CheckBox
            // 
            this.channel1CheckBox.AutoSize = true;
            this.channel1CheckBox.Location = new System.Drawing.Point(12, 95);
            this.channel1CheckBox.Name = "channel1CheckBox";
            this.channel1CheckBox.Size = new System.Drawing.Size(106, 17);
            this.channel1CheckBox.TabIndex = 3;
            this.channel1CheckBox.Text = "Enable channel1";
            this.channel1CheckBox.UseVisualStyleBackColor = true;
            this.channel1CheckBox.CheckedChanged += new System.EventHandler(this.Channel1CheckBox_CheckedChanged);
            // 
            // channel2CheckBox
            // 
            this.channel2CheckBox.AutoSize = true;
            this.channel2CheckBox.Location = new System.Drawing.Point(12, 129);
            this.channel2CheckBox.Name = "channel2CheckBox";
            this.channel2CheckBox.Size = new System.Drawing.Size(106, 17);
            this.channel2CheckBox.TabIndex = 4;
            this.channel2CheckBox.Text = "Enable channel2";
            this.channel2CheckBox.UseVisualStyleBackColor = true;
            this.channel2CheckBox.CheckedChanged += new System.EventHandler(this.Channel2CheckBox_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.channel2CheckBox);
            this.Controls.Add(this.channel1CheckBox);
            this.Controls.Add(this.channel0CheckBox);
            this.Controls.Add(this.FileChooserButton);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button FileChooserButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.CheckBox channel0CheckBox;
        private System.Windows.Forms.CheckBox channel1CheckBox;
        private System.Windows.Forms.CheckBox channel2CheckBox;
    }
}

