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

namespace blanc.ViewModels
{
    public class MiniTableViewModel: ViewModelBase
    {
        const string menuJsn = "Menu.json";
        const string billJsn = "Bill.json";
        

        private TableModel _selectedTable;
        public TableModel SelectedTable
        {
            get { return _selectedTable; }
            set
            {
                _selectedTable = value;
                OnPropertyChanged(nameof(SelectedTable));
                // You might also want to load or refresh the bill items for the selected table here.
            }
        }

        private ObservableCollection<Menu>? _menuItems;
        private ObservableCollection<Bill>? _billItems;
        public ObservableCollection<Bill> BillItems
        {
            get =>_billItems; 
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




        public ICommand AddToBillCommand { get; private set; }

        public MiniTableViewModel()
        {
            BillItems = new ObservableCollection<Bill>();
             MenuItems = new ObservableCollection<Menu>();
           AddToBillCommand = new RelayCommand(AddToBill);
            // Presumably, you would load your menu items here or in a method called by the constructor
            LoadMenuItems();
        }

        private void LoadMenuItems()
        {
            
            // Logic to load menu items from JSON into MenuItems collection
            string menuJson = File.ReadAllText(menuJsn);
            List<Menu> menuItems = JsonConvert.DeserializeObject<List<Menu>>(menuJson);
           /* List<Menu> menuItems = JsonConvert.DeserializeObject<List<Menu>>(menuJson);*/
            MenuItems = new ObservableCollection<Menu>(menuItems);

            OnPropertyChanged(nameof(MenuItems));
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
        }

       
       
    }
}
