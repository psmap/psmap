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

        public Form(string DatabaseModification)
        {
            InitializeComponent();
            Save_button.Click += DatabaseModification == "Update"
                ? new RoutedEventHandler(Update)
                : new RoutedEventHandler(Insert);

            VoltageLevelSelection.ItemsSource = db.Voltage_levels.ToList();
            TypeSelection.ItemsSource = db.Types.ToList();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Insert(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            Objects objects_list = element.DataContext as Objects;

            if (objects_list.Name != null)
            {
                db.Objects.Add(objects_list);
                db.SaveChanges();
                MessageBox.Show("Данные успешно добавлены", "Добавление данных", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                Close();
            }
            else MessageBox.Show("Название объекта не указано", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            Objects objects_list = element.DataContext as Objects;
            Objects note = db.Objects.Find(objects_list.Id);


            if (objects_list.Name != "")
            {
                note.Name = objects_list.Name;
                note.Latitude = objects_list.Latitude;
                note.Longitude = objects_list.Longitude;
                note.Type = objects_list.Type;
                note.Voltage = objects_list.Voltage;
                db.SaveChanges();

                MessageBox.Show("Название объекта не указано", "Изменение данных", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                Close();
            }

            else MessageBox.Show("Некоторые значения не указаны", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}