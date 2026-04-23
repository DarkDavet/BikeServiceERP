using BusinessAccountantService.Managers;
using BusinessAccountantService.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
    /// Interaction logic for EditServiceRepairRecordWindow.xaml
    /// </summary>
    public partial class EditServiceRepairRecordWindow : Window
    {
        private readonly RepairManager _repairManager = new();
        public RepairItem SelectedResult { get; private set; }

        private RepairItem _originalItem;
        private RepairItem _editedItem;
        private ICollectionView _serviceView;

        public EditServiceRepairRecordWindow(RepairItem item)
        {
            InitializeComponent();
            
            _originalItem = item;
            _editedItem = new RepairItem
            {
                ProductId = item.ProductId,
                Name = item.Name,
                Price = item.Price,
                Quantity = item.Quantity,
                PurchasePrice = item.PurchasePrice
            };

            // Populate the UI with current values
            ServiceSearchBox.Text = item.Name;
            PriceBox.Text = item.Price.ToString("F2");
            
            // Load service suggestions
            LoadServiceSuggestions();
        }

        private void LoadServiceSuggestions()
        {
            // Загружаем список один раз
            var allServices = _repairManager.GetServiceSuggestions("");
            _serviceView = CollectionViewSource.GetDefaultView(allServices);

            // Привязываем источник один раз!
            ServiceSearchBox.ItemsSource = _serviceView;
            ServiceSearchBox.Focus();
        }

        // Поиск в прайс-листе по мере ввода текста
        private void ServiceSearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Enter || e.Key == Key.Escape) return;

            // Берем текст прямо из TextBox внутри ComboBox, чтобы избежать лагов
            var textBox = (TextBox)ServiceSearchBox.Template.FindName("PART_EditableTextBox", ServiceSearchBox);
            string query = textBox?.Text.ToLower() ?? ServiceSearchBox.Text.ToLower();

            _serviceView.Filter = item =>
            {
                if (string.IsNullOrEmpty(query)) return true;
                return (item as ServiceItem).Name.ToLower().Contains(query);
            };

            ServiceSearchBox.IsDropDownOpen = true;

            // Возвращаем курсор в конец, чтобы текст не затирался при автодополнении
            if (textBox != null)
            {
                textBox.SelectionStart = textBox.Text.Length;
                textBox.SelectionLength = 0;
            }
        }

        // Если выбрали готовую услугу — подставляем её цену
        private void ServiceSearchBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ServiceSearchBox.SelectedItem is ServiceItem selected)
            {
                PriceBox.Text = selected.Price.ToString();
                PriceBox.Focus();
                PriceBox.SelectAll();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Validate input
            string name = ServiceSearchBox.Text.Trim();
            decimal.TryParse(PriceBox.Text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price);

            if (string.IsNullOrWhiteSpace(name) || price <= 0)
            {
                MessageBox.Show("Укажите корректное название и цену работы!");
                return;
            }

            // Update the item with new values
            _editedItem.Name = name;
            _editedItem.Price = price;
            // Note: Total is a read-only property, calculated from Quantity * Price

            // Create the final result for the caller
            SelectedResult = new RepairItem
            {
                ProductId = _editedItem.ProductId,
                Name = _editedItem.Name,
                Price = _editedItem.Price,
                Quantity = _editedItem.Quantity,
                PurchasePrice = _editedItem.PurchasePrice
                // Total is calculated automatically
            };

            this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}