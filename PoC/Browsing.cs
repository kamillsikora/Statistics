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

        /// <summary>
        /// Fills checkedListBox with directories and files from a chosen path and adds the path to history 
        /// </summary>
        /// <param name="root_path">Path that was chosen in folderBrowserDialog or main_path</param>
        public void Reload(string root_path)
        {
            history.Push(root_path);
            try
            {
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
            catch (Exception)
            {
                MessageBox.Show("You are not connected with VPN");
                return;
            }
        }
        /// <summary>
        /// Puts path of the main directory into headline textbox 
        /// </summary>
        /// <param name="path">path of the main directory</param>
        public void InitializeHeadline(string path)
        {
            textBox1.Text = path + "\\";
        }

        /// <summary>
        /// Creates Statistics from folders checked in a checkedListBox, checks if the folder already exists and shows loading window
        /// </summary>
        /// <param name="path">path of the directory from which Statistics are going to be made</param>
        public void CreateNewStats(string path)
        {
            string filePath = Path.Combine(@"O:\Projects\Statistics_Excels", Path.GetFileName(path) + "_Statistics.xlsx");
            try
            {

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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "\n\nClose it before creating new Statistics.");
                return;
            }
            rowAT = 0;
            colAT = "A";
            rowCV = 0;
            colCV = "A";
            summaryCatAT.Clear();
            summaryCatCV.Clear();
            Loading_Window loading_Window = new Loading_Window();
            loading_Window.loading_path = path;
            loading_Window.InitializeTextBox();
            loading_Window.Show();
            loading_Window.Refresh();
            ProcessDirectory(path, filePath);
            loading_Window.Dispose();
            rowAT = 0;
            colAT = "A";
            rowCV = 0;
            colCV = "A";
            for (int i = 0; i < summaryCatAT.Count; i++)
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Open(filePath, true))
                {
                    AddDataToSheet(document, 2, summaryCatAT.Keys.ElementAt(i), summaryCatAT.Values.ElementAt(i).ToString());
                    AddDataToSheet(document, 4, summaryCatCV.Keys.ElementAt(i), summaryCatCV.Values.ElementAt(i).ToString());
                }

            }
            MessageBox.Show("Done: " + filePath);
        }
        /// <summary>
        /// Looks for json files in the directory (checks zips and subfolders)
        /// </summary>
        /// <param name="path">path of the directory from which Statistics are going to be made</param>
        /// <param name="filePath">path of the creating file</param>
        public void ProcessDirectory(string path, string filePath)
        {
            string[] subfolders = Directory.GetDirectories(path);
            string[] zips = Directory.GetFiles(path, "*.zip");
            string[] jsons = Directory.GetFiles(path, "*.json");
            
            foreach (string json in jsons)
            {
                CountSatistics(json, filePath);
            }
            foreach (string zip in zips)
            {
                ProcessZip(zip, filePath);
            }
            foreach (string subfolder in subfolders)
            {
                ProcessDirectory(subfolder, filePath);
            }
        }

        /// <summary>
        /// Looks for json files in zip
        /// </summary>
        /// <param name="path">path of the directory from which Statistics are going to be made</param>
        /// <param name="filePath">path of the creating file</param>
        public void ProcessZip(string path, string filePath)
        {
            using (ZipArchive archive = ZipFile.OpenRead(path))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        using (Stream stream = entry.Open())
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string jsonContent = reader.ReadToEnd();
                            CountSatisticsFromJson(jsonContent, Path.GetDirectoryName(Path.GetDirectoryName(entry.ToString())), filePath);
                        }

                    }
                }
            }
        }
    }
}