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
    /// Логика взаимодействия для MainPageWindow.xaml
    /// </summary>
    public partial class MainPageWindow : Window
    {
        public MainPageWindow()
        {
            InitializeComponent();
            // UpdateUI();
            Window[] openWindows = Application.Current.Windows.OfType<Window>().ToArray();

            // Перебираем окна и закрываем их (кроме текущего):
            foreach (Window window in openWindows)
            {
                if (window != this)  // Не закрываем текущее окно
                {
                    window.Close(); // Закрываем другое окно
                }
            }
        }
        

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            // Здесь должна быть логика начала игры
            GameSettingsWindow gameSettingsWindow = new GameSettingsWindow(this);
            gameSettingsWindow.Owner = this;
            gameSettingsWindow.Show();
            //this.Close();
        }
        private void QuickPlayButton_Click(Object sender, RoutedEventArgs e)
        {
            GameVsComputerWindow mainWindow = new GameVsComputerWindow();
            mainWindow.Show();
            this.Close();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Здесь должна быть логика выхода из системы
            this.Close();
        }
        private void ImageButton_Click(object sender, RoutedEventArgs args)
        {

        }

        //private void UpdateUI()
        //{
        //    UnauthorizedPanel.Visibility = _isLoggedIn ? Visibility.Collapsed : Visibility.Visible;
        //    AuthorizedPanel.Visibility = _isLoggedIn ? Visibility.Visible : Visibility.Collapsed;
        //}
    }
}
