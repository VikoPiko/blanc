using blanc.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace blanc.ViewModels
{
    public  class OrderWindowViewModel : ViewModelBase
    {
        //const string billJsn = "Bill.json";
        const string ordJsn = "Orders.json";

/*        private Bill _orderSelected;
        public Bill OrderSelected
        {
            get => _orderSelected;
            set
            {
                _orderSelected = value;
                OnPropertyChanged(nameof(OrderSelected));
            }
        }*/
        private Orders _items;// honestly ne znam dali mi trqbva tva :"D
        public Orders Items 
        {
            get => _items;
            set 
            {
                _items = value; OnPropertyChanged(nameof(Items));
            }
        } // honestly ne znam dali mi trqbva tva :"D
/*
        private ObservableCollection<Bill>? _billItems;
        public ObservableCollection<Bill>? BillItems // used to store the bill items which have been ordered; mai? 
        {
            get => _billItems;
            set { _billItems = value; OnPropertyChanged(nameof(BillItems)); }
        }
*/
        private ObservableCollection<Orders>? _itemsFromOrder; // honestly ne znam dali mi trqbva tva :"D
        public ObservableCollection<Orders>? ItemsFromOrder 
        {
            get => _itemsFromOrder;
            set 
            {
                _itemsFromOrder = value; OnPropertyChanged(nameof(ItemsFromOrder));
            }
        }// honestly ne znam dali mi trqbva tva :"D

        public OrderWindowViewModel() 
        {


            LoadOrders();
        }

        private void LoadOrders() // Loadva Itemite koito sa poruchani i sa v bill; Shte go polzvam za zarejdane na itemite v Orders
        {
            try
            {
                string orderJson = File.ReadAllText(ordJsn);
                List<Orders>? orderItems = JsonConvert.DeserializeObject<List<Orders>>(ordJsn);
                ItemsFromOrder = new ObservableCollection<Orders>(orderItems);
                OnPropertyChanged(nameof(ItemsFromOrder));
            }
            catch (FileNotFoundException ex)
            {
                Debug.WriteLine("Файлът не може да бъде намерен: " + ex.FileName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Възникна грешка при зареждане на менюто: " + ex.Message);
            }
        }
    }
}
