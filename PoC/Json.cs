using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Wordprocessing;

namespace PoC
{
    public partial class Statistics
    {
        public Dictionary<string, int> summaryCatAT = new Dictionary<string, int>();
        public Dictionary<string, int> summaryCatCV = new Dictionary<string, int>();

        /// <summary>
        /// Initializes the process of counting Statistics
        /// </summary>
        /// <param name="path">path of the json file</param>
        /// <param name="filePath">path of the creating file</param>
        public void CountSatistics(string path, string filePath)
        {
            string data = File.ReadAllText(path);
            path = Path.GetDirectoryName(Path.GetDirectoryName(path));
            CountSatisticsFromJson(data, path, filePath);
        }

        /// <summary>
        /// Initializes the process of counting Statistics from json
        /// </summary>
        /// <param name="jsonContent">data from json file</param>
        /// <param name="path">path of the json file</param>
        /// <param name="filePath">path of the creating file</param>
        public void CountSatisticsFromJson(string jsonContent, string path, string filePath)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(filePath, true))
            {
                AddDataToSheet(document, 1, Path.GetFileName(path), null);
                AddDataToSheet(document, 3, Path.GetFileName(path), null);
            }
            // Deserializacja JSON do obiektu
            json rootCategory = JsonConvert.DeserializeObject<json>(jsonContent);
            // Przykład wyświetlenia nazw kategori
            CountCategories(filePath, rootCategory);
        }

        /// <summary>
        /// Makes a dictionary with names of categories and numbers of occurrences
        /// </summary>
        /// <param name="filePath">path of the creating file</param>
        /// <param name="json">deserialized data from json</param>
        public void CountCategories(string filePath, json json)
        {
            Dictionary<string, int> categories_helperAT = new Dictionary<string, int>();
            Dictionary<string, int> categories_helperCV = new Dictionary<string, int>();
            string[] ids = new string[json.categories.Count];
            List<string> tracks = new List<string>();
            int j = 0;
            //secures from other jsons (not from cvat export)
            if (json.licenses != null)
            {
                foreach (var category in json.categories)
                {
                    string id = Convert.ToString(category.id);
                    ids[j] = id;
                    j++;
                    if (!categories_helperAT.ContainsKey(id))
                    {
                        categories_helperAT[Convert.ToString(id)] = 0;
                        categories_helperCV[Convert.ToString(id)] = 0;
                    }
                }
                foreach (var annotation in json.annotations)
                {
                    string track = "";
                    if (annotation.attributes.ContainsKey("occluded") && annotation.attributes["occluded"].ToString() == "False")
                    {
                        string id = Convert.ToString(annotation.category_id);
                        categories_helperAT[id]++;
                        if (annotation.attributes.ContainsKey("track_id"))
                        {
                            track = annotation.attributes["track_id"].ToString();
                            if (!tracks.Contains(track) && track != "")
                            {
                                tracks.Add(track);
                                categories_helperCV[id]++;
                            }

                        }
                        else
                        {
                            categories_helperCV[id]++;
                        }
                    }

                }
                Dictionary<string, int> categoriesAT = ChangeKeys(json, categories_helperAT);
                Dictionary<string, int> categoriesCV = ChangeKeys(json, categories_helperCV);
                summaryCatAT = SummaryCat(summaryCatAT, categoriesAT);
                summaryCatCV = SummaryCat(summaryCatCV, categoriesCV);
                for (int i = 0; i < categoriesAT.Count; i++)
                {

                    using (SpreadsheetDocument document = SpreadsheetDocument.Open(filePath, true))
                    {
                        AddDataToSheet(document, 1, categoriesAT.Keys.ElementAt(i), categoriesAT.Values.ElementAt(i).ToString());
                        AddDataToSheet(document, 3, categoriesCV.Keys.ElementAt(i), categoriesCV.Values.ElementAt(i).ToString());
                    }
                    CountAttributesCat(filePath, 1, json, ids[i]);
                }
            }
        }

        /// <summary>
        /// Counts the summarized categories from all jsons in the main directory
        /// </summary>
        /// <param name="summaryCat">the dictionary of summarized categories</param>
        /// <param name="categories">counted categories from one json</param>
        /// <returns></returns>
        public Dictionary<string, int> SummaryCat(Dictionary<string, int> summaryCat, Dictionary<string, int> categories)
        {
            foreach (var category in categories)
            {
                if (!summaryCat.ContainsKey(category.Key))
                {
                    summaryCat[category.Key] = category.Value;
                }
                else
                {
                    summaryCat[category.Key] += category.Value;
                }
            }
            return summaryCat;
        }

        /// <summary>
        /// Makes a dictiornary from attributes founded in one annotation
        /// </summary>
        /// <param name="filePath">path of the creating file</param>
        /// <param name="team">index of the team for whom the Statistics are counted</param>
        /// <param name="json">deserialized data from json</param>
        /// <param name="cat">name of the category from which the attributes are counted</param>
        public void CountAttributesCat(string filePath, int team, json json, string cat)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            Dictionary<string, Dictionary<string, int>> nestedDictionary = new Dictionary<string, Dictionary<string, int>>();
            dictionary.Clear();
            nestedDictionary.Clear();

            if (json.categories != null)
            {
                foreach (var annotation in json.annotations)
                {
                    if (annotation.category_id.ToString() == cat)
                    {
                        foreach (var kvp in annotation.attributes)
                        {
                            if ((annotation.attributes.ContainsKey("occluded") && annotation.attributes["occluded"].ToString() == "False") || kvp.Key == "occluded")
                            {
                                dictionary[kvp.Key] = kvp.Value;
                            }

                        }
                        CreateNestedDictionary(nestedDictionary, dictionary);
                        dictionary.Clear();

                    }

                }
                WriteAttributesToExcel(filePath, team, nestedDictionary);
            }
        }

        /// <summary>
        /// Adds counted attributes to created excxel
        /// </summary>
        /// <param name="filePath">path of the creating file</param>
        /// <param name="sheetIndex">Index of the sheet in which data are added</param>
        /// <param name="nestedDictionary">a summarized dictionary</param>
        public static void WriteAttributesToExcel(string filePath, int sheetIndex, Dictionary<string, Dictionary<string, int>> nestedDictionary)
        {
            if (sheetIndex == 1 || sheetIndex == 2)
            {

                IncreaseColumn(ref colAT);
                foreach (KeyValuePair<string, Dictionary<string, int>> kvp in nestedDictionary)
                {
                    using (SpreadsheetDocument document = SpreadsheetDocument.Open(filePath, true))
                    {
                        string attribute = kvp.Key;
                        Dictionary<string, int> valueCounts = kvp.Value;
                        AddDataToSheet(document, sheetIndex, attribute, null);
                        foreach (KeyValuePair<string, int> valueKvp in valueCounts)
                        {
                            IncreaseColumn(ref colAT);
                            AddDataToSheet(document, sheetIndex, valueKvp.Key, valueKvp.Value.ToString());
                            DecreaseColumn(ref colAT);
                        }
                    }
                }
                DecreaseColumn(ref colAT);
            }
        }

        /// <summary>
        /// Changes keys in dictionary, form numbers (indexes) to names
        /// </summary>
        /// <param name="json">deserialized data from json</param>
        /// <param name="old_categories">a dictionary before making changes</param>
        /// <returns></returns>
        public static Dictionary<string, int> ChangeKeys(json json, Dictionary<string, int> old_categories)
        {
            Dictionary<string, int> new_categories = new Dictionary<string, int>();
            foreach (var category in json.categories)
            {
                string id = Convert.ToString(category.id);
                if (old_categories.ContainsKey(id))
                {
                    new_categories[category.name] = old_categories[id];
                }

            }
            return new_categories;
        }

        /// <summary>
        /// Makes nested dictionary for attributes found in json
        /// </summary>
        /// <param name="nestedDictionary">a summarized dictionary</param>
        /// <param name="dict">a dictionary from which attributes are counted</param>
        public static void CreateNestedDictionary(Dictionary<string, Dictionary<string, int>> nestedDictionary, Dictionary<string, object> dict)
        {

            foreach (KeyValuePair<string, object> kvp in dict)
            {
                string attribute = kvp.Key;
                string value = kvp.Value.ToString();

                if (!nestedDictionary.ContainsKey(attribute))
                {
                    nestedDictionary[attribute] = new Dictionary<string, int>();
                }

                if (!nestedDictionary[attribute].ContainsKey(value))
                {
                    nestedDictionary[attribute][value] = 0;
                }

                nestedDictionary[attribute][value]++;
            }

        }
    }
}