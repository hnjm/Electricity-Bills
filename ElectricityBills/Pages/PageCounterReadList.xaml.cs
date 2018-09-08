using DAL.Models;
using Services.ServicesClasses;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ElectricityBills.Pages
{
    /// <summary>
    /// Interaction logic for PageCounterReadList.xaml
    /// </summary>
    public partial class PageCounterReadList
    {
        private CounterReadServices _counterReadServices;
        public PageCounterReadList()
        {
            InitializeComponent();
            _counterReadServices = new CounterReadServices();
        }

        private async void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //PBar.Visibility = Visibility.Visible;
            await this.ShowOverlayAsync();
            await PopulateListDataGrid();
            await this.HideOverlayAsync();
            //PBar.Visibility = Visibility.Collapsed;
        }

        private async Task PopulateListDataGrid()
        {
            var dateFrom = !DateFrom.IsEnabled
                ? null
                : DateFrom.SelectedDate;

            var dateTo = !DateTo.IsEnabled
                ? null
                : DateTo.SelectedDate;

            using (_counterReadServices = new CounterReadServices())
            {
                await _counterReadServices.PopulateReadsListDataGrid(CounterReadsDataGrid, TxtCustomerSearch.Text , dateFrom , dateTo);
            }
        }

        private async void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(CounterReadsDataGrid.SelectedItem is CounterReads item)) return;

            using (_counterReadServices = new CounterReadServices())
            {
                await _counterReadServices.CounterReadsRepository.UpdateAsync(item, item.Id);
                await _counterReadServices.CounterReadsRepository.SaveAsync();
            }
        }

        private async void TxtCustomerSearch_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            await PopulateListDataGrid();
        }

        private async void ToggleButton_OnClick(object sender, RoutedEventArgs e)
        {
            await PopulateListDataGrid();
        }
        private async Task FilterData()
        {
            if (DateFrom.SelectedDate == null && DateTo.SelectedDate == null) return;
            await PopulateListDataGrid();
        }

        private async void CheckedAndUnChecked(object sender, RoutedEventArgs e)
        {
            await FilterData();
        }
    }
}
