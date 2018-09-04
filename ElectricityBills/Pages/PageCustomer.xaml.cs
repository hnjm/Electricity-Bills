using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Services;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace ElectricityBills.Pages
{
    /// <summary>
    /// Interaction logic for PageCustomer.xaml
    /// </summary>
    public partial class PageCustomer
    {
        private readonly LineServices _lineServices;

        public PageCustomer()
        {
            InitializeComponent();
            _lineServices = new LineServices();
        }

        private async void PageCustomer_OnLoaded(object sender, RoutedEventArgs e)
        {
           await PopulateLineComboBox();
        }

        public async Task PopulateLineComboBox()
        {
            ComboBoxLine.ItemsSource = await _lineServices.Line.GetAllAsync();
        }

        private void BtnSaveBalance_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelet_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void TxtCustomerSearch_OnTextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void BtnNew_OnClick(object sender, RoutedEventArgs e)
        {
           //BasicClass.Notifier.ShowSuccess("خطsdfffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffر");
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void CustomerDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void BtnOpeningBalance_OnClick(object sender, RoutedEventArgs e)
        {
        }

    }
}
