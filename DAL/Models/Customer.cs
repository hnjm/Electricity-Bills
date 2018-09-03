using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Customer : BaseNotifyPropertyChanged
    {
        public Customer()
        {
            CounterReads = new HashSet<CounterReads>();
            CustomerBills = new HashSet<CustomerBills>();
        }

        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public int? LineId { get; set; }
        public int? CounterNumber { get; set; }
        public decimal? LastBalance { get; set; }
        public long? LastRead { get; set; }
        public bool? CustomerStatue { get; set; }

        public Line Line { get; set; }
        public ICollection<CounterReads> CounterReads { get; set; }
        public ICollection<CustomerBills> CustomerBills { get; set; }
    }
}
