using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using Services.ServicesClasses;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DAL.Models;
using ToastNotifications.Messages;

namespace ElectricityBills.Pages
{
    /// <summary>
    /// Interaction logic for PagePayment.xaml
    /// </summary>
    public partial class PagePayment
    {
        private CustomerService _customerService;
        private PaymentServices _paymentServices;
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
                PopulateGataGrid()
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

        private async Task PopulateGataGrid()
        {
            using (_paymentServices)
            {
                await _paymentServices.PopulateDataGrid(PaymentDataGrid, TxtCustomerSearch.Text, null, null);
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

        private void BtnDelet_OnClick(object sender, RoutedEventArgs e)
        {

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

                    await _paymentServices.PaymentRepository.AddAsync(item);
                    await _paymentServices.PaymentRepository.SaveAsync();
                }
                else
                {
                    await _paymentServices.PaymentRepository.UpdateAsync(item, item.Id);
                    await _paymentServices.PaymentRepository.SaveAsync();
                }

                SystemSounds.Exclamation.Play();
                await PopulateGataGrid();
            }
        }

        private void BtnNew_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void TxtCustomerSearch_OnTextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
}
