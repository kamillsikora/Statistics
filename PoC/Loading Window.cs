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
        public void TextLoading()
        {
            while (true)
            {
                if(label1.Text == "Loading")
                {
                    label1.Text = "Loading...";
                }
                if (label1.Text == "Loading...")
                {
                    label1.Text = "Loading";
                }
            }
        }

    }
}
