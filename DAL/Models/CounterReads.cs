﻿using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class CounterReads : BaseNotifyPropertyChanged
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? DateOfRead { get; set; }
        public long? TheRead { get; set; }
        public string Note { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
