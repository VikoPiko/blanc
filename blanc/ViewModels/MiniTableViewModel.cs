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

namespace blanc.ViewModels
{
    public class MiniTableViewModel: ViewModelBase
    {
        const string menuJsn = "Menu.json";
        private ObservableCollection<Menu>? _menuItems;
        private Menu? _selectedItems;
        public ObservableCollection<Menu> MenuItems
        {
            get { return _menuItems; }
            set { _menuItems = value; OnPropertyChanged(nameof(MenuItems)); }
        }



        
        public ICommand AddToBillCommand { get; private set; }

        public MiniTableViewModel()
        {
           
            MenuItems = new ObservableCollection<Menu>();
           AddToBillCommand = new RelayCommandWithObject(AddToBillExecute, AddToBillCanExecute);
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

        private bool AddToBillCanExecute(object arg)
        {
            // Logic to determine if AddToBillCommand can execute
            return true; // For now, we'll just return true
        }

        private void AddToBillExecute(object parameter)
        {
            if (parameter is MenuItem menuItem)
            {
                // Logic to add menuItem to the SelectedTable's bill
            }
        }
       
    }
}
