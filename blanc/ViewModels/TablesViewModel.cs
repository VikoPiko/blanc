using blanc.Models;
using blanc.ViewModels.Commands;
using blanc.Views;
using FontAwesome.Sharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Menu = blanc.Models.Menu;

namespace blanc.ViewModels
{
    public class TablesViewModel : ViewModelBase
    {
        const string tablesJsn = "Tables.json";
        const string menuJsn = "Menu.json";
        const string billJsn = "Bill.json";



        private ObservableCollection<TableModel> _tables = new ObservableCollection<TableModel>();


       
        public ObservableCollection<TableModel> Tables 
        {
            get { return _tables; }
            set
            {
                _tables = value;
                OnPropertyChanged(nameof(Tables));
            }
        }


       private TableModel _selectedTable;
       public TableModel SelectedTable
        {
            get { return _selectedTable; }
            set
            {
                _selectedTable = value;
                OnPropertyChanged(nameof(SelectedTable));
                
            }
        }

        private ObservableCollection<Bill>? _billItems;
        public ObservableCollection<Bill> BillItems
        {
            get => _billItems;
            set { _billItems = value; OnPropertyChanged(nameof(BillItems)); }
        }

        private double _sum;
        public double Sum
        {
            get => _sum;
            set
            {
                _sum = value;
                OnPropertyChanged(nameof(Sum));
            }
        }


        public int seats {  get; set; }
        public string[]? OrderedItems { get; set; }
        public float Bill {  get; set; }
 
        
        public ICommand AddTableCommand {  get; private set; }
        public ICommand RemoveTableCommand {  get; private set; }
        public ICommand AddBillCommand {  get; private set; }
        public ICommand OpenMiniTablesCommand { get; private set;  }
        
      
        public ICommand PayBillCommand {  get; private set; }
 
        public TablesViewModel() 
        {
           
             AddTableCommand = new RelayCommand(AddTable);
            OpenMiniTablesCommand = new RelayCommand(OpenMiniTables);
            RemoveTableCommand = new RelayCommand(RemoveTable, CanRemoveTable);
           
            Tables = new ObservableCollection<TableModel>();

          
            

            string rawJson = File.ReadAllText(tablesJsn);
            List<TableModel>? table = JsonConvert.DeserializeObject<List<TableModel>>(rawJson);

            if (table != null)
            {
                foreach (var item in table)
                {
                    Tables.Add(item);
                }
            }

        }

        private double _sumForDataGrid;
        public double SumForDataGrid
        {
            get => _sumForDataGrid;
            set
            {
                _sumForDataGrid = value;
                OnPropertyChanged(nameof(SumForDataGrid));
            }
        }

       

        private Dictionary<int, MiniTable> openTables = new Dictionary<int, MiniTable>();
        private void OpenMiniTables()
        {
            if (this.SelectedTable != null)
            {
                // Зареждате всички маси от JSON файла
                string json = File.ReadAllText(tablesJsn);
                List<TableModel> tables = JsonConvert.DeserializeObject<List<TableModel>>(json);
             
                // Ако съответната маса е намерена
                if (SelectedTable != null && !openTables.ContainsKey(SelectedTable.tableId))
                {
                    // Create the window and add it to the dictionary
                    var miniTableView = new MiniTable(SelectedTable);
                    miniTableView.Closed += (sender, args) => openTables.Remove(SelectedTable.tableId);
                    openTables[SelectedTable.tableId] = miniTableView;
                    miniTableView.Show();
                }
            }
         }

        

        public void AddTable()
        {

            var newTable = new TableModel
            {
                tableId = Tables.Count + 1,
                seats = seats
            };
            Tables.Add(newTable);

            List<TableModel> table = new List<TableModel>();

            foreach (TableModel item in Tables)
            {
                table.Add(new TableModel()
                {
                    tableId = item.tableId,
                    seats = item.seats
                });
            }
            string json = JsonConvert.SerializeObject(table, Formatting.Indented);


            File.WriteAllText(tablesJsn, json);


        }
        private bool CanRemoveTable()
        {
            return _selectedTable != null;
        }

        private void RemoveTable()
        {
            if (_selectedTable != null)
            {
                Tables.Remove(_selectedTable);

                string json = JsonConvert.SerializeObject(Tables, Formatting.Indented);
                File.WriteAllText(tablesJsn, json);
            }
        }





        private List<Menu> _menuItems;
        public List<Menu> MenuItems
        {
            get { return _menuItems; }
            set
            {
                _menuItems = value;
                OnPropertyChanged(nameof(MenuItems));
            }
        }




      

    }

     

}
