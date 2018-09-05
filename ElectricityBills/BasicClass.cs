using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;


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


        public static Notifier Notifier { get;} = new Notifier(cfg =>
        {
            cfg.DisplayOptions.Width = 500;
            


            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.BottomLeft,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });


        //await metroWidow.ShowMessageAsync("حذف", "هل ترغب بتاكيد عملية الحذف ؟",
        //MessageDialogStyle.AffirmativeAndNegative, ClsGeneral.Settings);


    }
}