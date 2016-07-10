using System;
using System.Windows;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Data.Entity;
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
        ObjectsEntities db = new ObjectsEntities();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            var pushpins = from x in db.Objects
                           join y in db.Types on x.Type equals y.Id
                           join z in db.Voltage_levels on x.Voltage equals z.Id
                           select new
                           {
                               Name = x.Name,
                               Latitude = x.Latitude,
                               Longitude = x.Longitude,
                               Voltage = z.Voltage,
                               Type = y.Type
                           };

            foreach (var x in pushpins)
            {
                Pushpin pin = new Pushpin();
                string Latitude = Convert.ToString(x.Latitude).Replace(',', '.');
                string Longitude = Convert.ToString(x.Longitude).Replace(',', '.');
                string ObjectName = Convert.ToString(x.Name);
                pin.Location = new Location(Convert.ToDouble(x.Latitude), Convert.ToDouble(x.Longitude));
                pin.ToolTip = ObjectName + "\n" + Latitude + ", " + Longitude + "\nType: " + x.Type + "\nVoltage: " + x.Voltage;
                pin.MouseDown += new MouseButtonEventHandler(PushpinMouseLeftButtonDown);
                pin.Tag = pin.Location;
                myMap.Children.Add(pin);
            }
        }

        private void PushpinMouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            var element = sender as FrameworkElement;
            string[] substrings = Convert.ToString(element.Tag).Split(',');
            string latitude = Convert.ToString(substrings[0] + "," + substrings[1]);
            string longitude = Convert.ToString(substrings[2] + "," + substrings[3]);
            Location pinLocation = new Location(Convert.ToDouble(latitude), Convert.ToDouble(longitude));
            string qwe = Convert.ToString(element.ToolTip);
            Form a = new Form(pinLocation, qwe);
            a.Show();
        }

        private void myMapMouseDoubleClick(object sender, MouseEventArgs e)
        {
            e.Handled = true;
            Point mousePosition = e.GetPosition(this);
            Location pinLocation = myMap.ViewportPointToLocation(mousePosition);
            Form a = new Form(pinLocation, "123");
            a.Show();
        }

        private void ZoomLevelUp(object sender, RoutedEventArgs e)
        {
            myMap.ZoomLevel += 1;
        }

        private void ZoomLevelDown(object sender, RoutedEventArgs e)
        {
            myMap.ZoomLevel -= 1;
        }
    }
}