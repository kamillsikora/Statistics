using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


public class json
{
    public licenses[] licenses { get; set; }
    public info info { get; set; }
    public List<categories> categories { get; set; }
    public List<images> images { get; set; }
    public List<annotations> annotations { get; set; }
}



public class licenses
{
    public string name { get; set; }
    public int id { get; set; }
    public string url { get; set; }
}
public class info
{
    public string contributor { get; set; }
    public string date_created { get; set; }
    public string description { get; set; }
    public string url { get; set; }
    public string version { get; set; }
    public string year { get; set; }
}
public class categories
{
    public int id { get; set; }
    public string name { get; set; }
    public string supercategory { get; set; }
}
public class images
{
    public int id { get; set; }
    public int width { get; set; }
    public int height { get; set; }
    public string file_name { get; set; }
    public int license { get; set; }
    public string flickr_url { get; set; }
    public string coco_url { get; set; }
    public int date_captured { get; set; }
}
public class annotations
{
    public int id { get; set; }
    public int image_id { get; set; }
    public int category_id { get; set; }
    public string[] segmentation { get; set; }
    public float area { get; set; }
    public float[] bbox { get; set; }
    public int iscrowd { get; set; }
    public Dictionary<string, object> attributes { get; set; }
}




