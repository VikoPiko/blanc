using blanc.Models;
using blanc.Views;
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

namespace blanc.ViewModels
{
    public class TablesViewModel : ViewModelBase
    {
        const string tables = "D:\\vikoEdit\\blanc\\blanc\\jsonFiles\\Tables.json";

        private ObservableCollection<TableModel>? _tables;
        private TableModel? _selectedTable;

        public ObservableCollection<TableModel>? Tables 
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
            AddTableCommand = new RelayCommand(AddTable);
            AddBillCommand = new RelayCommand(AddBill);
            PayBillCommand = new RelayCommand(PayBill);
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

        public TableModel? SelectedTable
        {
            get { return _selectedTable; }
            set
            {
                _selectedTable = value;
                OnPropertyChanged(nameof(SelectedTable));
                CommandManager.InvalidateRequerySuggested();
            }
        }
        private void AddBill()
        {
            throw new NotImplementedException();
        }
        private void PayBill()
        {
            throw new NotImplementedException();
        }

    }
}
