using BusinessAccountantService.Data;
using BusinessAccountantService.Managers;
using BusinessAccountantService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BusinessAccountantService
{
    /// <summary>
    /// Interaction logic for ServiceListPage.xaml
    /// </summary>
    public partial class ServiceListPage : Page
    {
        private readonly ServiceManager _serviceManager = new();
        private ICollectionView _servicesView;

        public ServiceListPage()
        {
            InitializeComponent();
            LoadServices();
        }

        private void LoadServices()
        {
            var services = _serviceManager.GetAllServices();
            _servicesView = CollectionViewSource.GetDefaultView(services);
            _servicesView.Filter = ServiceFilterPredicate;
            ServicesGrid.ItemsSource = _servicesView;
        }

        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            AddServiceWindow addWindow = new AddServiceWindow { Owner = Application.Current.MainWindow };
            if (addWindow.ShowDialog() == true) LoadServices();
        }

        private void EditService_Click(object sender, RoutedEventArgs e)
        {
            if (ServicesGrid.SelectedItem is Models.ServiceItem selectedService)
            {
                EditServiceWindow editWindow = new EditServiceWindow(selectedService)
                {
                    Owner = Application.Current.MainWindow
                };
                if (editWindow.ShowDialog() == true) LoadServices();
            }
            else
            {
                MessageBox.Show("Выберите услугу для редактирования!");
            }
        }

        private void DeleteService_Click(object sender, RoutedEventArgs e)
        {
            if (ServicesGrid.SelectedItem is Models.ServiceItem selectedService)
            {
                if (MessageBox.Show($"Удалить услугу '{selectedService.Name}'?", "Удаление", 
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    _serviceManager.DeleteService(selectedService.Name);
                    LoadServices();
                }
            }
            else
            {
                MessageBox.Show("Выберите услугу для удаления!");
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _servicesView?.Refresh();
        }

        private bool ServiceFilterPredicate(object obj)
        {
            if (string.IsNullOrWhiteSpace(SearchBox.Text)) return true;
            var service = obj as Models.ServiceItem;
            if (service == null) return false;

            string query = SearchBox.Text.ToLower();
            return (service.Name?.ToLower().Contains(query) ?? false);
        }

        private void ServicesGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ServicesGrid.SelectedItem is Models.ServiceItem selectedService)
            {
                EditServiceWindow editWindow = new EditServiceWindow(selectedService)
                {
                    Owner = Application.Current.MainWindow
                };
                if (editWindow.ShowDialog() == true) LoadServices();
            }
        }
    }
}