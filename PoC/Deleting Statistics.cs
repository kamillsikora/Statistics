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
    public partial class Deleting_Statistics : Form
    {
        public Deleting_Statistics()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult= DialogResult.Cancel;
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Deleting_Statistics_Load(object sender, EventArgs e)
        {

        }
    }
}
