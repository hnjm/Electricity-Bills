﻿using System;

namespace DAL.Models
{
    public partial class CustomerBills : BaseNotifyPropertyChanged
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? LineId { get; set; }
        public DateTime? DateOfLastRead { get; set; }
        public string PeriodOfBill { get; set; }
        public int? MinimunAmount { get; set; }
        public long? LastRead { get; set; }
        public long? CurrentRead { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? MaintenanceFees { get; set; }
        public decimal? ServicesFees { get; set; }
        public decimal? LastBalance { get; set; }
        public decimal? Discount { get; set; }
        public decimal? BillAmount { get; set; }
        public string Notes { get; set; }

        public Customer Customer { get; set; }
        public Line Line { get; set; }
    }
}