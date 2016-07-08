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
        ObjectEntities context = new ObjectEntities();
        List<Voltage_levels> levels_list;
        List<Types> types_list;


   




        void myMap_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {


            var element = sender as FrameworkElement;

            
            System.Windows.Controls.Label label = new System.Windows.Controls.Label();
            label.Content = "Voltage: " + element.GetType() + "\nType: " + element.Name + "\nKoor: ";
            label.Foreground = new SolidColorBrush(Colors.DarkBlue);
            label.Background = new SolidColorBrush(Colors.WhiteSmoke);
            label.FontSize = 10;
            MapLayer.SetPosition(label, new Location(54, 83));
            myMap.Children.Add(label);
            if (myMap.Name == element.Name) { }




        }

        public MainWindow()
        {

            InitializeComponent();
            DataContext = this;



          






            using (var db = new ObjectEntities())
            {
                var VoltageQuery = from b in db.Voltage_levels
                                   orderby b.Id
                                   select b;
                levels_list = VoltageQuery.ToList();

                QQQ.ItemsSource = levels_list;

                var TypeQuery = from b in db.Types
                                orderby b.Id
                                select b;

                types_list = TypeQuery.ToList();
                KKK.ItemsSource = types_list;


                var ObjectsQuery = from b in db.Objects
                              orderby b.Id
                              select b;

               


        

                foreach (var idname in ObjectsQuery)
                {
                    Pushpin pin = new Pushpin();
                    pin.Location = new Location(Convert.ToDouble(idname.Latitude), Convert.ToDouble(idname.Longitude));
                    pin.Uid = Convert.ToString(idname.Id);
                    pin.Name = "name"+Convert.ToString(idname.Name);
                    pin.MouseDown +=
                new MouseButtonEventHandler(myMap_MouseLeftButtonDown);

                    myMap.Children.Add(pin);
                }




            }

        }

        void addNewPolyline()
        {
            MapPolyline polyline = new MapPolyline();
            polyline.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.DarkGray);
            polyline.StrokeThickness = 5;
            polyline.Opacity = 0.7;
            polyline.Locations = new LocationCollection() {
        new Location(53.355195,83.769511),
        new Location(53.337574,83.788342)};

            myMap.Children.Add(polyline);
        }

        private void MapWithPushpins_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

          //  MessageBox.Show("123"+ e.ChangedButton, "Ошибка при вводе имени",
      //  MessageBoxButton.OK, MessageBoxImage.None);

            e.Handled = true;
            Point mousePosition = e.GetPosition(this);
            Location pinLocation = myMap.ViewportPointToLocation(mousePosition);



            Pushpin pin = new Pushpin();
            pin.Location = pinLocation;

            myMap.Children.Add(pin);

            System.Windows.Controls.Label label = new System.Windows.Controls.Label();
            label.Content = "Voltage: " + levels_list[0].Voltage + "kV\nType: " + types_list[0].Type + "kV\nKoor: " + pinLocation;
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