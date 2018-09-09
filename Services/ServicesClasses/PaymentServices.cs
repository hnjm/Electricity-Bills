using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using ViewModel;

namespace Services.ServicesClasses
{
    public class PaymentServices : BaseNotifyPropertyChanged
    {
        public IGenericRepository<Payment> PaymentRepository;

        public PaymentServices() => PaymentRepository = new GenericRepository<Payment>(new ElectricityBillsContext());

        public async Task<bool> CheckSanad(int? sanad) => await PaymentRepository.AnyAsync(x => x.Sanad == sanad);

        public async Task PopulateDataGrid(DataGrid dgv, string customerName, DateTime? dateFrom, DateTime? dateTo)
        {
            var source = PaymentRepository
                .GetAll(x => x.Customer.CustomerName.Contains(customerName),
                        payments => payments.OrderBy(x => x.DateOfPay),
                        payment => payment.Customer)
            .Select(x => new VMPayment
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    CustomerName = x.Customer.CustomerName,
                    DateOfPay = x.DateOfPay,
                    PaymentAmount = x.PaymentAmount,
                    Sanad = x.Sanad,
                    Note = x.Note
                });

            if((dateFrom != null) && (dateTo != null))
            {
                source = source.Where(x => (x.DateOfPay >= dateFrom) && (x.DateOfPay <= dateTo));
            }

            var list = new ObservableCollection<VMPayment>(await source.ToListAsync());

            dgv.ItemsSource = list;
        }
    }
}