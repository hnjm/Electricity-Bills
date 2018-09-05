using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Line : BaseNotifyPropertyChanged
    {
        public Line()
        {
            Customer = new HashSet<Customer>();
            CustomerBills = new HashSet<CustomerBills>();
        }

        public int Id { get; set; }
        public string LineName { get; set; }
        public decimal? UnitPrice { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<CustomerBills> CustomerBills { get; set; }
    }
}
