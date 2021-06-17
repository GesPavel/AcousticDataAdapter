using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AcousticDataAdapter
{
    public partial class Form1 : Form
    {
        MainLogic mainLogic;
        public Form1()
        {
            mainLogic = new MainLogic();
            InitializeComponent();
        }
    }
}
