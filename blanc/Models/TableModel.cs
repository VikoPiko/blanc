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

        public double BillTotal { get; set; }


    }


}
