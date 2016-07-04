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

        public MainWindow()
        {
            InitializeComponent();
            myMap.Focus();
        }

        private void MapWithPushpins_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            Point mousePosition = e.GetPosition(this);
            Location pinLocation = myMap.ViewportPointToLocation(mousePosition);

            Pushpin pin = new Pushpin();
            pin.Location = pinLocation;

            myMap.Children.Add(pin);
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