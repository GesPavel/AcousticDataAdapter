using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Windows.Forms;

namespace AcousticDataAdapter
{
    public partial class MainForm : Form
    {
        MainLogic mainLogic;
        public MainForm()
        {
            InitializeComponent();
            mainLogic = new MainLogic();
            mainLogic.ConvertChannel0 = channel0Checkbox.Checked;
            mainLogic.ConvertChannel1 = channel1Checkbox.Checked;
            mainLogic.ConvertChannel2 = channel2Checkbox.Checked;
            mainLogic.CompressTextFile = compressCheckbox.Checked;
            mainLogic.CheckForIntegrity = integrityCheckbox.Checked;

            mainLogic.Begin.Year = beginYearTextbox.Text;
            mainLogic.Begin.Month = beginMonthTextbox.Text;
            mainLogic.Begin.Day = beginDayTextbox.Text;
            mainLogic.Begin.Hour = beginHourTextbox.Text;
            mainLogic.Begin.Minute = beginMinuteTextbox.Text;
            mainLogic.Begin.Second = beginSecondTextbox.Text;


            mainLogic.End.Year = endYearTextbox.Text;
            mainLogic.End.Month = endMonthTextbox.Text;
            mainLogic.End.Day = endDayTextbox.Text;
            mainLogic.End.Hour = endHourTextbox.Text;
            mainLogic.End.Minute = endMinuteTextbox.Text;
            mainLogic.End.Second = endSecondTextbox.Text;

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private async void FileChooserButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                
                    
                    var filePath = folderBrowserDialog.SelectedPath;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.FileName != "")
                    {
                        var filePath2 = saveFileDialog.FileName;


                        conversionProgressBar.Visible = true;
                        conversionProgressBar.Style = ProgressBarStyle.Marquee;
                        FileChooserButton.Enabled = false;
                        await Task.Run(() => {
                            try
                            {
                                mainLogic.OpenFolderAndSaveConversionResult(filePath, filePath2);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message.ToString(), "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        });
                        FileChooserButton.Enabled = true;
                        conversionProgressBar.Visible = false;
                    }
            }
        }


        private void Channel1CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            mainLogic.ConvertChannel1 = channel1Checkbox.Checked;
        }

        private void Channel2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            mainLogic.ConvertChannel2 = channel2Checkbox.Checked;
        }

        private void channel0CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            mainLogic.ConvertChannel0 = channel0Checkbox.Checked;
        }

        private void CompressCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            mainLogic.CompressTextFile = compressCheckbox.Checked;
        }

        private void BeginYearTextBox_TextChanged(object sender, EventArgs e)
        {
            mainLogic.Begin.Year = beginYearTextbox.Text;
        }

        private void BeginMonthTextBox_TextChanged(object sender, EventArgs e)
        {
            mainLogic.Begin.Month = beginMonthTextbox.Text;
        }

        private void BeginDayTextBox_TextChanged(object sender, EventArgs e)
        {
            mainLogic.Begin.Day = beginDayTextbox.Text;
        }

        private void BeginHourTextbox_TextChanged(object sender, EventArgs e)
        {
            mainLogic.Begin.Hour = beginHourTextbox.Text;
        }

        private void BeginMinuteTextbox_TextChanged(object sender, EventArgs e)
        {
            mainLogic.Begin.Minute = beginMinuteTextbox.Text;
        }

        private void BeginSecondTextBox_TextChanged(object sender, EventArgs e)
        {
            mainLogic.Begin.Second = beginSecondTextbox.Text;
        }

        private void EndYearTextbox_TextChanged(object sender, EventArgs e)
        {
            mainLogic.End.Year = endYearTextbox.Text;
        }

        private void EndMonthTextbox_TextChanged(object sender, EventArgs e)
        {
            mainLogic.End.Month = endMonthTextbox.Text;
        }

        private void EndDayTextBox_TextChanged(object sender, EventArgs e)
        {
            mainLogic.End.Day = endDayTextbox.Text;
        }

        private void EndHourTextBox_TextChanged(object sender, EventArgs e)
        {
            mainLogic.End.Hour = endHourTextbox.Text;
        }

        private void EndMinuteTextBox_TextChanged(object sender, EventArgs e)
        {
            mainLogic.End.Minute = endMinuteTextbox.Text;
        }

        private void EndSecondTextBox_TextChanged(object sender, EventArgs e)
        {
            mainLogic.End.Second = endSecondTextbox.Text;
        }

        private void IntegrityCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            mainLogic.CheckForIntegrity = integrityCheckbox.Checked;
        }
    }
}
