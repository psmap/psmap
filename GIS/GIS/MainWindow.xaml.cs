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
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace GIS
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        ObjectsEntities db = new ObjectsEntities();

        private ObservableCollection<Objects> ObjectsListPrivate;
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string Name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
        }

        public ObservableCollection<Objects> ObjectsList
        {
            get { return ObjectsListPrivate; }
            set { ObjectsListPrivate = value; RaisePropertyChanged("ObjectsList"); }
        }

        public LocationCollection LineLocations = new LocationCollection();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Filter();
        }

        public void Filter()
        {
            Point mousePosition = Point.Parse(Convert.ToString(myMap.ActualWidth) + "," + Convert.ToString(myMap.ActualHeight));
            Location pinLocation = myMap.ViewportPointToLocation(mousePosition);
            Point nulloc = Point.Parse("0,0");
            Location nulLocation = myMap.ViewportPointToLocation(nulloc);

            if (nulLocation == myMap.Center)
            {
                IQueryable<Objects> pushpins = db.Objects
                        .Where(q => q.Voltage >= 8 && q.Voltage <= 10);
                ObjectsList = new ObservableCollection<Objects>(pushpins.ToList());
            }
            else
            {
                if (myMap.ZoomLevel < 6) { ObjectsList = new ObservableCollection<Objects>(); }
                else
                {

                    if (myMap.ZoomLevel >= 6 && myMap.ZoomLevel <= 7)
                    {
                        IQueryable<Objects> pushpins = db.Objects
                            .Where(q => q.Voltage >= 8 && q.Voltage <= 10 &&
                            q.Latitude <= nulLocation.Latitude && q.Longitude >= nulLocation.Longitude &&
                            q.Latitude >= pinLocation.Latitude && q.Longitude <= pinLocation.Longitude);
                        ObjectsList = new ObservableCollection<Objects>(pushpins.ToList());
                    }

                    if (myMap.ZoomLevel > 7 && myMap.ZoomLevel <= 8)
                    {
                        IQueryable<Objects> pushpins = db.Objects
                            .Where(q => q.Voltage >= 4 && q.Voltage <= 10 &&
                            q.Latitude <= nulLocation.Latitude && q.Longitude >= nulLocation.Longitude &&
                            q.Latitude >= pinLocation.Latitude && q.Longitude <= pinLocation.Longitude);
                        ObjectsList = new ObservableCollection<Objects>(pushpins.ToList());
                    }

                    if (myMap.ZoomLevel > 8)
                    {
                        IQueryable<Objects> pushpins = db.Objects
                            .Where(q => q.Voltage >= 1 && q.Voltage <= 10 &&
                            q.Latitude <= nulLocation.Latitude && q.Longitude >= nulLocation.Longitude &&
                            q.Latitude >= pinLocation.Latitude && q.Longitude <= pinLocation.Longitude);
                        ObjectsList = new ObservableCollection<Objects>(pushpins.ToList());
                    }
                }
            }

            ObservableCollection<Lines> LinesList = new ObservableCollection<Lines> (db.Lines.ToList());

            foreach (var x in LinesList)
            {
                MapPolyline polyline = new MapPolyline();
                polyline.Stroke = new SolidColorBrush(Colors.Gray);
                polyline.StrokeThickness = 2;
                polyline.Opacity = 0.8;

                var FirstPointLocation = new Location(db.Objects.Find(x.FirstObject).Latitude, db.Objects.Find(x.FirstObject).Longitude);
                var SecondPointLocation = new Location(db.Objects.Find(x.SecondObject).Latitude, db.Objects.Find(x.SecondObject).Longitude);

                polyline.Locations = new LocationCollection() { FirstPointLocation, SecondPointLocation };
                polyline.ToolTip = x.Name + "\nType: Polyline";
                myMap.Children.Add(polyline);
            }
        }

        private void PushpinMouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            e.Handled = true;
            FrameworkElement element = sender as FrameworkElement;
            Objects obj = element.DataContext as Objects;
            Form DataModification = new Form("Update");
            DataModification.DataContext = obj;
            DataModification.Show();
        }

        public Objects obj = new Objects();
        private void PushpinMouseRightButtonDown(object sender, MouseEventArgs e)
        {
            e.Handled = true;
            FrameworkElement element = sender as FrameworkElement;
            obj = element.DataContext as Objects;
        }

        private void PushpinMouseRightButtonUp(object sender, MouseEventArgs e)
        {
            e.Handled = true;
            FrameworkElement element = sender as FrameworkElement;
            Objects x = element.DataContext as Objects;

            Lines pl = new Lines()
            {
                FirstObject = obj.Id,
                SecondObject = x.Id,
                FirstObjectLocation = Convert.ToString(obj.Latitude).Replace(',','.') + ", " 
                + Convert.ToString(obj.Longitude).Replace(',', '.'),
                SecondObjectLocation = Convert.ToString(x.Latitude).Replace(',', '.') + ", "
                + Convert.ToString(x.Longitude).Replace(',', '.'),
            };

            if (pl.FirstObject != pl.SecondObject)
            {
                AddPolyline add_polyline = new AddPolyline();
                add_polyline.DataContext = pl;
                add_polyline.Show();
            }
        } 

        private void myMapMouseDoubleClick(object sender, MouseEventArgs e)
        {
            e.Handled = true;
            Point mousePosition = e.GetPosition(this);
            Location pinLocation = myMap.ViewportPointToLocation(mousePosition);

            string[] x = Convert.ToString(pinLocation).Split(',');
            double latitude = Math.Round(Convert.ToDouble(x[0] + ',' + x[1]), 4);
            double longitude = Math.Round(Convert.ToDouble(x[2] + ',' + x[3]), 4);

            Objects obj = new Objects { Latitude = latitude, Longitude = longitude };

            Form DataModification = new Form("Insert");
            DataModification.DataContext = obj;
            DataModification.Show();
        }

        private void ZoomLevelUp(object sender, RoutedEventArgs e) { myMap.ZoomLevel += 1; Filter(); }

        private void ZoomLevelDown(object sender, RoutedEventArgs e) { myMap.ZoomLevel -= 1; Filter(); }

        private void myMapMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0) { myMap.ZoomLevel += 1; Filter(); }
            else { myMap.ZoomLevel -= 1; Filter(); }
        }

        private void myMapMouseUp(object sender, RoutedEventArgs e) { Filter(); }
    }
}