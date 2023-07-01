﻿using Newtonsoft.Json;
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
// Definicja struktury JSON

namespace PoC
{
    public partial class Statistics
    {
        public Dictionary<string, int> summaryCatAT = new Dictionary<string, int>();
        public Dictionary<string, int> summaryCatCV = new Dictionary<string, int>();
        public void CountSattistics(string path, string filePath)
        {
            // Odczyt pliku JSON jako tekstu
            string data = File.ReadAllText(path);

            // Przetwarzanie zawartości pliku JSON
            CountSattisticsFromJson(data, path, filePath);

        }
        public void CountSattisticsFromJson(string jsonContent, string path, string filePath)
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
        public void CountCategories(string filePath, json json)
        {
            Dictionary<string, int> categories_helperAT = new Dictionary<string, int>();
            Dictionary<string, int> categories_helperCV = new Dictionary<string, int>();
            string[] ids = new string[json.categories.Count];
            List<string> tracks = new List<string>();
            int j = 0;
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
            summaryCatAT = SummaryCat(summaryCatAT,categoriesAT);
            summaryCatCV = SummaryCat(summaryCatCV, categoriesCV);
            for (int i = 0; i < categoriesAT.Count; i++)
            {

                using (SpreadsheetDocument document = SpreadsheetDocument.Open(filePath, true))
                {
                    AddDataToSheet(document, 1, categoriesAT.Keys.ElementAt(i), categoriesAT.Values.ElementAt(i).ToString());
                    AddDataToSheet(document, 3, categoriesCV.Keys.ElementAt(i), categoriesCV.Values.ElementAt(i).ToString());
                }
                CountAttributesCat(filePath, 1, json, ids[i]);
                CountAttributesCat(filePath, 3, json, ids[i]);

            }
        }
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

        public void CountAttributesCat(string filePath, int team, json json, string cat)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            Dictionary<string, Dictionary<string, int>> nestedDictionary = new Dictionary<string, Dictionary<string, int>>();
            dictionary.Clear();
            nestedDictionary.Clear();
            List<string> tracks = new List<string>();
            if (team == 1)
            {
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
                    WriteAttributesToTxt(filePath, team, nestedDictionary);

                }
            }
            else
            {
                if (json.categories != null)
                {
                    string track = "";
                    foreach (var annotation in json.annotations)
                    {
                        if (annotation.attributes.ContainsKey("track_id"))
                        {
                            track = annotation.attributes["track_id"].ToString();
                            if (!tracks.Contains(track) && track != "")
                            {

                                if (annotation.category_id.ToString() == cat)
                                {
                                    foreach (var kvp in annotation.attributes)
                                    {
                                        if ((annotation.attributes.ContainsKey("occluded") && annotation.attributes["occluded"].ToString() == "False") || kvp.Key == "occluded")
                                        {
                                            tracks.Add(track);
                                            dictionary[kvp.Key] = kvp.Value;
                                        }

                                    }
                                }
                            }
                        }
                        else
                        {
                            if (annotation.category_id.ToString() == cat)
                            {
                                foreach (var kvp in annotation.attributes)
                                {
                                    if ((annotation.attributes.ContainsKey("occluded") && annotation.attributes["occluded"].ToString() == "False") || kvp.Key == "occluded")
                                    {
                                        tracks.Add(track);
                                        dictionary[kvp.Key] = kvp.Value;
                                    }

                                }
                            }
                        }
                        CreateNestedDictionary(nestedDictionary, dictionary);
                        dictionary.Clear();
                    }
                    WriteAttributesToTxt(filePath, team, nestedDictionary);
                }
            }
        }

        public static void WriteAttributesToTxt(string filePath, int sheetIndex, Dictionary<string, Dictionary<string, int>> nestedDictionary)
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
            if (sheetIndex == 3 || sheetIndex == 4)
            {

                IncreaseColumn(ref colCV);
                foreach (KeyValuePair<string, Dictionary<string, int>> kvp in nestedDictionary)
                {
                    using (SpreadsheetDocument document = SpreadsheetDocument.Open(filePath, true))
                    {
                        string attribute = kvp.Key;
                        Dictionary<string, int> valueCounts = kvp.Value;
                        AddDataToSheet(document, sheetIndex, attribute, null);
                        foreach (KeyValuePair<string, int> valueKvp in valueCounts)
                        {
                            IncreaseColumn(ref colCV);
                            AddDataToSheet(document, sheetIndex, valueKvp.Key, valueKvp.Value.ToString());
                            DecreaseColumn(ref colCV);
                        }
                    }
                }
                DecreaseColumn(ref colCV);
            }
        }
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