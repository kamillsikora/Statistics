using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static OfficeOpenXml.ExcelErrorValue;
// Definicja struktury JSON

namespace PoC
{
    public partial class Statistics
    {
        public void CountSattistics(string path, string filePath)
        {
            // Odczyt pliku JSON jako tekstu
            string data = File.ReadAllText(path);

            // Przetwarzanie zawartości pliku JSON
            CountSattisticsFromJson(data, filePath, path);

        }
        public void CountSattisticsFromJson(string jsonContent, string filePath, string path)
        {
            // Deserializacja JSON do obiektu
            json rootCategory = JsonConvert.DeserializeObject<json>(jsonContent);

            // Przykład wyświetlenia nazw kategorii

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                CountCategories(rootCategory, writer, path);
            }
        }
        public static void CountCategories(json json, StreamWriter writer, string path)
        {
            writer.WriteLine(Path.GetFileName(Path.GetDirectoryName(path)));
            writer.WriteLine("Nazwa kategorii\tLiczba");

            Dictionary<string, int> categories_helper = new Dictionary<string, int>();
            string[] ids = new string[json.categories.Count];
            int j = 0;
            foreach (var category in json.categories)
            {
                string id = Convert.ToString(category.id);
                ids[j] = id;
                j++;
                if (!categories_helper.ContainsKey(id))
                {
                    categories_helper[Convert.ToString(id)] = 0;
                }
            }
            foreach (var annotation in json.annotations)
            {
                string id = Convert.ToString(annotation.category_id);
                categories_helper[id]++;

            }
            Dictionary<string, int> categories = ChangeKeys(json, categories_helper);
            for (int i = 0; i < categories.Count; i++)
            {

                writer.WriteLine($"{categories.Keys.ElementAt(i)}\t{categories.Values.ElementAt(i)}");

                CountAttributesCat(json, ids[i], writer);

            }
        }

        public static void CountAttributesCat(json json, string cat, StreamWriter writer)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            Dictionary<string, Dictionary<string, int>> nestedDictionary = new Dictionary<string, Dictionary<string, int>>();
            if (json.categories != null)
            {
                foreach (var annotation in json.annotations)
                {
                    if (annotation.category_id.ToString() == cat)
                    {
                        foreach (var kvp in annotation.attributes)
                        {
                            dictionary[kvp.Key] = kvp.Value;

                        }
                        CreateNestedDictionary(nestedDictionary, dictionary);
                        dictionary.Clear();

                    }
                }
                WriteAttributesToTxt(nestedDictionary, writer);

            }
        }

        public static void WriteAttributesToTxt(Dictionary<string, Dictionary<string, int>> nestedDictionary, StreamWriter writer)
        {
            foreach (KeyValuePair<string, Dictionary<string, int>> kvp in nestedDictionary)
            {
                string attribute = kvp.Key;
                Dictionary<string, int> valueCounts = kvp.Value;
                writer.Write($"\t{attribute}\t");
                int i = 0;
                foreach (KeyValuePair<string, int> valueKvp in valueCounts)
                {
                    i++;
                    if (i > 1)
                    {
                        writer.WriteLine($"\t\t\t\t{valueKvp.Key}\t{valueKvp.Value}");
                    }
                    else
                    {
                        writer.WriteLine($"\t{valueKvp.Key}\t{valueKvp.Value}");
                    }
                }
                
            }
            writer.WriteLine();
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

