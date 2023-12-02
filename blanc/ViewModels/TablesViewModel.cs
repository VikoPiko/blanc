using blanc.Models;
using blanc.Views;
using FontAwesome.Sharp;
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
using System.Windows.Documents;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Menu = blanc.Models.Menu;

namespace blanc.ViewModels
{
    public class TablesViewModel : ViewModelBase
    {
        const string tables = "C:\\BlankSystem\\blanc\\blanc\\jsonFiles\\Tables.json";


        private MiniTableViewModel miniTableViewModel;  

        private ObservableCollection<TableModel> _tables = new ObservableCollection<TableModel>();

        private TableModel _selectedTable;
        public ObservableCollection<TableModel> Tables 
        {
            get { return _tables; }
            set
            {
                _tables = value;
                OnPropertyChanged(nameof(Tables));
            }
        }

        public int seats {  get; set; }
        public string[]? OrderedItems { get; set; }
        public float Bill {  get; set; }

        public ICommand MiniTableCr { get; private set; }
        public ICommand AddTableCommand {  get; private set; }
        public ICommand RemoveTableCommand {  get; private set; }
        public ICommand AddBillCommand {  get; private set; }

        
      //  public ICommand AddMenuItemCommand {  get; private set; }
      //  public ICommand RemoveMenuItemCommand {  get; private set; }
        public ICommand PayBillCommand {  get; private set; }
       /* public ICommand SplitTableCommand {  get; private set; }
        public ICommand JoinTableCommand {  get; private set; }*/
        public TablesViewModel() 
        {
            MiniTableCr = new RelayCommand(ExecuteMiniTablesCommand);
            AddTableCommand = new RelayCommand(AddTable);
           /* AddBillCommand = new RelayCommand(AddBill);
            PayBillCommand = new RelayCommand(PayBill);*/
            RemoveTableCommand = new RelayCommand(RemoveTable, CanRemoveTable);

            Tables = new ObservableCollection<TableModel>();

            string rawJson = File.ReadAllText(tables);
            List<TableModel>? table = JsonConvert.DeserializeObject<List<TableModel>>(rawJson);

            if (table != null)
            {
                foreach (var item in table)
                {
                    Tables.Add(item);
                }
            }

        }
        private Dictionary<int, MiniTable> openTables = new Dictionary<int, MiniTable>();
        private void ExecuteMiniTablesCommand()
        {
            if (this.SelectedTable != null)
            {


                // Зареждате всички маси от JSON файла
                string json = File.ReadAllText("C:\\BlankSystem\\blanc\\blanc\\jsonFiles\\Tables.json");
                List<TableModel> tables = JsonConvert.DeserializeObject<List<TableModel>>(json);

                // Намерете масата, която отговаря на избраната маса по tableId
                TableModel tableToDisplay = tables.FirstOrDefault(t => t.tableId == this.SelectedTable.tableId);

               

                // Ако съответната маса е намерена
                if (tableToDisplay != null)
                {
                    string menuJson = File.ReadAllText("C:\\BlankSystem\\blanc\\blanc\\jsonFiles\\Menu.json");
                    List<Menu> menuItems = JsonConvert.DeserializeObject<List<Menu>>(menuJson);

                    // Проверете дали вече има отворен прозорец за тази маса
                    if (!openTables.ContainsKey(tableToDisplay.tableId))
                    {
                        // Създайте прозореца и го добавете към речника
                        var miniTableView = new MiniTable(tableToDisplay,menuItems);
                        miniTableView.Closed += (sender, args) => openTables.Remove(tableToDisplay.tableId);
                        openTables[tableToDisplay.tableId] = miniTableView;
                        miniTableView.Show();
                    }
                    else
                    {
                        // Фокусиране на вече отворения прозорец
                        openTables[tableToDisplay.tableId].Focus();
                    }
                }
            }
        }

        private void AddTable()
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


            File.WriteAllText(tables, json);


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
                File.WriteAllText(tables, json);
            }
        }

        public TableModel SelectedTable
        {
            get { return _selectedTable; }
            set
            {
                _selectedTable = value;
                OnPropertyChanged(nameof(SelectedTable));
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
       /* private void AddBill()
        {
            throw new NotImplementedException();
        }
        private void PayBill()
        {
            throw new NotImplementedException();
        }*/

       

}
