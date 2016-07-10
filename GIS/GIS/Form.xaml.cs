using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Microsoft.Maps.MapControl.WPF;
using System.Windows.Shapes;

namespace GIS
{
    public partial class Form : Window
    {
        ObjectsEntities db = new ObjectsEntities();

        public static void insertToDataBase(List<Objects> objects_list)
        {

            ObjectsEntities db = new ObjectsEntities();
            foreach (Objects obj in objects_list)
                db.Objects.Add(obj);

            db.SaveChanges();
        }

        public Form(Location pinLocation, string qwe)
        {
            InitializeComponent();

            string[] substrings = Convert.ToString(pinLocation).Split(',');
            string latitude = Convert.ToString(substrings[0] + "," + substrings[1]);
            string longitude = Convert.ToString(substrings[2] + "," + substrings[3]);
            ObjectLatitude.Text = latitude;
            ObjectLongitude.Text = longitude;

            if (qwe != "123")
            {
                Save_button.Click += new RoutedEventHandler(Update);

                string[] qwea = Convert.ToString(qwe).Split('\n', ' ');

                string type = qwea[4];
                var TypeId = db.Types
                .Where(b => b.Type == type)
                .FirstOrDefault();

                double voltage = Convert.ToDouble(qwea[6]);
                var VoltageId = db.Voltage_levels
                .Where(b => b.Voltage == voltage)
                .FirstOrDefault();

                ObjectName.Text = qwea[0];
                TypeSelection.SelectedValue = Convert.ToInt32(TypeId.Id);
                VoltageLevelSelection.SelectedValue = Convert.ToInt32(VoltageId.Id);

                Save_button.Tag = ObjectName.Text;
            }

            else
            {
                Save_button.Click += new RoutedEventHandler(Insert);
            }

            var VoltageQuery = from b in db.Voltage_levels
                               orderby b.Id
                               select b;

            ObservableCollection<Voltage_levels> levels_list = new ObservableCollection<Voltage_levels>(VoltageQuery.ToList());
            VoltageLevelSelection.ItemsSource = levels_list;

            var TypeQuery = from b in db.Types
                               orderby b.Id
                               select b;

            ObservableCollection<Types> types_list = new ObservableCollection<Types>(TypeQuery.ToList());
            TypeSelection.ItemsSource = types_list;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Insert(object sender, RoutedEventArgs e)
        {
            Objects objects_list = new Objects
            {
                Name = ObjectName.Text,
                Latitude = Math.Round(Convert.ToDouble(ObjectLatitude.Text), 4),
                Longitude = Math.Round(Convert.ToDouble(ObjectLongitude.Text), 4),
                Type = Convert.ToInt32(TypeSelection.SelectedValue),
                Voltage = Convert.ToInt32(VoltageLevelSelection.SelectedValue)
            };

            db.Objects.Add(objects_list);
            db.SaveChanges();

            //MessageBox.Show("Данные успешно добавлены", "Добавление данных", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            Close();
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            var element = sender as FrameworkElement;
            string name = Convert.ToString(element.Tag);
          
            var query = db.Objects
                .Where(b => b.Name == name)
                .FirstOrDefault();

            query.Name = ObjectName.Text;
            query.Latitude = Math.Round(Convert.ToDouble(ObjectLatitude.Text),4);
            query.Longitude = Math.Round(Convert.ToDouble(ObjectLongitude.Text),4);
            query.Type = Convert.ToInt32(TypeSelection.SelectedValue);
            query.Voltage = Convert.ToInt32(VoltageLevelSelection.SelectedValue);

            db.SaveChanges();
            MessageBox.Show("Данные успешно обновлены", "Изменение данных", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            Close();
        }
    }
}