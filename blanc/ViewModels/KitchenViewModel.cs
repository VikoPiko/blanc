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

        public ObservableCollection<Orders> _Orders 
        {
            get {  return _orders; } 
            set { _orders = value; OnPropertyChanged(nameof(_Orders)); }
        }

       

        public ICommand StartOrderCommand { get; private set; }
        public ICommand CancelOrderCommand { get; private set; }
        public ICommand OrderReadyCommand { get; private set; }
   
        public KitchenViewModel() 
        {
          /*  StartOrderCommand = new RelayCommand(StartOrder);
            CancelOrderCommand = new RelayCommand(CancelOrder);
            OrderReadyCommand = new RelayCommand(OrderReady);*/
         
        }

        public int tableNum { get; set; } // number of MASA
        public string itemNm { get; set; } // name of HRANA
        public int qntty { get; set; } // quantity of HRANA
       /* public OrderStatus oStatus { get; set; } // status of the PORUCHKA*/

       
    

       /* private void OrderReady()
        {
            Orders order = new Orders();
            order.Status = SelectedOrder.Ready;
        }

        private void CancelOrder()
        {
            Orders order = new Orders();
            order.Status = SelectedOrder.Canceled;
        }

        private void StartOrder()
        {
            Orders order = new Orders();
            order.Status = SelectedOrder.Started;
        }*/
    }
}
