using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private CounterReadServices _counterReadServices;
        public PageCounterReadList()
        {
            InitializeComponent();
            _counterReadServices = new CounterReadServices();
        }

        private async void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            PBar.Visibility = Visibility.Visible;
            await this.ShowOverlayAsync();
            await PopulateListDataGrid();
            await this.HideOverlayAsync();
            PBar.Visibility = Visibility.Collapsed;


        }

        private async Task PopulateListDataGrid()
        {
            //var timer = new Stopwatch();
            //timer.Start();
            using (_counterReadServices = new CounterReadServices())
            {
                await _counterReadServices.PopulateReadsListDataGrid(CounterReadsDataGrid);
            }
            //timer.Stop();
            //MessageBox.Show("Time Elapsed was :  "+ timer.Elapsed.Milliseconds.ToString()+"  MS"+"\n"+"Number Of Records is : "+CounterReadsDataGrid.Items.Count);
        }

        
    }
}
