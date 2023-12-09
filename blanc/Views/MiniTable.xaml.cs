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
using Menu = blanc.Models.Menu;

namespace blanc.Views
{
    public partial class MiniTable : Window
    {
        private TablesView _mainWindow;
        const string billJsn = "Bill.json";
        public MiniTable()
        {
            try
            {
                InitializeComponent();
                MiniTableViewModel viewModel = new MiniTableViewModel();
               
                this.DataContext = viewModel;
            }
            catch (Exception ex)
            {
                // Handle the exception
                MessageBox.Show("An error occurred while opening the table.");
            }
        }
        public MiniTable(TableModel selectedTable):this()
        {
            var viewModel = this.DataContext as MiniTableViewModel;
            if (viewModel != null)
            {
                viewModel.SelectedTable = selectedTable;
            }
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            MiniTableViewModel viewModel = this.DataContext as MiniTableViewModel;
            if (viewModel != null)
            {
                int currentTableId = viewModel.SelectedTable.tableId; // Assuming you have a SelectedTable property
                var allBills = new List<Bill>(); // Load existing bills if necessary
                if (File.Exists(billJsn))
                {
                    string existingBillData = File.ReadAllText(billJsn);
                    allBills = JsonConvert.DeserializeObject<List<Bill>>(existingBillData) ?? new List<Bill>();
                }

                // Remove old bills for the current table
                allBills.RemoveAll(b => b.tableId == currentTableId);

                // Add the current bills for the table
                allBills.AddRange(viewModel.BillItems);

                // Save all bills back to the file
                string billData = JsonConvert.SerializeObject(allBills);
                File.WriteAllText(billJsn, billData);
            }
           
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            MiniTableViewModel viewModel = this.DataContext as MiniTableViewModel;
            if (viewModel != null && viewModel.SelectedTable != null)
            {
                int currentTableId = viewModel.SelectedTable.tableId; // Assuming you have a SelectedTable property
                string billData = File.ReadAllText(billJsn);
                var allBills = JsonConvert.DeserializeObject<List<Bill>>(billData) ?? new List<Bill>();
                var tableBills = allBills.Where(b => b.tableId == currentTableId);
                viewModel.BillItems = new ObservableCollection<Bill>(tableBills);
            }

            // Execute the CalculateSum command
            if (viewModel != null && viewModel.CalculateSumCommand.CanExecute(null))
            {
                viewModel.CalculateSumCommand.Execute(null);
            }
        }

       
    }
}
