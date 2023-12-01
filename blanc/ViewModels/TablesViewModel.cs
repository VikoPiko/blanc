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
        const string tables = "Tables.json";

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
        public ICommand AddMenuItemCommand {  get; private set; }
        public ICommand RemoveMenuItemCommand {  get; private set; }
        public ICommand PayBillCommand {  get; private set; }
        public ICommand SplitTableCommand {  get; private set; }
        public ICommand JoinTableCommand {  get; private set; }
        public TablesViewModel() 
        {
            AddTableCommand = new RelayCommand(AddTable);
            AddBillCommand = new RelayCommand(AddBill);
            PayBillCommand = new RelayCommand(PayBill);

            string rawJson = File.ReadAllText(tables);
            List<TableModel>? reservations = JsonConvert.DeserializeObject<List<TableModel>>(rawJson);


        }

        private void AddTable()
        {
            throw new NotImplementedException();
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
