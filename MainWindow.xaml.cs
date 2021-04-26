using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Project_1;

namespace ProjectClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // global data members
        DataModeler data;
        Statistics cityStats;
        public MainWindow()
        {
            InitializeComponent();           
            
            data = new DataModeler();
            //cityStats = new Statistics("Canadacities-JSON.json", "json");
        }

        private void ButtonParseXML_Click(object sender, RoutedEventArgs e)
        {            
            data.ParseFile("Canadacities-XML.xml", "xml");
            cityStats = new Statistics("Canadacities-XML.xml", "xml");
            List<string> provinces = new List<string>();
            foreach (CityInfo c in data.CityCatalogue.Values)
            {
                CitiesList.Items.Add(c.CityName);
                provinces.Add(c.Province);
            }
            List<string> uniqueProvinces = provinces.Distinct().ToList();
            foreach (string prov in uniqueProvinces)
            {
                ProvincesList.Items.Add(prov);                
            }
            CityInfo city = cityStats.DisplayLargestPopulationCity();
            txtCalcDistanceCity3.Text = city.CityName;
            CityInfo city2 = cityStats.DisplaySmallestPopulationCity();
            txtCalcDistanceCity4.Text = city2.CityName;
        }
        private void ButtonParseJSON_Click(object sender, RoutedEventArgs e)
        {            
            data.ParseFile("Canadacities-JSON.json", "json");
            cityStats = new Statistics("Canadacities-JSON.json", "json");
            List<string> provinces = new List<string>();
            foreach (CityInfo c in data.CityCatalogue.Values)
            {
                CitiesList.Items.Add(c.CityName);
                provinces.Add(c.Province);
            }
            List<string> uniqueProvinces = provinces.Distinct().ToList();
            foreach (string prov in uniqueProvinces)
            {
                ProvincesList.Items.Add(prov);
            }
            CityInfo city = cityStats.DisplayLargestPopulationCity();
            txtCalcDistanceCity3.Text = city.CityName;
            CityInfo city2 = cityStats.DisplaySmallestPopulationCity();
            txtCalcDistanceCity4.Text = city2.CityName;
        }
        private void ButtonParseCSV_Click(object sender, RoutedEventArgs e)
        {           
            data.ParseFile("Canadacities.csv", "csv");
            cityStats = new Statistics("Canadacities-JSON.json", "json");
            List<string> provinces = new List<string>();
            foreach (CityInfo c in data.CityCatalogue.Values)
            {
                CitiesList.Items.Add(c.CityName);
                provinces.Add(c.Province);
            }
            List<string> uniqueProvinces = provinces.Distinct().ToList();
            foreach(string prov in uniqueProvinces)
            {
                ProvincesList.Items.Add(prov);
            }
            CityInfo city = cityStats.DisplayLargestPopulationCity();
            txtCalcDistanceCity3.Text = city.CityName;
            CityInfo city2 = cityStats.DisplaySmallestPopulationCity();
            txtCalcDistanceCity4.Text = city2.CityName;
        }
        private void ButtonCompare_Click(object sender, RoutedEventArgs e)
        {
            var city1 = txtCompPopulationCity1.Text.ToString();
            var city2 = txtCompPopulationCity2.Text.ToString();
            var city = cityStats.CompareCitiesPopulation(city1, city2);
            txtCompPopulationLargarCity.Text = city.CityName.ToString();
            txtCompPopulationLargarCityPopulation.Text = city.Population.ToString();
        }
        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            var city1 = txtCalcDistanceCity1.Text.ToString();
            var city2 = txtCalcDistanceCity2.Text.ToString();
            double distance = cityStats.CalculateDistanceBetweenCities(city1, city2);
            txtCalcDistance.Text = distance.ToString() + "km";
        }
        private void ButtonGetProvincePopulation_Click(object sender, RoutedEventArgs e)
        {
            string province = txtProvinceInfoName.Text.ToString();
            double population = cityStats.DisplayProvincePopulation(province);
            txtProvinceInfoPopulation.Text = population.ToString();
        }
        private void RadioBtnRankCities_Click(object sender, RoutedEventArgs e)
        {
            txtProvinceFunctionList.Items.Clear();
            txtProvinceFunctionList.Items.Add(cityStats.RankProvincesByCities());
        }

        private void RadioBtnRankProvinces_Click(object sender, RoutedEventArgs e)
        {
            txtProvinceFunctionList.Items.Clear();
            txtProvinceFunctionList.Items.Add(cityStats.RankProvincesByPopulation());
        }

        private void CitiesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string city = CitiesList.SelectedItem.ToString();
            displayCityInfo(city);
        }

        //helper method
        private void displayCityInfo(string name)
        {
            CityInfo city = new CityInfo();
            foreach (string s in data.CityCatalogue.Keys)
            {
                foreach (CityInfo c in data.CityCatalogue.Values)
                {
                    if (c.CityName == name)
                    {
                        city.CityName = c.CityName;
                        city.Province = c.Province;
                        city.Population = c.Population;
                        city.Latitude = c.Latitude;
                        city.Longitude = c.Longitude;
                    }
                }
            }
            txtCityInfoName.Text = city.CityName.ToString();
            txtCityInfoProvince.Text = city.Province.ToString();
            txtCityInfoPopulation.Text = city.Population.ToString();
            txtCityInfoLat.Text = city.Latitude.ToString();
            txtCityInfoLong.Text = city.Longitude.ToString();
        }

        private void MapsButton_Click(object sender, RoutedEventArgs e)
        {
            try { 
                string city = CitiesList.SelectedItem.ToString();
                cityStats.ShowCityOnMap(city);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
