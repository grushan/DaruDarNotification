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
using DaruDarNotification.Helper;
namespace DaruDarNotification
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DaruDarProvider ddProvider = new DaruDarProvider();
        StorageGiftsHandler giftsHandler = new StorageGiftsHandler();
        List<DaruDarProvider.Region> regions = new List<DaruDarProvider.Region>();
        List<DaruDarProvider.Country> countries = new List<DaruDarProvider.Country>();
        List<DaruDarProvider.City> cities = new List<DaruDarProvider.City>();
        List<DaruDarProvider.Gift> gifts = new List<DaruDarProvider.Gift>();
        List<CheckBox> categories = new List<CheckBox>();
        public MainWindow()
        {
            InitializeComponent();
            countries = ddProvider.GetCountries();
            for (int i = 0; i < countries.Count; i++)
            {
                countryBox.Items.Add(countries[i]);
            }
            for (int j = 1; j < 25; j++)
            {
                CheckBox cbox = grid.Children.OfType<CheckBox>().FirstOrDefault(cb => cb.Name.Equals("CheckBox" + j.ToString()));
                categories.Add(cbox);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i  = 0; i<categories.Count; i++)
            {
                if (categories[i].IsChecked == true)
                {
                    gifts = ddProvider.GetGifts(categories[i].Tag.ToString());
                    giftsHandler.CompareGifts(gifts, categories[i].Tag.ToString());
                    giftsHandler.SaveGifts(gifts, categories[i].Tag.ToString());
                }
                
            }
           
            
        }

        private void RegionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cities = ddProvider.GetCities((regionBox.SelectedItem as DaruDarProvider.Region).GetRegionID());
            cityBox.Items.Clear();
            for (int i = 0; i < cities.Count; i++)
            {
                cityBox.Items.Add(cities[i]);
            }
            cityBox.IsEnabled = true;
        }

        private void CountryBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            regions = ddProvider.GetRegions((countryBox.SelectedItem as DaruDarProvider.Country).GetCountryID());
            regionBox.Items.Clear();
            for (int i = 0; i < regions.Count; i++)
            {
                regionBox.Items.Add(regions[i]);
            }
            regionBox.IsEnabled = true;
        }

        private void CityBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show((cityBox.SelectedItem as DaruDarProvider.City).GetCityID());

        }

        private void CityBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            tb.Text = (cityBox.SelectedItem as DaruDarProvider.City).GetCityID();
        }

        private void CheckBox1_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
