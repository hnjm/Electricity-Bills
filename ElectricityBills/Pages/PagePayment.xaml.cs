using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using DAL.Models;
using MahApps.Metro.Controls.Dialogs;
using Services.ServicesClasses;
using ToastNotifications.Messages;
using ViewModel;

namespace ElectricityBills.Pages
{
    /// <summary>
    /// Interaction logic for PagePayment.xaml
    /// </summary>
    public partial class PagePayment
    {
        private readonly CustomerService _customerService;
        private readonly PaymentServices _paymentServices;

        public PagePayment()
        {
            InitializeComponent();
            _customerService = new CustomerService();
            _paymentServices = new PaymentServices();
        }
        private async void PagePayment_OnLoaded(object sender, RoutedEventArgs e)
        {
            var tasks = new List<Task>()
            {
                PopulateCustomerComboBox(),
                PopulateDataGrid()
            };

            await Task.WhenAll(tasks);
        }
        private async Task PopulateCustomerComboBox()
        {
            using (_customerService)
            {
                ComboBoxCustomer.ItemsSource = await _customerService.CustomerRepository.GetAllAsync();
            }
        }

        private async Task PopulateDataGrid()
        {
            var dateFrom = !DateFrom.IsEnabled
                ? null
                : DateFrom.SelectedDate;

            var dateTo = !DateTo.IsEnabled
                ? null
                : DateTo.SelectedDate;
            using (_paymentServices)
            {
                await _paymentServices.PopulateDataGrid(PaymentDataGrid, TxtCustomerSearch.Text, dateFrom, dateTo);
            }
        }
        private void CustomerDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(PaymentDataGrid.SelectedItem is Payment item)) return;

            StackItems.DataContext = item;

        }

        private void NumericInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != ".") e.Handled = true;
        }

        private async void BtnCustomerPage_OnClick(object sender, RoutedEventArgs e)
        {
            await this.ShowOverlayAsync();
            new PageCustomer().ShowDialog();
            await PopulateCustomerComboBox();
            await HideOverlayAsync();
        }

        private async void BtnDelet_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(StackItems.DataContext is Payment item)) return;

            var massage = await BasicClass.ShowBasicMessage(this,
                "تنبيه",
                "هل ترغب في حذف الاشتراك ؟ ",
                MessageDialogStyle.AffirmativeAndNegative);
            if (massage != MessageDialogResult.Affirmative) return;

            using (_paymentServices)
            {
                _paymentServices.PaymentRepository.Delete(item);
                await _paymentServices.PaymentRepository.SaveAsync();
            }

            StackItems.DataContext = new Payment();
            BtnNew_OnClick(sender,e);
            await PopulateDataGrid();
        }

        private async void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(StackItems.DataContext is Payment item)) return;

            using (_paymentServices)
            {
                var checkedItem = await _paymentServices.PaymentRepository
                    .AnyAsync(x => x.Id == item.Id);

                if (!checkedItem)
                {
                    var checkName = await _paymentServices.PaymentRepository
                        .AnyAsync(x => x.CustomerId == item.CustomerId && x.DateOfPay == item.DateOfPay);


                    if (checkName)
                    {
                        BasicClass.Notifier.ShowError("هذا المشترك تم إضافة دفعة له بنفس التاريخ مسبقاً");
                        return;
                    }

                    var checkedSanad = await _paymentServices.CheckSanad(item.Sanad);

                    if (checkedSanad)
                    {
                        var massage = await BasicClass.ShowBasicMessage(this,
                            "تنبيه",
                            "رقم السند مسجل مسبقاً ... هل ترغب بالاستمرار ؟ ",
                            MessageDialogStyle.AffirmativeAndNegative);
                        if (massage != MessageDialogResult.Affirmative) return;
                    }

                    await _paymentServices.PaymentRepository.AddAsync(item);
                    await _paymentServices.PaymentRepository.SaveAsync();
                }
                else
                {
                    await _paymentServices.PaymentRepository.UpdateAsync(item, item.Id);
                    await _paymentServices.PaymentRepository.SaveAsync();
                }

                SystemSounds.Exclamation.Play();
                BtnNew_OnClick(sender , e);
                await PopulateDataGrid();
            }
        }

        private void BtnNew_OnClick(object sender, RoutedEventArgs e)
        {
            StackItems.DataContext = new VMPayment();
        }

        private async void TxtCustomerSearch_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            using (_paymentServices)
            {
                await PopulateDataGrid();
            }
        }

        private async void CheckedAndUnChecked(object sender, RoutedEventArgs e)
        {
            if (DateFrom.SelectedDate == null && DateTo.SelectedDate == null) return;

            var checkDates = BasicClass.CheckDates(DateFrom.SelectedDate, DateTo.SelectedDate);

            if (checkDates)
            {
                BasicClass.Notifier.ShowInformation("الرجاء التأكد من صحة التواريخ المدخلة");
                return;
            }

            await PopulateDataGrid();
        }

        private async void ToggleButton_OnClick(object sender, RoutedEventArgs e)
        {
            await PopulateDataGrid();
        }
    }
}