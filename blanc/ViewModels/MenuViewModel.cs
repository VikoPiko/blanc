using blanc.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace blanc.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private ObservableCollection<Menu>? _items;
        private Menu? _selectedItems;
        public ObservableCollection<Menu> Items
        {
            get { return _items; }
            set { _items = value; OnPropertyChanged(nameof(Items)); }
        }

        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public float ItemPrice { get; set; }

        public ICommand AddItemCommand { get; private set; }

        public ICommand RemoveItemCommand { get; private set; }

        public MenuViewModel()
        {
            RemoveItemCommand = new RelayCommand(RemoveItem, CanRemoveItem);
            Items = new ObservableCollection<Menu>();
            AddItemCommand = new RelayCommand(AddItem);

            var burger = new Menu
            {
                ID_Menu = 0,
                Name = "Burger",
                Description = "Tasty",
                Price = 8.99f
            };
            Items.Add(burger);
            var fries = new Menu
            {
                ID_Menu = 1,
                Name = "Fries",
                Description = "Crispy",
                Price = 3.99f
            };
            Items.Add(fries);
            var salad = new Menu
            {
                ID_Menu = 2,
                Name = "Salad",
                Description = "Boring",
                Price = 14.99f
            };
            Items.Add(salad);
            var steak = new Menu
            {
                ID_Menu = 3,
                Name = "Steak",
                Description = "SO FUCKING JUICY",
                Price = 11.99f
            };
            Items.Add(steak);
            var pizza = new Menu
            {
                ID_Menu = 4,
                Name = "Pizza",
                Description = "Just pizza",
                Price = 6.99f
            };
            Items.Add(pizza);
        }

        private void AddItem()
        {
            var newItem = new Menu
            {
                ID_Menu = Items.Count + 1,
                Name = ItemName,
                Description = ItemDescription,
                Price = ItemPrice
            };
            Items.Add(newItem);
        }

        public Menu? SelectedItem
        {
            get { return _selectedItems; }
            set { _selectedItems = value; OnPropertyChanged(nameof(SelectedItem)); }
        }

        private bool CanRemoveItem()
        {
            return SelectedItem != null;
        }

        private void RemoveItem()
        {
            if (SelectedItem != null)
            {
                Items.Remove(SelectedItem);
            }
        }
    }
}
