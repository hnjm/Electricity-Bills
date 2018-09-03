using System;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ElectricityBills
{
    public static class BasicClass
    {
        internal static MetroDialogSettings Settings = new MetroDialogSettings
        {
            AffirmativeButtonText = "موافق",
            NegativeButtonText = "لا",
            AnimateHide = true,
            AnimateShow = true,
            ColorScheme = MetroDialogColorScheme.Accented
        };

        public static async Task<MessageDialogResult> ShowBasicMessage(MetroWindow window, string title, string caption, MessageDialogStyle messageDialogStyle)
        {
            return await window.ShowMessageAsync(title, caption, messageDialogStyle, Settings);
        }

        public static bool CheckDates(DateTime? datFrom, DateTime? dateTo)
        {
            return datFrom > dateTo;
        }

        public static bool CheckTime(DateTime? timeFrom, DateTime? timeTo)
        {
            return timeFrom?.TimeOfDay > timeTo?.TimeOfDay;
        }

        public static void OpenWindow(Window win)
        {
            win?.ShowDialog();
        }
        //await metroWidow.ShowMessageAsync("حذف", "هل ترغب بتاكيد عملية الحذف ؟",
        //MessageDialogStyle.AffirmativeAndNegative, ClsGeneral.Settings);


    }
}