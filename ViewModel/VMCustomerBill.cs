using DAL.Models;

namespace ViewModel
{
    public class VMCustomerBill : CustomerBills
    {
        public bool IsSelected { get; set; }
        public string CustomerName { get; set; }
        public string LineName { get; set; }

    }
}
