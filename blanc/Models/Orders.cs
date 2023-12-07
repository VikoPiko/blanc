using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blanc.Models
{
    public class Orders
    {
        public int OrderID {  get; set; }
        public int TableNumber { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public OrderStatus Status { get; set; }
    }
    public enum OrderStatus 
    {
        Ready = 1,
        Started = 2,
        Canceled = 3
    }
}
