using System;
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
using DAL.Models;
using MahApps.Metro.Controls.Dialogs;
using Services;
using Services.ServicesClasses;
using Line = DAL.Models.Line;

namespace ElectricityBills.Pages
{
    /// <summary>
    /// Interaction logic for PageLine.xaml
    /// </summary>
    public partial class PageLine
    {
        private readonly LineServices _line;
        public PageLine()
        {
            InitializeComponent();
            _line = new LineServices();
        }

        private async void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await PopulateDgv();
        }

        private async Task PopulateDgv()
        {
            using (_line)
            {
                await _line.PopulateDataGrid(LineDataGrid);
            }
        }

        private void NumericInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != ".")
                e.Handled = true;
        }

        private void BtnNew_OnClick(object sender, RoutedEventArgs e)
        {
            StackItem.DataContext = new Line();
        }

        private async void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            if(!(StackItem.DataContext is Line item))return;

            using (_line)
            {
                var checkedItem = await _line.Line.AnyAsync(x => x.Id == item.Id);

                if (!checkedItem)
                {
                    await _line.Line.AddAsync(item);
                    await _line.Line.SaveAsync();
                }
                else
                {
                    await _line.Line.UpdateAsync(item , item.Id);
                    await _line.Line.SaveAsync();
                }

                System.Media.SystemSounds.Exclamation.Play();
                await PopulateDgv();
            }
        }

        private async void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(StackItem.DataContext is Line item)) return;

            var massage = await BasicClass.ShowBasicMessage(this, "تنبيه", "هل ترغب في حذف الاشتراك ؟ ",
                MessageDialogStyle.AffirmativeAndNegative);
            if (massage != MessageDialogResult.Affirmative) return;

            using (_line)
            {
                _line.Line.Delete(item);
                await _line.Line.SaveAsync();
            }

            StackItem.DataContext = new Customer();

            await PopulateDgv();
        }

        private void LineDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(LineDataGrid.SelectedItem is Line item)) return;

            StackItem.DataContext = item;
        }
    }
}
