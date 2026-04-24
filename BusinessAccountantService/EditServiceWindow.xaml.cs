using BusinessAccountantService.Managers;
using BusinessAccountantService.Models;
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

namespace BusinessAccountantService
{
    /// <summary>
    /// Interaction logic for EditServiceWindow.xaml
    /// </summary>
    public partial class EditServiceWindow : Window
    {
        private readonly ServiceManager _serviceManager = new();
        private readonly Models.ServiceItem _serviceItem;

        public EditServiceWindow(Models.ServiceItem serviceItem)
        {
            InitializeComponent();
            _serviceItem = serviceItem;
            ServiceNameTextBox.Text = serviceItem.Name;
            ServicePriceTextBox.Text = serviceItem.Price.ToString("F2");
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ServiceNameTextBox.Text))
            {
                MessageBox.Show("Введите название услуги!");
                return;
            }

            if (!decimal.TryParse(ServicePriceTextBox.Text, out decimal price))
            {
                MessageBox.Show("Введите корректную цену услуги!");
                return;
            }

            try
            {
                _serviceManager.UpdateService(_serviceItem.Name, ServiceNameTextBox.Text.Trim(), price);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении услуги: {ex.Message}");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}