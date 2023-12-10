using blanc.Models;
using blanc.Views;
using blanc.ViewModels.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Menu = blanc.Models.Menu;
using System.Windows.Documents;
using System.Diagnostics;
using static blanc.ViewModels.TablesViewModel;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Collections;

namespace blanc.ViewModels
{
    public class MiniTableViewModel : ViewModelBase
    {
        const string menuJsn = "Menu.json";
        const string billJsn = "Bill.json";
        const string tablesJsn = "Tables.json";
        const string sumData = "Sum.json";


        private TableModel _selectedTable;
        public TableModel SelectedTable
        {
            get { return _selectedTable; }
            set
            {
                _selectedTable = value;
                OnPropertyChanged(nameof(SelectedTable));

            }
        }

        private Bill _selectedOrder;
        public Bill SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));

            }
        }

        private ObservableCollection<Orders> _orders;
        public ObservableCollection<Orders> _Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(_Orders));
            }

        }

        private ObservableCollection<Menu>? _menuItems;
        private ObservableCollection<Bill>? _billItems;


        public Action CloseAction { get; set; }
        public ObservableCollection<Bill> BillItems
        {
            get => _billItems;
            set { _billItems = value; OnPropertyChanged(nameof(BillItems)); }
        }
        private Menu? _selectedItems;
        public ObservableCollection<Menu> MenuItems
        {
            get => _menuItems;
            set { _menuItems = value; OnPropertyChanged(nameof(MenuItems)); }
        }


        private Menu _selectedMenuItem;
        public Menu SelectedItem
        {
            get => _selectedMenuItem;
            set
            {
                _selectedMenuItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }


        private double _sum;
        public double Sum
        {
            get => _sum;
            set
            {
                _sum = value;
                OnPropertyChanged(nameof(Sum));
            }
        }

        /*private ObservableCollection<Bill> _selectedOrders = new ObservableCollection<Bill>();

        public ObservableCollection<Bill> SelectedOrders
        {
            get => _selectedOrders;
            set
            {
                _selectedOrders = value;
                OnPropertyChanged(nameof(SelectedOrders));
            }
        }*/

        public ICommand ClearTableCommand { get; private set; }


        public ICommand CalculateSumCommand { get; private set; }

        public ICommand RemoveFromBillCommand { get; private set; }
        public ICommand AddToBillCommand { get; private set; }
        public ICommand AddToOrderCommand { get; private set; }


        public MiniTableViewModel()
        {
            AddToOrderCommand = new RelayCommand(AddToOrder);
            BillItems = new ObservableCollection<Bill>();
            MenuItems = new ObservableCollection<Menu>();
            _Orders = new ObservableCollection<Orders>();
            AddToBillCommand = new RelayCommand(AddToBill);
            RemoveFromBillCommand = new RelayCommandWithObject(RemoveFromBill);
            CalculateSumCommand = new RelayCommand(CalculateSum);
             ClearTableCommand = new RelayCommand(ClearTable);

            // Presumably, you would load your menu items here or in a method called by the constructor
            LoadMenuItems();
        }



        public void CalculateSum()
        {
            if (BillItems != null)
            {
                double calculateSum = BillItems.Sum(item => item.Price);
                Sum = calculateSum;
            }
        }

        private void LoadMenuItems()
        {
            try
            {
                string menuJson = File.ReadAllText(menuJsn);
                List<Menu> menuItems = JsonConvert.DeserializeObject<List<Menu>>(menuJson);
                MenuItems = new ObservableCollection<Menu>(menuItems);
                OnPropertyChanged(nameof(MenuItems));
            }
            catch (FileNotFoundException ex)
            {
                // Логване на грешката или показване на съобщение за грешка
                Debug.WriteLine("Файлът не може да бъде намерен: " + ex.FileName);
            }
            catch (Exception ex)
            {
                // Обработка на други видове изключения
                Debug.WriteLine("Възникна грешка при зареждане на менюто: " + ex.Message);
            }
        }

        private void AddToBill()
        {
            if (SelectedItem != null)
            {
                var billItem = new Bill
                {
                    tableId = this.SelectedTable.tableId,
                    Name = SelectedItem.Name,
                    Price = SelectedItem.Price,
                    Quantity = 1
                };
                BillItems.Add(billItem);
            }
            CalculateSum();
        }


        private void RemoveFromBill(object parameter)
        {
            var itemToRemove = parameter as Bill;
            if (itemToRemove != null)
            {
                BillItems.Remove(itemToRemove);
            }
            CalculateSum();
        }

        private void AddToOrder()
        {
            foreach (var selectedOrder in BillItems)
            {
                var newOrder = new Orders
                {
                    OrderID = _Orders.Count + 1,
                    TableNumber = selectedOrder.tableId,
                    ItemName = selectedOrder.Name,
                    Quantity = selectedOrder.Quantity,
                };
                _Orders.Add(newOrder);
            }

            string orders = JsonConvert.SerializeObject(_Orders, Formatting.Indented);
            File.WriteAllText("Orders.json", orders);



        }



        private void ClearTable()
        {
            if (SelectedTable != null)
            {
                // Read the contents of Bill.json
                string billJson = File.ReadAllText(billJsn);
                List<Bill> bills = JsonConvert.DeserializeObject<List<Bill>>(billJson);

                // Filter the records that are NOT for the selected table
                bills = bills.Where(bill => bill.tableId != SelectedTable.tableId).ToList();

                // Write the filtered data back to Bill.json
                File.WriteAllText(billJsn, JsonConvert.SerializeObject(bills));

                // Read and filter Tables.json
                string tablesJson = File.ReadAllText(tablesJsn);
                List<TableModel> tables = JsonConvert.DeserializeObject<List<TableModel>>(tablesJson);
                tables = tables.Where(table => table.tableId != SelectedTable.tableId).ToList();

                // Write the filtered data back to Tables.json
                File.WriteAllText(tablesJsn, JsonConvert.SerializeObject(tables));

                // Update the UI
                BillItems.Clear();
                _Orders.Clear(); // Clear orders as well
                                 // Additional UI update logic can be added here
                CloseAction?.Invoke();
            }


        }
    }
}
