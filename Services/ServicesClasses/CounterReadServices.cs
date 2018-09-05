using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using ViewModel;

namespace Services.ServicesClasses
{
    public class CounterReadServices : BaseNotifyPropertyChanged
    {
        public readonly IGenericRepository<CounterReads> CounterReadsRepository;
        private readonly IGenericRepository<Customer> _customerRepository;
        public CounterReadServices()
        {
            CounterReadsRepository = new GenericRepository<CounterReads>(new ElectricityBillsContext());
            _customerRepository = new GenericRepository<Customer>(new ElectricityBillsContext());
        }

        public long? GetLastCustomerCounterRead(int customerId, DateTime? dateOfRead)
        {
            var result = CounterReadsRepository
                .GetAll(x => x.CustomerId == customerId, order => order.OrderBy(x => x.DateOfRead) );
                

            if (dateOfRead != null)
            {
                result = result.Where(x => x.DateOfRead < dateOfRead);
            }

            var read = result.LastOrDefault()?.TheRead;

            return read ?? 0;
        }

        public async Task PopulateRegisterDataGrid(DataGrid dgv)
        {
            var list = await _customerRepository
                .GetAll(x => x.CustomerStatue == true)
                .Select(x => new VMCounterReads()
                {
                    CustomerId = x.Id,
                    CustomerName = x.CustomerName,
                }).ToListAsync();

            dgv.ItemsSource = list;

        }

    }
}