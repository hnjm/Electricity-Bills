using Services.ServicesClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using DAL.Models;
using ToastNotifications.Messages;

namespace ElectricityBills.Pages
{
    /// <summary>
    /// Interaction logic for PageCounterRead.xaml
    /// </summary>
    public partial class PageCounterRead
    {
        private readonly CounterReadServices _counterReadServices;
        private readonly CustomerService _customerService;

        public PageCounterRead()
        {
            InitializeComponent();
            _counterReadServices = new CounterReadServices();
            _customerService = new CustomerService();
        }

        private async Task PopulateDataGrid()
        {
            using(_counterReadServices)
            {
                await _counterReadServices.PopulateRegisterDataGrid(CounterReadsDataGrid);
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await PopulateDataGrid();
        }

        private async void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (CounterReadsDataGrid.Items.Count <=0) return;

            var list = new List<CounterReads>();

            foreach (var item in CounterReadsDataGrid.Items )
            {
                var counter = item as CounterReads;

                //using (_counterReadServices)
                //{
                //    var lastrec = _counterReadServices.GetLastCustomerCounterRead((int)counter.CustomerId, null);

                //    if (!(counter?.TheRead <= lastrec)) return;
                //    var name = counter.Customer.CustomerName;
                //    BasicClass.Notifier.ShowError("تحقق من قراءة عداد الزبون" + counter.Customer.CustomerName);
                //}

                if (counter?.TheRead!= null)
                {
                    counter.DateOfRead = DateOfRead.SelectedDate;
                    list.Add(counter);
                }
            }

            using (_counterReadServices)
            {
                await _counterReadServices.CounterReadsRepository.AddRangAsync(list);
                await _counterReadServices.CounterReadsRepository.SaveAsync();
                await PopulateDataGrid();
            }
        }

        private void TxtRead_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != ".") e.Handled = true;
        }

        private void TxtRead_OnLostFocus(object sender, RoutedEventArgs e)
        {
            var item = CounterReadsDataGrid.CurrentItem as CounterReads;

            if(item?.TheRead == null) return;

            using (_counterReadServices)
            {
                var lastrec = _counterReadServices.GetLastCustomerCounterRead((int)item.CustomerId, null);

                if (!(item?.TheRead <= lastrec)) return;
                BasicClass.Notifier.ShowError("تحقق من قراءة العداد");
                return;
            }
        }

        private void TxtRead_OnGotFocus(object sender, RoutedEventArgs e)
        {
            var item = CounterReadsDataGrid.SelectedItem as CounterReads;

            if (item?.TheRead == null) return;

            using (_counterReadServices)
            {
                var lastrec = _counterReadServices.GetLastCustomerCounterRead((int)item.CustomerId, null);

                if (!(item?.TheRead <= lastrec)) return;
                BasicClass.Notifier.ShowError("تحقق من قراءة العداد");
                return;
            }
        }
    }
}
