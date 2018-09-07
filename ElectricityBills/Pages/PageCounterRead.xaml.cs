using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DAL.Models;
using Services.ServicesClasses;
using ToastNotifications.Messages;

namespace ElectricityBills.Pages
{
    /// <summary>
    ///     Interaction logic for PageCounterRead.xaml
    /// </summary>
    public partial class PageCounterRead
    {
        private CounterReadServices _counterReadServices;
        private readonly CustomerService _customerService;

        public PageCounterRead()
        {
            InitializeComponent();
            _counterReadServices = new CounterReadServices();
            _customerService = new CustomerService();
        }

        private async Task PopulateDataGrid()
        {
            using (_counterReadServices)
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
            if (CounterReadsDataGrid.Items.Count <= 0) return;

            var list = new List<CounterReads>();

            foreach (var item in CounterReadsDataGrid.Items)
            {
                var counter = item as CounterReads;

                if (counter?.TheRead != null)
                {
                    counter.DateOfRead = DateOfRead.SelectedDate;
                    list.Add(counter);
                }
            }

            if (string.IsNullOrEmpty(DateOfRead.Text))
            {
                BasicClass.Notifier.ShowInformation("يرجى تحديد تاريخ القراءة");
                return;
            }

            using (_counterReadServices = new CounterReadServices())
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

        private  void TxtRead_OnLostFocus(object sender, RoutedEventArgs e)
        {
            var item = CounterReadsDataGrid.CurrentItem as CounterReads;

            if (item?.TheRead == null) return;

            using (_counterReadServices)
            {
                var lastrec = _counterReadServices.GetLastCustomerCounterRead((int) item.CustomerId, null);

                if (!(item?.TheRead <= lastrec)) return;

                BasicClass.Notifier.ShowError("تحقق من قراءة العداد");
            }
        }
    }
}