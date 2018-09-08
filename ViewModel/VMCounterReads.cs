using System.ComponentModel;
using DAL.Models;

namespace ViewModel
{
    public class VMCounterReads : CounterReads 
    {
        public string CustomerName { get; set; }
        public long? LastRead { get; set; }


        private long? _amount;
        public virtual long? Amount
        {
            get => TheRead - LastRead;

            set
            {
                _amount = value;
                 OnPropertyChanged("Amount");
            }
        }
    }
}