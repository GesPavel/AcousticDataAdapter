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
            this.channel0Checkbox = new System.Windows.Forms.CheckBox();
            this.channel1Checkbox = new System.Windows.Forms.CheckBox();
            this.channel2Checkbox = new System.Windows.Forms.CheckBox();
            this.beginDateLabel = new System.Windows.Forms.Label();
            this.yearLabel = new System.Windows.Forms.Label();
            this.monthLabel = new System.Windows.Forms.Label();
            this.dayLabel = new System.Windows.Forms.Label();
            this.hourLabel = new System.Windows.Forms.Label();
            this.minuteLabel = new System.Windows.Forms.Label();
            this.secondLabel = new System.Windows.Forms.Label();
            this.endDateLabel = new System.Windows.Forms.Label();
            this.beginYearTextbox = new System.Windows.Forms.TextBox();
            this.sepLabel1 = new System.Windows.Forms.Label();
            this.sepLabel2 = new System.Windows.Forms.Label();
            this.beginMonthTextbox = new System.Windows.Forms.TextBox();
            this.beginDayTextbox = new System.Windows.Forms.TextBox();
            this.beginHourTextbox = new System.Windows.Forms.TextBox();
            this.sepLabel3 = new System.Windows.Forms.Label();
            this.beginMinuteTextbox = new System.Windows.Forms.TextBox();
            this.sepLabel4 = new System.Windows.Forms.Label();
            this.beginSecondTextbox = new System.Windows.Forms.TextBox();
            this.endSecondTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.endMinuteTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.endHourTextbox = new System.Windows.Forms.TextBox();
            this.endDayTextbox = new System.Windows.Forms.TextBox();
            this.endMonthTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.endYearTextbox = new System.Windows.Forms.TextBox();
            this.compressCheckbox = new System.Windows.Forms.CheckBox();
            this.integrityCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // FileChooserButton
            // 
            this.FileChooserButton.Location = new System.Drawing.Point(323, 340);
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
            // channel0Checkbox
            // 
            this.channel0Checkbox.AutoSize = true;
            this.channel0Checkbox.Checked = true;
            this.channel0Checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.channel0Checkbox.Location = new System.Drawing.Point(12, 60);
            this.channel0Checkbox.Name = "channel0Checkbox";
            this.channel0Checkbox.Size = new System.Drawing.Size(106, 17);
            this.channel0Checkbox.TabIndex = 2;
            this.channel0Checkbox.Text = "Enable channel0";
            this.channel0Checkbox.UseVisualStyleBackColor = true;
            this.channel0Checkbox.CheckedChanged += new System.EventHandler(this.channel0CheckBox_CheckedChanged);
            // 
            // channel1Checkbox
            // 
            this.channel1Checkbox.AutoSize = true;
            this.channel1Checkbox.Checked = true;
            this.channel1Checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.channel1Checkbox.Location = new System.Drawing.Point(12, 95);
            this.channel1Checkbox.Name = "channel1Checkbox";
            this.channel1Checkbox.Size = new System.Drawing.Size(106, 17);
            this.channel1Checkbox.TabIndex = 3;
            this.channel1Checkbox.Text = "Enable channel1";
            this.channel1Checkbox.UseVisualStyleBackColor = true;
            this.channel1Checkbox.CheckedChanged += new System.EventHandler(this.Channel1CheckBox_CheckedChanged);
            // 
            // channel2Checkbox
            // 
            this.channel2Checkbox.AutoSize = true;
            this.channel2Checkbox.Checked = true;
            this.channel2Checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.channel2Checkbox.Location = new System.Drawing.Point(12, 129);
            this.channel2Checkbox.Name = "channel2Checkbox";
            this.channel2Checkbox.Size = new System.Drawing.Size(106, 17);
            this.channel2Checkbox.TabIndex = 4;
            this.channel2Checkbox.Text = "Enable channel2";
            this.channel2Checkbox.UseVisualStyleBackColor = true;
            this.channel2Checkbox.CheckedChanged += new System.EventHandler(this.Channel2CheckBox_CheckedChanged);
            // 
            // beginDateLabel
            // 
            this.beginDateLabel.AutoSize = true;
            this.beginDateLabel.Location = new System.Drawing.Point(216, 90);
            this.beginDateLabel.Name = "beginDateLabel";
            this.beginDateLabel.Size = new System.Drawing.Size(88, 13);
            this.beginDateLabel.TabIndex = 5;
            this.beginDateLabel.Text = "Enter begin date:";
            // 
            // yearLabel
            // 
            this.yearLabel.AutoSize = true;
            this.yearLabel.Location = new System.Drawing.Point(320, 61);
            this.yearLabel.Name = "yearLabel";
            this.yearLabel.Size = new System.Drawing.Size(21, 13);
            this.yearLabel.TabIndex = 6;
            this.yearLabel.Text = "YY";
            // 
            // monthLabel
            // 
            this.monthLabel.AutoSize = true;
            this.monthLabel.Location = new System.Drawing.Point(405, 61);
            this.monthLabel.Name = "monthLabel";
            this.monthLabel.Size = new System.Drawing.Size(25, 13);
            this.monthLabel.TabIndex = 7;
            this.monthLabel.Text = "MM";
            // 
            // dayLabel
            // 
            this.dayLabel.AutoSize = true;
            this.dayLabel.Location = new System.Drawing.Point(458, 60);
            this.dayLabel.Name = "dayLabel";
            this.dayLabel.Size = new System.Drawing.Size(23, 13);
            this.dayLabel.TabIndex = 8;
            this.dayLabel.Text = "DD";
            // 
            // hourLabel
            // 
            this.hourLabel.AutoSize = true;
            this.hourLabel.Location = new System.Drawing.Point(523, 61);
            this.hourLabel.Name = "hourLabel";
            this.hourLabel.Size = new System.Drawing.Size(19, 13);
            this.hourLabel.TabIndex = 9;
            this.hourLabel.Text = "hh";
            // 
            // minuteLabel
            // 
            this.minuteLabel.AutoSize = true;
            this.minuteLabel.Location = new System.Drawing.Point(576, 60);
            this.minuteLabel.Name = "minuteLabel";
            this.minuteLabel.Size = new System.Drawing.Size(23, 13);
            this.minuteLabel.TabIndex = 10;
            this.minuteLabel.Text = "mm";
            // 
            // secondLabel
            // 
            this.secondLabel.AutoSize = true;
            this.secondLabel.Location = new System.Drawing.Point(629, 60);
            this.secondLabel.Name = "secondLabel";
            this.secondLabel.Size = new System.Drawing.Size(17, 13);
            this.secondLabel.TabIndex = 11;
            this.secondLabel.Text = "ss";
            // 
            // endDateLabel
            // 
            this.endDateLabel.AutoSize = true;
            this.endDateLabel.Location = new System.Drawing.Point(216, 119);
            this.endDateLabel.Name = "endDateLabel";
            this.endDateLabel.Size = new System.Drawing.Size(80, 13);
            this.endDateLabel.TabIndex = 12;
            this.endDateLabel.Text = "Enter end date:";
            // 
            // beginYearTextbox
            // 
            this.beginYearTextbox.Location = new System.Drawing.Point(323, 83);
            this.beginYearTextbox.Name = "beginYearTextbox";
            this.beginYearTextbox.Size = new System.Drawing.Size(63, 20);
            this.beginYearTextbox.TabIndex = 22;
            this.beginYearTextbox.Text = "2021";
            this.beginYearTextbox.TextChanged += new System.EventHandler(this.BeginYearTextBox_TextChanged);
            // 
            // sepLabel1
            // 
            this.sepLabel1.AutoSize = true;
            this.sepLabel1.Location = new System.Drawing.Point(392, 86);
            this.sepLabel1.Name = "sepLabel1";
            this.sepLabel1.Size = new System.Drawing.Size(10, 13);
            this.sepLabel1.TabIndex = 23;
            this.sepLabel1.Text = "-";
            // 
            // sepLabel2
            // 
            this.sepLabel2.AutoSize = true;
            this.sepLabel2.Location = new System.Drawing.Point(445, 86);
            this.sepLabel2.Name = "sepLabel2";
            this.sepLabel2.Size = new System.Drawing.Size(10, 13);
            this.sepLabel2.TabIndex = 24;
            this.sepLabel2.Text = "-";
            // 
            // beginMonthTextbox
            // 
            this.beginMonthTextbox.Location = new System.Drawing.Point(408, 83);
            this.beginMonthTextbox.Name = "beginMonthTextbox";
            this.beginMonthTextbox.Size = new System.Drawing.Size(31, 20);
            this.beginMonthTextbox.TabIndex = 25;
            this.beginMonthTextbox.Text = "06";
            this.beginMonthTextbox.TextChanged += new System.EventHandler(this.BeginMonthTextBox_TextChanged);
            // 
            // beginDayTextbox
            // 
            this.beginDayTextbox.Location = new System.Drawing.Point(461, 83);
            this.beginDayTextbox.Name = "beginDayTextbox";
            this.beginDayTextbox.Size = new System.Drawing.Size(31, 20);
            this.beginDayTextbox.TabIndex = 26;
            this.beginDayTextbox.Text = "06";
            this.beginDayTextbox.TextChanged += new System.EventHandler(this.BeginDayTextBox_TextChanged);
            // 
            // beginHourTextbox
            // 
            this.beginHourTextbox.Location = new System.Drawing.Point(526, 83);
            this.beginHourTextbox.Name = "beginHourTextbox";
            this.beginHourTextbox.Size = new System.Drawing.Size(31, 20);
            this.beginHourTextbox.TabIndex = 27;
            this.beginHourTextbox.Text = "00";
            this.beginHourTextbox.TextChanged += new System.EventHandler(this.BeginHourTextbox_TextChanged);
            // 
            // sepLabel3
            // 
            this.sepLabel3.AutoSize = true;
            this.sepLabel3.Location = new System.Drawing.Point(563, 86);
            this.sepLabel3.Name = "sepLabel3";
            this.sepLabel3.Size = new System.Drawing.Size(10, 13);
            this.sepLabel3.TabIndex = 28;
            this.sepLabel3.Text = ":";
            // 
            // beginMinuteTextbox
            // 
            this.beginMinuteTextbox.Location = new System.Drawing.Point(579, 83);
            this.beginMinuteTextbox.Name = "beginMinuteTextbox";
            this.beginMinuteTextbox.Size = new System.Drawing.Size(31, 20);
            this.beginMinuteTextbox.TabIndex = 29;
            this.beginMinuteTextbox.Text = "00";
            this.beginMinuteTextbox.TextChanged += new System.EventHandler(this.BeginMinuteTextbox_TextChanged);
            // 
            // sepLabel4
            // 
            this.sepLabel4.AutoSize = true;
            this.sepLabel4.Location = new System.Drawing.Point(616, 86);
            this.sepLabel4.Name = "sepLabel4";
            this.sepLabel4.Size = new System.Drawing.Size(10, 13);
            this.sepLabel4.TabIndex = 30;
            this.sepLabel4.Text = ":";
            // 
            // beginSecondTextbox
            // 
            this.beginSecondTextbox.Location = new System.Drawing.Point(632, 83);
            this.beginSecondTextbox.Name = "beginSecondTextbox";
            this.beginSecondTextbox.Size = new System.Drawing.Size(31, 20);
            this.beginSecondTextbox.TabIndex = 31;
            this.beginSecondTextbox.Text = "00";
            this.beginSecondTextbox.TextChanged += new System.EventHandler(this.BeginSecondTextBox_TextChanged);
            // 
            // endSecondTextbox
            // 
            this.endSecondTextbox.Location = new System.Drawing.Point(632, 116);
            this.endSecondTextbox.Name = "endSecondTextbox";
            this.endSecondTextbox.Size = new System.Drawing.Size(31, 20);
            this.endSecondTextbox.TabIndex = 41;
            this.endSecondTextbox.Text = "00";
            this.endSecondTextbox.TextChanged += new System.EventHandler(this.EndSecondTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(616, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = ":";
            // 
            // endMinuteTextbox
            // 
            this.endMinuteTextbox.Location = new System.Drawing.Point(579, 116);
            this.endMinuteTextbox.Name = "endMinuteTextbox";
            this.endMinuteTextbox.Size = new System.Drawing.Size(31, 20);
            this.endMinuteTextbox.TabIndex = 39;
            this.endMinuteTextbox.Text = "00";
            this.endMinuteTextbox.TextChanged += new System.EventHandler(this.EndMinuteTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(563, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = ":";
            // 
            // endHourTextbox
            // 
            this.endHourTextbox.Location = new System.Drawing.Point(526, 116);
            this.endHourTextbox.Name = "endHourTextbox";
            this.endHourTextbox.Size = new System.Drawing.Size(31, 20);
            this.endHourTextbox.TabIndex = 37;
            this.endHourTextbox.Text = "00";
            this.endHourTextbox.TextChanged += new System.EventHandler(this.EndHourTextBox_TextChanged);
            // 
            // endDayTextbox
            // 
            this.endDayTextbox.Location = new System.Drawing.Point(461, 116);
            this.endDayTextbox.Name = "endDayTextbox";
            this.endDayTextbox.Size = new System.Drawing.Size(31, 20);
            this.endDayTextbox.TabIndex = 36;
            this.endDayTextbox.Text = "06";
            this.endDayTextbox.TextChanged += new System.EventHandler(this.EndDayTextBox_TextChanged);
            // 
            // endMonthTextbox
            // 
            this.endMonthTextbox.Location = new System.Drawing.Point(408, 116);
            this.endMonthTextbox.Name = "endMonthTextbox";
            this.endMonthTextbox.Size = new System.Drawing.Size(31, 20);
            this.endMonthTextbox.TabIndex = 35;
            this.endMonthTextbox.Text = "06";
            this.endMonthTextbox.TextChanged += new System.EventHandler(this.EndMonthTextbox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(445, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "-";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(392, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "-";
            // 
            // endYearTextbox
            // 
            this.endYearTextbox.Location = new System.Drawing.Point(323, 116);
            this.endYearTextbox.Name = "endYearTextbox";
            this.endYearTextbox.Size = new System.Drawing.Size(63, 20);
            this.endYearTextbox.TabIndex = 32;
            this.endYearTextbox.Text = "2021";
            this.endYearTextbox.TextChanged += new System.EventHandler(this.EndYearTextbox_TextChanged);
            // 
            // compressCheckbox
            // 
            this.compressCheckbox.AutoSize = true;
            this.compressCheckbox.Checked = true;
            this.compressCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.compressCheckbox.Location = new System.Drawing.Point(12, 205);
            this.compressCheckbox.Name = "compressCheckbox";
            this.compressCheckbox.Size = new System.Drawing.Size(121, 17);
            this.compressCheckbox.TabIndex = 42;
            this.compressCheckbox.Text = "Compress output file";
            this.compressCheckbox.UseVisualStyleBackColor = true;
            this.compressCheckbox.CheckedChanged += new System.EventHandler(this.CompressCheckbox_CheckedChanged);
            // 
            // integrityCheckbox
            // 
            this.integrityCheckbox.AutoSize = true;
            this.integrityCheckbox.Checked = true;
            this.integrityCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.integrityCheckbox.Location = new System.Drawing.Point(12, 246);
            this.integrityCheckbox.Name = "integrityCheckbox";
            this.integrityCheckbox.Size = new System.Drawing.Size(135, 17);
            this.integrityCheckbox.TabIndex = 43;
            this.integrityCheckbox.Text = "Check for data integrity";
            this.integrityCheckbox.UseVisualStyleBackColor = true;
            this.integrityCheckbox.CheckedChanged += new System.EventHandler(this.IntegrityCheckbox_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.integrityCheckbox);
            this.Controls.Add(this.compressCheckbox);
            this.Controls.Add(this.endSecondTextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.endMinuteTextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.endHourTextbox);
            this.Controls.Add(this.endDayTextbox);
            this.Controls.Add(this.endMonthTextbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.endYearTextbox);
            this.Controls.Add(this.beginSecondTextbox);
            this.Controls.Add(this.sepLabel4);
            this.Controls.Add(this.beginMinuteTextbox);
            this.Controls.Add(this.sepLabel3);
            this.Controls.Add(this.beginHourTextbox);
            this.Controls.Add(this.beginDayTextbox);
            this.Controls.Add(this.beginMonthTextbox);
            this.Controls.Add(this.sepLabel2);
            this.Controls.Add(this.sepLabel1);
            this.Controls.Add(this.beginYearTextbox);
            this.Controls.Add(this.endDateLabel);
            this.Controls.Add(this.secondLabel);
            this.Controls.Add(this.minuteLabel);
            this.Controls.Add(this.hourLabel);
            this.Controls.Add(this.dayLabel);
            this.Controls.Add(this.monthLabel);
            this.Controls.Add(this.yearLabel);
            this.Controls.Add(this.beginDateLabel);
            this.Controls.Add(this.channel2Checkbox);
            this.Controls.Add(this.channel1Checkbox);
            this.Controls.Add(this.channel0Checkbox);
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
        private System.Windows.Forms.CheckBox channel0Checkbox;
        private System.Windows.Forms.CheckBox channel1Checkbox;
        private System.Windows.Forms.CheckBox channel2Checkbox;
        private System.Windows.Forms.Label beginDateLabel;
        private System.Windows.Forms.Label yearLabel;
        private System.Windows.Forms.Label monthLabel;
        private System.Windows.Forms.Label dayLabel;
        private System.Windows.Forms.Label hourLabel;
        private System.Windows.Forms.Label minuteLabel;
        private System.Windows.Forms.Label secondLabel;
        private System.Windows.Forms.Label endDateLabel;
        private System.Windows.Forms.TextBox beginYearTextbox;
        private System.Windows.Forms.Label sepLabel1;
        private System.Windows.Forms.Label sepLabel2;
        private System.Windows.Forms.TextBox beginMonthTextbox;
        private System.Windows.Forms.TextBox beginDayTextbox;
        private System.Windows.Forms.TextBox beginHourTextbox;
        private System.Windows.Forms.Label sepLabel3;
        private System.Windows.Forms.TextBox beginMinuteTextbox;
        private System.Windows.Forms.Label sepLabel4;
        private System.Windows.Forms.TextBox beginSecondTextbox;
        private System.Windows.Forms.TextBox endSecondTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox endMinuteTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox endHourTextbox;
        private System.Windows.Forms.TextBox endDayTextbox;
        private System.Windows.Forms.TextBox endMonthTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox endYearTextbox;
        private System.Windows.Forms.CheckBox compressCheckbox;
        private System.Windows.Forms.CheckBox integrityCheckbox;
    }
}

