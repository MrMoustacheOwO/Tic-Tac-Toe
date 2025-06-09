using Sample.Models;
using Sample.ViewModels;
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
    /// Логика взаимодействия для GameSettingsWindow.xaml
    /// </summary>
    public partial class GameSettingsWindow : Window
    {
        private Window parentWindow;
        private GameVsComputerWindowViewModel gamewViewModel;
        private Window gamew;
        public GameSettingsWindow(Window window)
        {
            parentWindow = window;
            InitializeComponent();
            SideModeBox.Visibility = Visibility.Hidden;
            SideLabel.Visibility = Visibility.Hidden;
            gamewViewModel = new GameVsComputerWindowViewModel();
        }
        private void GameModeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(GameModeBox.SelectedIndex==0)
            {
                gamew = new MainWindow();

                SideLabel.Visibility = Visibility.Hidden;
                SideModeBox.Visibility=Visibility.Hidden;
            }
            else if (GameModeBox.SelectedIndex == 1)
            {
                gamew = new GameVsComputerWindow();
                //SideModeBox.Visibility = Visibility.Visible;
                //SideLabel.Visibility = Visibility.Visible;
            }
            if (gamew is null) throw new Exception("null");
        }
        private void SideModeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SideModeBox.SelectedIndex==0)
            {
                gamewViewModel.CurrentPlayerStatus = CellStatus.Cross;
            }
            else if(SideModeBox.SelectedIndex == 1)
            {
                gamewViewModel.CurrentPlayerStatus = CellStatus.Circle;
            }
        }
        private void Leave_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void StartButton_Click(Object sender, RoutedEventArgs e)
        {
            if(gamew is null) gamew=new GameVsComputerWindow();
            gamew.Show();
            parentWindow.Close();
            this.Close();
        }
    }
}
