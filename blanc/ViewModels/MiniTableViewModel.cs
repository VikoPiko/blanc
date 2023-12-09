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




        /*public ICommand ClearTableCommand { get; private set; }*/

       public ICommand CalculateSumCommand { get; private set; }

        public ICommand RemoveFromBillCommand { get; private set; }
        public ICommand AddToBillCommand { get; private set; }
     

        public MiniTableViewModel()
        {
            
            BillItems = new ObservableCollection<Bill>();
            MenuItems = new ObservableCollection<Menu>();
            AddToBillCommand = new RelayCommand(AddToBill);
            RemoveFromBillCommand = new RelayCommandWithObject(RemoveFromBill);
            CalculateSumCommand = new RelayCommand(CalculateSum);
            /* ClearTableCommand = new RelayCommand(ClearTable);*/

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




        /*  private void ClearTable()
          {
              if (SelectedTable != null)
              {
                  *//*
                  // Прочетете съдържанието на Bill.json
                  string billJson = File.ReadAllText(billJsn);
                  List<Bill> bills = JsonConvert.DeserializeObject<List<Bill>>(billJson);

                  // Филтриране на записите, които НЕ са за избраната маса
                  bills = bills.Where(bill => bill.tableId != SelectedTable.tableId).ToList();

                  // Запис на филтрираните данни обратно в Bill.json
                  File.WriteAllText(billJsn, JsonConvert.SerializeObject(bills));

                  *//*

                  string billJson = File.ReadAllText(billJsn);
                  List<Bill> bills = JsonConvert.DeserializeObject<List<Bill>>(billJson);
                  bills = bills.Where(bill => bill.tableId != SelectedTable.tableId).ToList();
                  File.WriteAllText(billJsn, JsonConvert.SerializeObject(bills));

                  // Прочетете и филтрирайте Tables.json
                  string tablesJson = File.ReadAllText(tablesJsn);
                  List<TableModel> tables = JsonConvert.DeserializeObject<List<TableModel>>(tablesJson);
                  tables = tables.Where(table => table.tableId != SelectedTable.tableId).ToList();
                  File.WriteAllText(tablesJsn, JsonConvert.SerializeObject(tables));


                  // Обновяване на UI
                  BillItems.Clear();
                  // Може да има още логика за обновяване на UI тук
                  CloseAction?.Invoke();


              }*/

 

    }
}
