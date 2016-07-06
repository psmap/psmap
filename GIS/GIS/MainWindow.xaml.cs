using System;
using System.Windows;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl;
using Microsoft.Maps.MapControl.WPF.Design;

namespace GIS
{
    public partial class MainWindow : Window
    {
        LocationConverter locConverter = new LocationConverter();
        ObjectsEntities context = new ObjectsEntities();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void MapWithPushpins_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            Point mousePosition = e.GetPosition(this);
            Location pinLocation = myMap.ViewportPointToLocation(mousePosition);

            var A = new double?[6];
            var B = new string[1];
            int i = 0, k = 0;
            using (var db = new ObjectsEntities())
            {
                var VoltageQuery = from b in db.Voltage_levels
                            orderby b.Id
                            select b;

                foreach (var item in VoltageQuery)
                {
                    A[i] = item.Voltage; i++;
                }

                var TypeQuery = from b in db.Types
                            orderby b.Id
                            select b;

                foreach (var item in TypeQuery)
                {
                    B[k] = item.Type; k++;
                }
            }

            Pushpin pin = new Pushpin();
            pin.Location = pinLocation;

            myMap.Children.Add(pin);

            System.Windows.Controls.Label label = new System.Windows.Controls.Label();
            label.Content = "Voltage: " + A[0] + "kV\nType: " + B[0];
            label.Foreground = new SolidColorBrush(Colors.DarkBlue);
            label.Background = new SolidColorBrush(Colors.WhiteSmoke);
            label.FontSize = 10;
            MapLayer.SetPosition(label, pinLocation);
            myMap.Children.Add(label);
        }

        private void Button1(object sender, RoutedEventArgs e)
        {
            myMap.ZoomLevel += 1;
        }

        private void Button2(object sender, RoutedEventArgs e)
        {
            myMap.ZoomLevel -= 1;
        }
    }
}