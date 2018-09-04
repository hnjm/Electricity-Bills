using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using ViewModel;

namespace Services.ServicesClasses
{
    public class LineServices : BaseNotifyPropertyChanged
    {
        public readonly IGenericRepository<Line> Line;
        public readonly IGenericRepository<Customer> Customer;

        public LineServices()
        {
            Line = new GenericRepository<Line>(new ElectricityBillsContext());
            Customer = new GenericRepository<Customer>(new ElectricityBillsContext());
        }

        public async Task PopulateDataGrid(DataGrid dgv)
        {
            var result = Line.GetAllIncluding(x => x.Customer);

            dgv.ItemsSource = await result.Select(x => new VMLine()
            {
                Id = x.Id,
                LineName = x.LineName,
                UnitPrice = x.UnitPrice,
                CustomerCount = GetLineCustomerCount(x.Id),
            }).ToListAsync();


        }

        public int GetLineCustomerCount(int id)
        {
            return Customer.GetAll(x => x.LineId == id).Count();
        }

    }
}