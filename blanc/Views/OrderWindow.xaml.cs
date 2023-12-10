using blanc.Models;
using blanc.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace blanc.Views
{
    public partial class OrderWindow : Window
    {
        const string orderJsn = "Orders.json";
        public OrderWindow()
        {
            try
            {
                InitializeComponent();
/*                OrderWindowViewModel viewModel = new OrderWindowViewModel();

                this.DataContext = viewModel;*/
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while opening the order XDD.");
            }
        }
        public OrderWindow(Orders selectedOrder) : this()
        {
            var viewModel = this.DataContext as OrderWindowViewModel;
            if (viewModel != null)
            {
                //viewModel.SelectedOrder = selectedOrder;
            }
        }

        private void OrdersClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OrderWindowViewModel? viewModel = this.DataContext as OrderWindowViewModel;
            if (viewModel != null) 
            {
                int currentOrderId = viewModel.Items.OrderID; // tuka crashva 
                string orderData = File.ReadAllText(orderJsn);
                var allOrders = JsonConvert.DeserializeObject<List<Orders>>(orderData) ?? new List<Orders>();
                var tableOrders = allOrders.Where(b => b.OrderID == currentOrderId);
                viewModel.ItemsFromOrder = new ObservableCollection<Orders>(tableOrders);
            }
        }
    }
}
