using System;

namespace DAL.Models
{
    public partial class Payment : BaseNotifyPropertyChanged
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? DateOfPay { get; set; }
        public decimal? PaymentAmount { get; set; }
        public int? Sanad { get; set; }
        public string Note { get; set; }

        public  Customer Customer { get; set; }
    }
}
