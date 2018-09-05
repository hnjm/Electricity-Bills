using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Customer : BaseNotifyPropertyChanged
    {
        public Customer()
        {
            CounterReads = new HashSet<CounterReads>();
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
        public int? PaymentId { get; set; }

        public Line Line { get; set; }
        public ICollection<CounterReads> CounterReads { get; set; }
        public ICollection<CustomerBills> CustomerBills { get; set; }
        public ICollection<Payment> Payment { get; set; }
    }
}
