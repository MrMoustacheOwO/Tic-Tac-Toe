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
    /// Логика взаимодействия для GameVsComputerWindow.xaml
    /// </summary>
    public partial class GameVsComputerWindow : Window
    {
        public GameVsComputerWindow()
        {
            InitializeComponent();
        }
        public void LeaveMatch(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Вы действительно хотите покинуть матч?",
                "Подтверждение выхода",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MainPageWindow mainWindow = new MainPageWindow();
                mainWindow.Show();
                this.Close();

                // Открываем окно MainPageWindow

            }
            // Если пользователь нажал "Нет", ничего не делаем и остаемся в игре
        }
    }
}
