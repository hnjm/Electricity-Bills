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
using MahApps.Metro.Controls.Dialogs;
using Services;
using Services.ServicesClasses;

namespace ElectricityBills.Pages
{
    /// <summary>
    /// Interaction logic for PageLogin.xaml
    /// </summary>
    public partial class PageLogin
    {
        private readonly LoginServices _loginServices;
        public PageLogin()
        {
            InitializeComponent();
            _loginServices = new LoginServices();
        }

        private async void BtnLogin_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                using (_loginServices)
                {
                    var usr = await _loginServices.CheckUser(NameTextBox.Text, FloatingPasswordBox.Password);

                    if (!usr)
                    {
                        await BasicClass.ShowBasicMessage(this, "تنبيه", "الرجاء التأكد من صحة إسم المستخدم وكلمة المرور", MessageDialogStyle.Affirmative);
                        return;
                    }
                    else
                    {
                        this.Hide();
                        //App.User = usr;
                        BasicClass.OpenWindow(new MainWindow());
                    }
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

                MessageBox.Show(exception.InnerException?.Message);
            }
        }
    }
}
