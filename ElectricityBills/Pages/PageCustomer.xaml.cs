using System.Collections.Generic;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DAL.Models;
using MahApps.Metro.Controls.Dialogs;
using Services.ServicesClasses;
using ToastNotifications.Messages;

namespace ElectricityBills.Pages
{
    /// <summary>
    ///     Interaction logic for PageCustomer.xaml
    /// </summary>
    public partial class PageCustomer
    {
        private readonly CustomerService _customerServices;
        private readonly LineServices _lineServices;


        public PageCustomer()
        {
            InitializeComponent();
            _lineServices = new LineServices();
            _customerServices = new CustomerService();
        }

        private async void BtnDelet_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(StackItems.DataContext is Customer item)) return;

            var massage = await BasicClass.ShowBasicMessage(this,
                "تنبيه",
                "هل ترغب في حذف الاشتراك ؟ ",
                MessageDialogStyle.AffirmativeAndNegative);
            if (massage != MessageDialogResult.Affirmative) return;

            using (_customerServices)
            {
                _customerServices.CustomerRepository.Delete(item);
                await _customerServices.CustomerRepository.SaveAsync();
            }

            StackItems.DataContext = new Customer();

            await PopulateDatGrid();
        }

        private void BtnNew_OnClick(object sender, RoutedEventArgs e)
        {
            StackItems.DataContext = new Customer();
        }

        private async void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(StackItems.DataContext is Customer item)) return;

            using (_customerServices)
            {
                var checkedItem = await _customerServices.CustomerRepository.AnyAsync(x => x.Id == item.Id);

                if (!checkedItem)
                {
                    var checkName = await _customerServices.CustomerRepository.AnyAsync(x => x.CustomerName ==
                                                                                             TxtName.Text);
                    if (checkName)
                    {
                        BasicClass.Notifier.ShowError("هذا المشترك تم إضافته مسبقاً");
                        return;
                    }

                    await _customerServices.CustomerRepository.AddAsync(item);
                    await _customerServices.CustomerRepository.SaveAsync();
                }
                else
                {
                    await _customerServices.CustomerRepository.UpdateAsync(item, item.Id);
                    await _customerServices.CustomerRepository.SaveAsync();
                }

                SystemSounds.Exclamation.Play();
                await PopulateDatGrid();
            }
        }

        private void CustomerDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(CustomerDataGrid.SelectedItem is Customer item)) return;

            StackItems.DataContext = item;
        }

        private void NumericInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != ".") e.Handled = true;
        }

        private async void PageCustomer_OnLoaded(object sender, RoutedEventArgs e)
        {
            var tasks = new List<Task>
            {
                PopulateLineComboBox(),
                PopulateDatGrid()
            };

            await Task.WhenAll(tasks);
        }

        private async void TxtCustomerSearch_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            await PopulateDatGrid();
        }

        public async Task PopulateDatGrid()
        {
            await _customerServices.PopulateDataGrid(CustomerDataGrid,
                TxtCustomerSearch.Text);
        }

        public async Task PopulateLineComboBox()
        {
            ComboBoxLine.ItemsSource = await _lineServices.Line.GetAllAsync();
        }

        private async void BtnLinePage_OnClick(object sender, RoutedEventArgs e)
        {
            var frm = new PageLine();
            frm.ShowDialog();
            await PopulateLineComboBox();
        }
    }
}