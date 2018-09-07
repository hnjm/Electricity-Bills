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
    public class CounterReadServices : BaseNotifyPropertyChanged
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        public IGenericRepository<CounterReads> CounterReadsRepository;

        public CounterReadServices()
        {
            CounterReadsRepository = new GenericRepository<CounterReads>(new ElectricityBillsContext());
            _customerRepository = new GenericRepository<Customer>(new ElectricityBillsContext());
        }

        public long? GetLastCustomerCounterRead(int customerId, DateTime? dateOfRead)
        {
            CounterReadsRepository = new GenericRepository<CounterReads>(new ElectricityBillsContext());

            var result = CounterReadsRepository
                .GetAll(x => x.CustomerId == customerId, order => order.OrderBy(x => x.DateOfRead));


            if (dateOfRead != null) result = result.Where(x => x.DateOfRead < dateOfRead);

            var read = result.LastOrDefault()?.TheRead;

            return read ?? 0;
        }

        public async Task<long?> GetLastCustomerCounterReadAsync(int customerId, DateTime? dateOfRead)
        {
            var result = await CounterReadsRepository
                .GetAll(x => x.CustomerId == customerId, order => order.OrderBy(x => x.DateOfRead))
                .ToListAsync();


            if (dateOfRead != null) result = result.FindAll(x => x.DateOfRead < dateOfRead);

            var read = result.LastOrDefault()?.TheRead;

            return read ?? 0;
        }

        public async Task PopulateRegisterDataGrid(DataGrid dgv)
        {
            var list = await _customerRepository
                .GetAllAsync(x => x.CustomerStatue == true);

            var source = list
                .Select(x => new VMCounterReads
                {
                    CustomerId = x.Id,
                    CustomerName = x.CustomerName,
                    LastRead = GetLastCustomerCounterRead(x.Id, null)
                }).ToList();

            dgv.ItemsSource = source;
        }


        public async Task PopulateReadsListDataGrid(DataGrid dgv)
        {
            using (CounterReadsRepository = new GenericRepository<CounterReads>(new ElectricityBillsContext()))
            {
                var list = await
                    CounterReadsRepository
                        .GetAll(null, reads => reads.OrderBy(x => x.DateOfRead), reads => reads.Customer)
                        .Select(x =>
                            new VMCounterReads
                            {
                                Id = x.Id,
                                CustomerId = x.CustomerId,
                                DateOfRead = x.DateOfRead,
                                CustomerName = x.Customer.CustomerName,
                                TheRead = x.TheRead,
                                LastRead = GetLastCustomerCounterRead((int)x.CustomerId, x.DateOfRead),
                                Note = x.Note
                            }).ToListAsync();

                dgv.ItemsSource = list;

            }
        }
    }
}