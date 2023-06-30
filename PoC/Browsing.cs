using System;
using System.Collections;
using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PoC
{
    public partial class Statistics
    {
        Stack<string> history = new Stack<string>();
        public string main_path = @"O:\Projects";
        public void start()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                main_path = folderBrowserDialog1.SelectedPath;
                Reload(folderBrowserDialog1.SelectedPath);
            }
        }

        public void Reload(string root_path)
        {
            history.Push(root_path);
            string[] dirs = Directory.GetDirectories(root_path);
            string[] files = Directory.GetFiles(root_path);
            checkedListBox1.Items.Clear();
            foreach (string dir in dirs)
            {
                checkedListBox1.Items.Add(Path.GetFileName(dir), CheckState.Unchecked);
            }
            foreach (string file in files)
            {
                checkedListBox1.Items.Add(Path.GetFileName(file), CheckState.Unchecked);
            }
            InitializeHeadline(root_path);
        }

        public void InitializeHeadline(string path)
        {
            // Puts path of the main directory into headline textbox  
            textBox1.Text = path + "\\";
        }

        public void CreateNewTxt(string path)
        {
            string filePath = Path.Combine(@"O:\Projects\Statistics Txt", Path.GetFileName(path) + " Statistics.xlsx");
            if (File.Exists(filePath))
            {
                Deleting_Statistics deleting_Statistics = new Deleting_Statistics();
                DialogResult result = deleting_Statistics.ShowDialog();

                if (result == DialogResult.OK)
                {
                    File.Delete(filePath);
                    CreateExcelFile(filePath);
                }
                else
                {
                    return;
                }
            }
            else
            {
                CreateExcelFile(filePath);
            }
            rowAT = 0;
            colAT = "A";
            rowCV = 0;
            colCV = "A";
            summaryCat.Clear();
            summaryNestedDict.Clear();
            ProcessFolder(path, filePath);
            rowAT = 0;
            colAT = "A";
            int i = 0;
            //int j = 0;
            for (i = 0; i < summaryCat.Count; i++)
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Open(filePath, true))
                {
                    AddDataToSheet(document, 2, summaryCat.Keys.ElementAt(i), summaryCat.Values.ElementAt(i).ToString());
                    /*if(summaryCat.Values.ElementAt(i) != 0)
                    {
                        string attribute = summaryNestedDict.Keys.ElementAt(j);
                        Dictionary<string, int> valueCounts = summaryNestedDict.Values.ElementAt(j);
                        AddDataToSheet(document, 2, attribute, null);
                        foreach (KeyValuePair<string, int> valueKvp in valueCounts)
                        {
                            IncreaseColumn(ref col);
                            AddDataToSheet(document, 2, valueKvp.Key, valueKvp.Value.ToString());
                            DecreaseColumn(ref col);
                        }
                        j++;
                    }
                    */
        }

    }
            //WriteAttributesToTxt(filePath, 2, summaryNestedDict);
            MessageBox.Show("Done");
        }

        public void ProcessFolder(string path, string filePath)
        {
            string[] subfolders = Directory.GetDirectories(path);
            string[] zips = Directory.GetFiles(path, "*.zip");
            string[] jsons = Directory.GetFiles(path, "*.json");
            
            foreach (string json in jsons)
            {
                MessageBox.Show(json + "json zwykly");
                CountSattistics(json, filePath);
            }
            foreach (string zip in zips)
            {
                ProcessZip(zip, filePath);
            }
            foreach (string subfolder in subfolders)
            {
                ProcessFolder(subfolder, filePath);
            }
        }

        public void ProcessZip(string path, string filePath)
        {
            using (ZipArchive archive = ZipFile.OpenRead(path))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        //MessageBox.Show(entry.ToString() + " zipowany");
                        using (Stream stream = entry.Open())
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string jsonContent = reader.ReadToEnd();
                            CountSattisticsFromJson(jsonContent, Path.GetDirectoryName(Path.GetDirectoryName(entry.ToString())), filePath);
                        }

                    }
                }
            }
        }

        public void DeserializeJson(string parh)
        {

        }


        private void Statistics_Load(object sender, EventArgs e)
        {

        }
    }
}