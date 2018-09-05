using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Customer : BaseNotifyPropertyChanged
    {
        public Customer()
        {
            //CounterReads = new HashSet<CounterReads>();
            CustomerBills = new HashSet<CustomerBills>();
            Payment = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public int? LineId { get; set; }
        public int? MinimumAmount { get; set; }
        public int? CounterNumber { get; set; }
        public decimal? LastBalance { get; set; }
        public bool? CustomerStatue { get; set; }

        public virtual Line Line { get; set; }
        public virtual ICollection<CounterReads> CounterReads { get; set; }
        public virtual ICollection<CustomerBills> CustomerBills { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
    }
}
