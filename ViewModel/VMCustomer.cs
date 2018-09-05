using DAL.Models;

namespace ViewModel
{
    public class VMCustomer : Customer
    {
        public string LineName { get; set; }
        public long? LastRead { get; set; }
    }
}