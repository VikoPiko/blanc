using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using blanc.Models;
using blanc.ViewModels.Commands;

namespace blanc.ViewModels
{
    public class KitchenViewModel : ViewModelBase
    {
        const string orderJson = "Orders.json";

        private ObservableCollection<Orders> _orders;
        private Orders _selectedOrder;

        public ObservableCollection<Orders> Orders 
        {
            get {  return _orders; } 
            set { _orders = value; OnPropertyChanged(nameof(Orders)); }
        }

        public ICommand StartOrderCommand { get; private set; }
        public ICommand CancelOrderCommand { get; private set; }
        public ICommand OrderReadyCommand { get; private set; }
        public ICommand AddOrderCommand { get; private set; }
        public KitchenViewModel() 
        {
            StartOrderCommand = new RelayCommand(StartOrder);
            CancelOrderCommand = new RelayCommand(CancelOrder);
            OrderReadyCommand = new RelayCommand(OrderReady);
            AddOrderCommand = new RelayCommand(AddOrder);
        }

        public int tableNum { get; set; } // number of MASA
        public string itemNm { get; set; } // name of HRANA
        public int qntty { get; set; } // quantity of HRANA
        public OrderStatus oStatus { get; set; } // status of the PORUCHKA

        private void AddOrder()
        {
            var newOrder = new Orders
            {
                OrderID = Orders.Count + 1,
                TableNumber = tableNum,
                ItemName = itemNm,
                Quantity = qntty,
                Status = oStatus
            };
            Orders.Add(newOrder);
            List<Orders> orders = new List<Orders>();

            foreach (Orders order in Orders) 
            {
                orders.Add(new Orders()
                {
                    OrderID = order.OrderID,
                    TableNumber = order.TableNumber,
                    ItemName = order.ItemName,
                    Quantity = order.Quantity,
                    Status = order.Status
                });
            }
        }

        public Orders SelectedOrder
        {
            get { return _selectedOrder; }
            set { _selectedOrder = value; OnPropertyChanged(nameof(SelectedOrder)); }
        }

        private void OrderReady()
        {
            Orders order = new Orders();
            order.Status = OrderStatus.Ready;
        }

        private void CancelOrder()
        {
            Orders order = new Orders();
            order.Status = OrderStatus.Canceled;
        }

        private void StartOrder()
        {
            Orders order = new Orders();
            order.Status = OrderStatus.Started;
        }
    }
}
