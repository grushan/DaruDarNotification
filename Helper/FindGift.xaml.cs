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
using System.Windows.Shapes;

namespace DaruDarNotification.Helper
{
    /// <summary>
    /// Логика взаимодействия для FindGift.xaml
    /// </summary>
    public partial class FindGift : Window
    {
        public FindGift(string name, string link)
        {
            InitializeComponent();
            NameLabel.Content = name;
            LinkLabel.Content = link;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(LinkLabel.Content.ToString());
            this.Close();
        }
    }
}
