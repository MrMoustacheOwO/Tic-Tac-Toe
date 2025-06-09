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

namespace Sample.Views
{
    /// <summary>
    /// Логика взаимодействия для AutorisationWindow.xaml
    /// </summary>
    public partial class AutorisationWindow : Window
    {
        public AutorisationWindow()
        {
            InitializeComponent();
        }
        private bool _isLoggedIn = false;

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Здесь должна быть логика авторизации
            // В случае успеха:
            _isLoggedIn = true;
            LogInWindow window = new LogInWindow();
            window.Owner = this;
            window.Show();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Здесь должна быть логика регистрации
            // После успешной регистрации, возможно, сразу авторизовать пользователя
            _isLoggedIn = true;
            RegistrationWindow window = new RegistrationWindow();
            window.Owner = this;
            window.Show();
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
