using blanc.Models;
using blanc.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Menu = blanc.Models.Menu;

namespace blanc.Views
{
    public partial class MiniTable : Window
    {
        private TablesView _mainWindow;

        public MiniTable(TablesView mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            
        }
        public MiniTable(TableModel selectedTable, List<Menu> menuItems)
        {
            InitializeComponent();
            // Предполагаме, че имате ViewModel, който управлява данните за MiniTable
            var viewModel = new TablesViewModel();
            viewModel.SelectedTable = selectedTable; // Сетвате модела на ViewModel
            viewModel.MenuItems = menuItems;
           
            this.DataContext = viewModel; // Задавате DataContext на прозореца
         
         
           
        }
       
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Check if left mouse button is pressed
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Begin dragging the window
                this.DragMove();
            }
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        // SHOULD CLOSE ONLY TABLE 
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

       
    }
}
