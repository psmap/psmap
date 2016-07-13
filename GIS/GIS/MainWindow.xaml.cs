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
        private ObservableCollection<Lines> LinesListPrivate;
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

        public ObservableCollection<Lines> LinesList
        {
            get { return LinesListPrivate; }
            set { LinesListPrivate = value; RaisePropertyChanged("LinesList"); }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public int VoltageBorder;

        public void Filter()
        {
            Location FL = myMap.ViewportPointToLocation(Point.Parse(Convert.ToString(myMap.ActualWidth) + "," 
                + Convert.ToString(myMap.ActualHeight)));
            Location SL = myMap.ViewportPointToLocation(Point.Parse("0,0"));

            if (myMap.ZoomLevel >= 1 && myMap.ZoomLevel < 6) VoltageBorder = 10;
            if (myMap.ZoomLevel >= 6 && myMap.ZoomLevel < 11) VoltageBorder = 9;
            if (myMap.ZoomLevel >= 11 && myMap.ZoomLevel < 12) VoltageBorder = 8;
            if (myMap.ZoomLevel >= 12) VoltageBorder = 1;

            IQueryable<Objects> pushpins = db.Objects
                .Where(q => q.Voltage >= VoltageBorder &&
                q.Latitude <= SL.Latitude && q.Longitude >= SL.Longitude &&
                q.Latitude >= FL.Latitude && q.Longitude <= FL.Longitude);
            ObjectsList = new ObservableCollection<Objects>(pushpins.ToList());

            IQueryable<Lines> lines = db.Lines
                .Where(q=> q.Objects.Voltage >= VoltageBorder && q.Objects1.Voltage >= VoltageBorder &&
                q.Objects.Latitude <= SL.Latitude && q.Objects.Longitude >= SL.Longitude &&
                q.Objects.Latitude >= FL.Latitude && q.Objects.Longitude <= FL.Longitude &&
                q.Objects1.Latitude <= SL.Latitude && q.Objects1.Longitude >= SL.Longitude &&
                q.Objects1.Latitude >= FL.Latitude && q.Objects1.Longitude <= FL.Longitude);
            LinesList = new ObservableCollection<Lines>(lines.ToList());
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
                FirstObjectLocation = Convert.ToString(obj.Latitude).Replace(',', '.') + ", "
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

        private void ZoomLevelUp(object sender, RoutedEventArgs e)
        {
            myMap.ZoomLevel += 1;
            Filter();
        }

        private void ZoomLevelDown(object sender, RoutedEventArgs e)
        {
            myMap.ZoomLevel -= 1;
            Filter(); 
        }

        private void myMapMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                myMap.ZoomLevel += 1;
                Filter();
            }

            else
            {
                myMap.ZoomLevel -= 1;
                Filter();
            }
        }

        private void myMapMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Filter();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            Filter();
        }
    }
}