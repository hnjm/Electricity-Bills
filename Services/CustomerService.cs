using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Services
{
    public class CustomerService : BaseNotifyPropertyChanged
    {
        private readonly IGenericRepository<Customer> _customer;

        public CustomerService()
        {
            _customer = new GenericRepository<Customer>(new ElectricityBillsContext());
        }

        public async Task<bool> CheckCustomerIfFound(string customerName)
        {
            return await _customer.GetAll().AnyAsync(x => x.CustomerName.Equals(customerName));
        }
    }
}