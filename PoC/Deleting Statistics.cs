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
{
    /// <summary>
    /// Form class <c>Deleting_Statistics</c> - window with request of deleting existing file or stopping process
    /// </summary>
    public partial class Deleting_Statistics : Form
    {
        public Deleting_Statistics()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Yes button
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// No button
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult= DialogResult.Cancel;
            this.Close();
        }
    }
}
