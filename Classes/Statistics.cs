using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Project_1
{
    public class Statistics
    {
        Dictionary<string, CityInfo> cityCatalogue = new Dictionary<string, CityInfo>();

        public Statistics() { }
        public Statistics(string file, string type)
        {
            DataModeler parser = new DataModeler();
            cityCatalogue = parser.ParseFile(file, type);
        }
        //citymethods
        public List<CityInfo> DisplayCityInformation(string cityName)
        {
            List<CityInfo> citiesInfo = new List<CityInfo>();
            citiesInfo.Add(cityCatalogue[cityName]);
            return citiesInfo;
        } //end DisplayCityInformation()

        /// <summary>Find the city with the largest population</summary>
        public CityInfo DisplayLargestPopulationCity()
        {
            CityInfo largestPopCity = new CityInfo();
            bool assignedFirst = false;

            foreach (var city in cityCatalogue)
            {
                if (!assignedFirst)
                {
                    assignedFirst = true;
                    largestPopCity = city.Value;
                }
                else
                {
                    if (largestPopCity.GetPopulation() < city.Value.GetPopulation())
                    {
                        largestPopCity = city.Value;
                    }
                }
            }
            return largestPopCity;
        } //end DisplayLargestPopulationCity()

        /// <summary>Find the city with the lowest population</summary>
        public CityInfo DisplaySmallestPopulationCity()
        {
            CityInfo smallestPopCity = new CityInfo();
            bool assignedFirst = false;

            foreach (var city in cityCatalogue)
            {
                if (!assignedFirst)
                {
                    assignedFirst = true;
                    smallestPopCity = city.Value;
                }
                else
                {
                    if (smallestPopCity.GetPopulation() > city.Value.GetPopulation())
                    {
                        smallestPopCity = city.Value;
                    }
                }
            }
            return smallestPopCity;
        } //end DisplaySmallestPopulationCity()

        /// <summary>Compare the populations of two cities and return the city with the higher result</summary>
        public CityInfo CompareCitiesPopulation(string cityName1, string cityName2)
        {
            string c = FirstCharToUpper(cityName1);
            string a = FirstCharToUpper(cityName2);
            CityInfo city1 = cityCatalogue[c];
            CityInfo city2 = cityCatalogue[a];

            if (city1.GetPopulation() > city2.GetPopulation())
            {
                return city1;
            }
            else
            {
                return city2;
            }
        } //end CompareCitiesPopulation()

        /// <summary>Print out the coordinates of the the city provided(should show on map but I don't think we can do that)</summary>
        public void ShowCityOnMap(string cityName)
        {
            CityInfo city = cityCatalogue[cityName];
            try
            {
                System.Diagnostics.Process.Start("explorer.exe", @"https://www.latlong.net/Show-Latitude-Longitude.html");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        } //end ShowCityOnMap()

        /// <summary>Calculates the distance between two coordinates</summary>
        public double CalculateDistanceBetweenCities(string cityName1, string cityName2)
        {
            string c = FirstCharToUpper(cityName1);
            string a = FirstCharToUpper(cityName2);
            CityInfo city1 = cityCatalogue[c];
            CityInfo city2 = cityCatalogue[a];
            if ((city1.Latitude == city2.Latitude) && (city1.Longitude == city2.Longitude))
            {
                return 0;
            }
            else
            {
                double theta = city1.Longitude - city2.Longitude;
                double dist = Math.Sin(deg2rad(city1.Latitude)) * Math.Sin(deg2rad(city2.Latitude)) + Math.Cos(deg2rad(city1.Latitude)) * Math.Cos(deg2rad(city2.Latitude)) * Math.Cos(deg2rad(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);
                dist = dist * 60 * 1.1515;
                dist = dist * 1.609344; //km                               
                return (dist);
            }
        }
        //helper methods for distance
        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }
        //helper methods for distance
        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
      
        //province methods
        public double DisplayProvincePopulation(string province)
        {
            string province1 = province.ToLower();
            double total = 0;
            foreach (var city in cityCatalogue)
            {
                if (city.Value.Province.ToLower() == province1)
                {
                    total += city.Value.GetPopulation();
                }
            }
            return total;
        }

        public string DisplayProvinceCities(string province)
        {
            string cityList = "";
            var provinceCities = cityCatalogue.Where(city => city.Value.GetProvince().ToLower() == province.ToLower()).ToList();
            foreach (var x in provinceCities)
            {
                cityList += x.Key + "\n";
            }
            return cityList;
        }
        public string RankProvincesByPopulation()
        {
            SortedList<double, string> populationList = new SortedList<double, string>();
            string view = "";
            int index = 1;
            var provinceList = cityCatalogue.GroupBy(c => c.Value.GetProvince()).Select(group => group.First()).ToList();
            foreach (var province in provinceList)
            {
                populationList.Add(DisplayProvincePopulation(province.Value.GetProvince()), province.Value.GetProvince());
            }
            foreach (var pop in populationList)
            {
                view += index + "." + pop.Value +" "+ pop.Key + "\n";
                index++;
            }
            return view;
        }
        public string RankProvincesByCities()
        {
            Dictionary<string, double> countList = new Dictionary<string, double>();
            string view = "";
            int index = 1;
            var provinceList = cityCatalogue.GroupBy(city => city.Value.GetProvince()).Select(temp => temp.First()).ToList();
            foreach (var province in provinceList)
            {
                int countedcity = cityCatalogue.Where(city => city.Value.GetProvince() == province.Value.GetProvince()).ToList().Count;
                countList.Add(province.Value.GetProvince(), countedcity);
            }
            foreach (var pop in countList.OrderBy(city => city.Value))
            {
                view += index + "." + pop.Key + " " + pop.Value + "\n";
                index++;
            }
            return view;
        }

        //helper method
        public static string FirstCharToUpper(string input) =>
       input switch
       {
           null => throw new ArgumentNullException(nameof(input)),
           "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
           _ => input.First().ToString().ToUpper() + input.Substring(1)
       };
    }
}
