using System;
using System.Collections.Generic;
using System.Text;
/*
 * Project: Project 1 (CityInfo Class)
 * Purpose: hold city information from the parsed file
 * Coders: Sonia Friesen, Dylan McNair, An Le
 * Date: Due February 21, 2021
 */
namespace Project_1
{
    public class CityInfo
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CityAscii { get; set; }
        public int Population { get; set; }
        public string Country { get; set; }
        public string Capital { get; set; }
        public string Province { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        //constructors
        public CityInfo()
        {
            //default
        }
        //one parameter contructor
        public CityInfo(City city)
        {
            CityName = city.city;
            CityAscii = city.city_ascii;
            Latitude = city.lat;
            Longitude = city.lng;
            Country = city.country;
            Province = city.admin_name;
            Capital = city.capital;
            Population = city.population;
            CityId = city.id;
        }
        //method: GetProvince()
        //Purpose: getter
        //Parameters: none
        //returns: string
        public string GetProvince()
        {
            return Province;
        }
        //method: GetPopulation()
        //Purpose: getter
        //Parameters: none
        //returns: string
        public int GetPopulation()
        {
            return Population;
        }
        //method: GetLocation()
        //Purpose: getter
        //Parameters: none
        //returns: string
        public string GetLocation()
        {
            return $"latitude: {Latitude}, longitude {Longitude}";
        }
    }
}
