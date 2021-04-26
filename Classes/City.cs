using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
/*
 * Project: Project 1 (City Class)
 * Purpose: hold city information from the parsed file with accurate names from the file
 * Coders: Sonia Friesen, Dylan McNair, An Le
 * Date: Due February 21, 2021
 */
namespace Project_1
{
    [Serializable]
    [XmlRoot("CanadaCities")]
    public class City
    {
        public string city { get; set; }
        public string city_ascii { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string country { get; set; }
        public string admin_name { get; set; }
        public string capital { get; set; }
        public int population { get; set; }
        public int id { get; set; }
    }
}
