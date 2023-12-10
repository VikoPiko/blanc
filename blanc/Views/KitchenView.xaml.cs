using System.Windows;
using System.Windows.Controls;
using blanc.Models;
using blanc.ViewModels;

namespace blanc.Views
{
    /// <summary>
    /// Interaction logic for KitchenView.xaml
    /// </summary>
    public partial class KitchenView : UserControl
    {
        public KitchenView()
        {
            InitializeComponent();
            this.DataContext = new KitchenViewModel();
        }
        private void OpenOrderWindow_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Orders order)
            {
                OrderWindow orderWindow = new OrderWindow();
                orderWindow.ShowDialog();
            }
        }
    }
}
