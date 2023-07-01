using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoC
{
    public partial class Statistics : Form
    {
        public Statistics()
        {
            InitializeComponent();
            Reload(main_path);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            if(checkedListBox1.CheckedItems.Count == 1) 
            {
                if (!checkedListBox1.CheckedItems[0].ToString().Contains('.'))
                {
                    Reload(textBox1.Text + checkedListBox1.CheckedItems[0].ToString());
                }
                else
                {
                    MessageBox.Show("Error: You cannot open a file");
                }

            }
            else if(checkedListBox1.CheckedItems.Count > 1)
            {
                MessageBox.Show("Error: multiple directories are checked");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Goes back to the previous directory
            if(history.Count > 1)
            {
                history.Pop();
                Reload(history.Pop().ToString());
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(var item in checkedListBox1.CheckedItems)
            {
                if (item.ToString().Contains("."))
                {
                    MessageBox.Show("Error: You cannot make Statistics from this file: " + item);
                    return;
                }
            }
            foreach(var item in checkedListBox1.CheckedItems)
            {
                CreateNewTxt(textBox1.Text + Convert.ToString(item));
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Reload(main_path);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start(@"O:\Projects\Statistics Excels");
        }
    }
}
