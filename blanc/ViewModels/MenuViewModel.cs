using blanc.Models;
using blanc.ViewModels.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace blanc.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {

        const string menuItems = "Menu.json";

        private ObservableCollection<Menu>? _items;
        private Menu? _selectedItems;
        public ObservableCollection<Menu> Items
        {
            get { return _items; }
            set { _items = value; OnPropertyChanged(nameof(Items)); }
        }

        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public float ItemPrice { get; set; }

        public ICommand AddItemCommand { get; private set; }

        public ICommand RemoveItemCommand { get; private set; }

       
        public MenuViewModel()
        {
            AddItemCommand = new RelayCommand(AddItem);
            Items = new ObservableCollection<Menu>();
            RemoveItemCommand = new RelayCommand(RemoveItem);


            string rawJson = File.ReadAllText(menuItems);

            List<Menu>? items = JsonConvert.DeserializeObject<List<Menu>>(rawJson);

            if (items != null)
            {
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
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

            List<Menu> items = new List<Menu>();

            foreach (Menu it in Items)
            {
                items.Add(new Menu()
                {
                    ID_Menu = it.ID_Menu,
                    Name = it.Name,
                    Description = it.Description,
                    Price = it.Price,
                });
            }

            string json = JsonConvert.SerializeObject(items, Formatting.Indented);

            
            File.WriteAllText(menuItems, json);
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
            if (SelectedItem == null)
            {
                return; // Nothing to remove
            }

            // Read the file
            string jsonContent = File.ReadAllText(menuItems);
            List<Menu> items = JsonConvert.DeserializeObject<List<Menu>>(jsonContent);

            // Finding and removing the chosen ONE
            var itemToRemove = items.FirstOrDefault(r => r.ID_Menu == SelectedItem.ID_Menu);
            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);

                // Serializing the object again into JSON
                string updatedJson = JsonConvert.SerializeObject(items, Formatting.Indented);

                // Saving the JSON file again
                File.WriteAllText(menuItems, updatedJson);
            }

            //Update front-end upon  deleting
            if (SelectedItem != null)
            {
                Items.Remove(SelectedItem);
            }
        }
    }
}
