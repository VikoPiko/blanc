using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using blanc.Models;
using blanc.ViewModels.Commands;
using blanc.Views;
using Newtonsoft.Json;

namespace blanc.ViewModels
{
    public class KitchenViewModel : ViewModelBase
    {
        const string orderJson = "Orders.json";

        private ObservableCollection<Orders> _orders;

        public ObservableCollection<Orders> _Orders
        {
            get { return _orders; }
            set { _orders = value; OnPropertyChanged(nameof(_Orders)); }
        }
        private Orders _selectedOrderItem;
        public Orders SelectedOrderItem
        {
            get { return _selectedOrderItem; }
            set
            {
                _selectedOrderItem = value;
                OnPropertyChanged(nameof(SelectedOrderItem));

            }
        }
        /*     public ICommand StartOrderCommand { get; private set; }
             public ICommand CancelOrderCommand { get; private set; }
             public ICommand OrderReadyCommand { get; private set; }*/

        public ICommand OpenOrderWindowCommand { get; private set; }
        public ICommand RemoveOrderCommand { get; private set; }

        public KitchenViewModel()
        {
            _orders = new ObservableCollection<Orders>();
            OpenOrderWindowCommand = new RelayCommand(OpenOrderWindow);
            RemoveOrderCommand = new RelayCommand(RemoveOrder, CanRemoveOrder);

            _Orders = new ObservableCollection<Orders>();
            string rawJson = File.ReadAllText(orderJson);
            List<Orders>? order = JsonConvert.DeserializeObject<List<Orders>>(rawJson);

            if (order != null)
            {
                foreach (var ord in order)
                {
                    _Orders.Add(ord);
                }
            }
        }

        private bool CanRemoveOrder()
        {
            return _selectedOrderItem != null;
        }

        private void RemoveOrder()
        {
            if (_selectedOrderItem != null)
            {
                _Orders.Remove(_selectedOrderItem);

                string json = JsonConvert.SerializeObject(_Orders, Formatting.Indented);
                File.WriteAllText(orderJson, json);
            }
        }

        private Dictionary<int, OrderWindow> openTables = new Dictionary<int, OrderWindow>();
        public void OpenOrderWindow() 
        {
            if (this.SelectedOrderItem != null)
            {
                // Зареждате всички маси от JSON файла
                string json = File.ReadAllText(orderJson);
                List<Orders>? tables = JsonConvert.DeserializeObject<List<Orders>>(json);
                // Ако съответната маса е намерена
                if (SelectedOrderItem != null && !openTables.ContainsKey(SelectedOrderItem.OrderID))
                {
                    // Create the window and add it to the dictionary
                    var orderWindowVar = new OrderWindow(SelectedOrderItem);
                    orderWindowVar.Closed += (sender, args) => openTables.Remove(SelectedOrderItem.OrderID);
                    openTables[SelectedOrderItem.OrderID] = orderWindowVar;
                    orderWindowVar.Show();
                }
            }
        }

/*        public int tableNum { get; set; } // number of MASA
        public string itemNm { get; set; } // name of HRANA
        public int qntty { get; set; } // quantity of HRANA
        public int ordId { get; set; } // order numero :)*/
    }
}
