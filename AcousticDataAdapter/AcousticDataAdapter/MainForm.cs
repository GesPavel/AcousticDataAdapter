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
            mainLogic = new MainLogic();
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void FileChooserButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = folderBrowserDialog.SelectedPath;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.FileName != "")
                    {
                        var filePath2 = saveFileDialog.FileName;
                        mainLogic.OpenFolderAndSaveConversionResult(filePath, filePath2);
                    }
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }


        private void Channel1CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            mainLogic.ConvertChannel1 = channel1CheckBox.Checked;
        }

        private void Channel2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            mainLogic.ConvertChannel2 = channel2CheckBox.Checked;
        }

        private void channel0CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            mainLogic.ConvertChannel0 = channel0CheckBox.Checked;
        }
    }
}
