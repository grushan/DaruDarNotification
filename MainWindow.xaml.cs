using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;
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
        string settingsFile = @"settings.txt";
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        WindowState prevState;
        struct Settings
        {
            public DaruDarProvider.Country country;
            public DaruDarProvider.Region region;
            public DaruDarProvider.City city;
            public List<string> categories;

        }
        Settings settings;

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
            settings.categories = new List<string>();
            SettingsLoad();
            
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Execute();
            SettingsSave();

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, int.Parse(minutesBox.Text), 0);
            dispatcherTimer.Start();
           
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Execute();
            SettingsSave();
        }
        private void SettingsLoad()
        {
            if (File.Exists(settingsFile) == false)
            {
                File.Create(settingsFile);

            }
            else
            {
                StreamReader streamReader = new StreamReader(settingsFile);
                string[] country = streamReader.ReadLine().Split (':');
                string[] region = streamReader.ReadLine().Split(':');
                string[] city = streamReader.ReadLine().Split(':');
                string[] categories = streamReader.ReadLine().Split(',');

                settings.country = new DaruDarProvider.Country(country[1], country[0]);
                settings.region = new DaruDarProvider.Region(region[1], region[0]);
                settings.city = new DaruDarProvider.City(city[1], city[0]);

                countryBox.Text = settings.country.name;
                regionBox.Text = settings.region.name;
                cityBox.Text = settings.city.name;

                for (int i = 0; i < categories.Length; i++)
                {
                    settings.categories.Add(categories[i]);
                    grid.Children.OfType<CheckBox>().FirstOrDefault(cb => cb.Tag.Equals(categories[i])).IsChecked = true;
                }

                streamReader.Close();
            }
        }
        private void SettingsSave()
        {
            
            if (File.Exists(settingsFile) == false)
            {
                File.Create(settingsFile);
            }
            else
            {
                StreamWriter f = new StreamWriter(settingsFile);
                f.WriteLine(String.Format("{0}:{1}", settings.country.name, settings.country.country_id));
                f.WriteLine(String.Format("{0}:{1}", settings.region.name, settings.region.region_id));
                f.WriteLine(String.Format("{0}:{1}", settings.city.name, settings.city.city_id));
                f.WriteLine(String.Join(",", settings.categories));
                f.Close();
            }
        }
        private void Execute()
        {

            settings.categories.Clear();
            for (int i = 0; i < categories.Count; i++)
            {
                if (categories[i].IsChecked == true)
                {
                    settings.categories.Add(categories[i].Tag.ToString());
                    gifts = ddProvider.GetGifts(settings.country.GetCountryID(), settings.region.GetRegionID(), settings.city.GetCityID(), categories[i].Tag.ToString());
                    giftsHandler.CompareGifts(gifts, categories[i].Tag.ToString());
                    giftsHandler.SaveGifts(gifts, categories[i].Tag.ToString());
                }
            }
        }
        private void RegionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            settings.region = (regionBox.SelectedItem as DaruDarProvider.Region);
            cities = ddProvider.GetCities(settings.region.GetRegionID());
            cityBox.Items.Clear();
            for (int i = 0; i < cities.Count; i++)
            {
                cityBox.Items.Add(cities[i]);
            }
            cityBox.IsEnabled = true;
        }

        private void CountryBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            settings.country = (countryBox.SelectedItem as DaruDarProvider.Country);
            regions = ddProvider.GetRegions(settings.country.GetCountryID());
            regionBox.Items.Clear();
            for (int i = 0; i < regions.Count; i++)
            {
                regionBox.Items.Add(regions[i]);
            }
            regionBox.IsEnabled = true;
        }


        private void CityBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            settings.city = (cityBox.SelectedItem as DaruDarProvider.City);
        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                Hide();
            else
                prevState = WindowState;
        }
        private void Maximize(object sender, RoutedEventArgs e)
        {
            Show();
            WindowState = prevState;
        }
    }
}
