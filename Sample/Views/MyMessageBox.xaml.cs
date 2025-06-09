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
    /// Логика взаимодействия для MyMessageBox.xaml
    /// </summary>
    public partial class MyMessageBox : Window
    {
        public Window window;
        public MyMessageBox(Window window)
        {
            this.window = window;
            InitializeComponent();
        }
        public bool Result { get; private set; }  // True = Повторить, False = Главное меню

        public MyMessageBox(string message)
        {
            InitializeComponent();
            MessageTextBlock.Text = message;
        }

        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            DialogResult = true; // Закрывает окно и возвращает DialogResult = true
            Close();
        }

        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            Result = false;
            DialogResult = false; // Закрывает окно и возвращает DialogResult = false
            Close();
        }

        public static bool Show(string message)
        {
            MyMessageBox dialog = new MyMessageBox(message);
            dialog.ShowDialog();
            return dialog.Result;
        }
    }
}
