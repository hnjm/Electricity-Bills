using DAL.Models;
using Repository;

namespace Services.ServicesClasses
{
    public class CustomerBillServices : BaseNotifyPropertyChanged
    {
        public IGenericRepository<CustomerBills> CustomerBillsRepository;
        public CustomerBillServices()
        {
            CustomerBillsRepository = new GenericRepository<CustomerBills>(new ElectricityBillsContext());
        }

    }
}