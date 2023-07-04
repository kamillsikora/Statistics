using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoC
{    /// <summary>
     /// Form class <c>Loading_Window</c> - window with loading screen
     /// </summary>
    public partial class Loading_Window : Form
    {
        public string loading_path;
        public Loading_Window()
        {
            InitializeComponent();
        }
        public void InitializeTextBox()
        {
            textBox1.Text = loading_path;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
