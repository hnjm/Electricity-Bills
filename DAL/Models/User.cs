﻿using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class User : BaseNotifyPropertyChanged
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}
