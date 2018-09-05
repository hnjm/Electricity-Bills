using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using ViewModel;

namespace Services.ServicesClasses
{
    public class CustomerService : BaseNotifyPropertyChanged
    {
        public readonly IGenericRepository<Customer> CustomerRepository;
        private readonly CounterReadServices _counterReadServices;

        public CustomerService()
        {
            CustomerRepository = new GenericRepository<Customer>(new ElectricityBillsContext());
            _counterReadServices = new CounterReadServices();
        }

        public async Task<bool> CheckCustomerIfFound(string customerName)
        {
            return await CustomerRepository.GetAll().AnyAsync(x => x.CustomerName.Equals(customerName));
        }

        public async Task PopulateDataGrid(DataGrid dgv, string customerName)
        {
            var result = CustomerRepository
                .GetAll(x => x.CustomerName.Contains(customerName))
                .Include(x => x.Line);
              
            dgv.ItemsSource = await result.Select(x => new VMCustomer()
            {
                Id = x.Id,
                LineId = x.LineId,
                CustomerName = x.CustomerName,
                CustomerMobile = x.CustomerMobile,
                CounterNumber = x.CounterNumber,
                LineName = x.Line.LineName,
                MinimumAmount = x.MinimumAmount,
                CustomerStatue = x.CustomerStatue,
                LastRead = _counterReadServices.GetLastCustomerCounterRead(x.Id , null),
                LastBalance = x.LastBalance
            }).ToListAsync();
        }

    }
}