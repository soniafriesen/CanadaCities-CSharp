using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using CsvHelper;
using System.Globalization;
using System.Linq;
/*
 * Project: Project 1 (DataModeler Class)
 * Purpose: To Parse the clients requested file (json,xml,csv)
 * Coders: Sonia Friesen, Dylan McNair, An Le
 * Date: Due February 21, 2021
 */
namespace Project_1
{
    class DataModeler
    {
        //data members
        private delegate void FileDelegate(string file);
        public Dictionary<string,CityInfo> CityCatalogue { get; set; }

        //constructor
        public DataModeler()
        {
            CityCatalogue = new Dictionary<string, CityInfo>();
        }

        //method: ParseFile()
        //Purpose: To parse user specific file
        //Parameters: string file, string type
        //returns: Dictionary<string,cityinfo>
        public Dictionary<string,CityInfo> ParseFile(string file, string type)
        {
            FileDelegate fileDelegate = null;
            switch (type.ToLower())
            {
                case "xml":
                    fileDelegate += ParseXML;
                    break;
                case "json":
                    fileDelegate += ParseJSON;
                    break;
                case "csv":
                    fileDelegate += ParseCSV;
                    break;
                default:
                    break;
            }
            fileDelegate(file);
            return CityCatalogue;
        }
        //method: ParseXML()
        //Purpose: To parse xml specific file
        //Parameters: string file
        //returns: void
        private void ParseXML(string file)
        {
            try
            {
                //create an XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(file); //load the file 
                XmlNodeList xmlNodeList = xmlDoc.DocumentElement.ChildNodes;
                List<City> cities = new List<City>();
                foreach (XmlNode node in xmlNodeList)
                {
                    XmlNodeList child = node.ChildNodes;
                    //create a city object
                    City city = new City();
                    city.city = child.Item(0).InnerText;
                    city.city_ascii = child.Item(1).InnerText;
                    city.lat = double.Parse(child.Item(2).InnerText);
                    city.lng = double.Parse(child.Item(3).InnerText);
                    city.country = child.Item(4).InnerText;
                    city.admin_name = child.Item(5).InnerText;
                    if (child.Item(6).InnerText == null)
                        city.capital = "";
                    city.capital = child.Item(6).InnerText;
                    city.population = int.Parse(child.Item(7).InnerText);
                    city.id = int.Parse(child.Item(8).InnerText);                   
                    //add teh city to the City Catalogue 
                    cities.Add(city);
                }
                foreach (City city in cities)
                {
                    if (!CityCatalogue.ContainsKey(city.city) && !string.IsNullOrWhiteSpace(city.city))
                    {
                        CityInfo newCity = new CityInfo(city);
                        CityCatalogue.Add(city.city, newCity);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        //method: ParseJSON()
        //Purpose: To parse json specific file
        //Parameters: string file
        //returns: void
        private void ParseJSON(string file)
        {
            try
            {
                //get and read the json
                string cityJSON = File.ReadAllText(file);
                //deserialize the object using newtonsoft json, adding those cities to a list
                List<City> cities = JsonConvert.DeserializeObject<List<City>>(cityJSON);

                //we can now add the cities to the catalogue
                foreach(City city in cities)
                {                    
                    if (!CityCatalogue.ContainsKey(city.city) && !string.IsNullOrWhiteSpace(city.city))
                    {
                        CityInfo newCity = new CityInfo(city);                        
                        CityCatalogue.Add(city.city, newCity);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
        //method: ParseCSV()
        //Purpose: To parse csv specific file
        //Parameters: string file
        //returns: void
        private void ParseCSV(string file)
        {
            //try and read in the file
            try
            {
                //create a list of city class to hold the data from the file
                List<City> cities = new List<City>();
                var reader = new StreamReader(file);
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var header = csv.Configuration.HasHeaderRecord;
                    cities = csv.GetRecords<City>().ToList();
                }
                //add the city from the list to the catalogue
                foreach (City city in cities)
                {                    
                    if (!CityCatalogue.ContainsKey(city.city) && !string.IsNullOrWhiteSpace(city.city))
                    {
                        CityInfo newCity = new CityInfo(city);
                        CityCatalogue.Add(city.city, newCity);
                    }
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
