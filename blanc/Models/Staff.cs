using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blanc.Models
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float HoursWorked { get; set; }
        public float Wage {  get; set; }
        public string Position { get; set; }
    }
}
