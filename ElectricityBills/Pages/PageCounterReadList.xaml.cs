﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Services.ServicesClasses;

namespace ElectricityBills.Pages
{
    /// <summary>
    /// Interaction logic for PageCounterReadList.xaml
    /// </summary>
    public partial class PageCounterReadList
    {
        private readonly CounterReadServices _counterReadServices;
        public PageCounterReadList()
        {
            InitializeComponent();
            _counterReadServices = new CounterReadServices();
        }

        private async void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await PopulateDataGrid();
        }

        private async Task PopulateDataGrid()
        {
            using (_counterReadServices)
            {
                await _counterReadServices.PopulateReadsListDataGrid(CounterReadsDataGrid);
            }
        }
    }
}