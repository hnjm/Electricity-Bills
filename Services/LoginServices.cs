using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Repository;
namespace Services
{
    public class LoginServices : BaseNotifyPropertyChanged
    {
        private readonly IGenericRepository<User> _login;
        public LoginServices()
        {
            _login = new GenericRepository<User>(new ElectricityBillsContext());
        }

        public async Task<bool> CheckUser(string userName, string passWord)
        {
            return await _login.GetAll(x => x.UserName.Equals(userName) && x.PassWord.Equals(passWord)).AnyAsync();
        }
    }
}