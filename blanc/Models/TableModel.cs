using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace blanc.Models
{
    public class TableModel
    {
        public int tableId { get; set; }
        public int seats { get; set; }
        public string[]? orderedItems{ get; set; }
        public float[]? bill {  get; set; }

        public int TableId { get; set; }
        public int NumberOfSeats { get; set; }
        public ObservableCollection<MenuItem> MenuItems { get; set; }
        public ObservableCollection<BillItem> Bill { get; set; }

        public TableModel()
        {
            MenuItems = new ObservableCollection<MenuItem>();
            Bill = new ObservableCollection<BillItem>();
        }
    }

    
    public class BillItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        // Other relevant properties
    }
}
