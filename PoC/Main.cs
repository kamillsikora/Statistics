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
        /// <summary>
        /// Starts program
        /// </summary>
        public Statistics()
        {
            InitializeComponent();
            Reload(main_path);
        }

        /// <summary>
        /// Opens a directory chosen from checkedListBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Goes back to the previous folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            //Goes back to the previous directory
            if(history.Count > 1)
            {
                history.Pop();
                Reload(history.Pop().ToString());
            }
        }

        /// <summary>
        /// Creates Statistics from folders checked in a checkedListBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                CreateNewStats(textBox1.Text + Convert.ToString(item));
            }
        }

        /// <summary>
        /// Opens folderBrowserDialog to change disk in checkedListBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                main_path = folderBrowserDialog1.SelectedPath;
                Reload(folderBrowserDialog1.SelectedPath);
            }
        }

        /// <summary>
        /// Goes back to path: O:\Projects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            Reload(main_path);
        }

        /// <summary>
        /// Opens folder with Statistics
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start(@"O:\Projects\Statistics_Excels");
        }
    }
}
