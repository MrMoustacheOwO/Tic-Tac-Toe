using Sample.ForDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Sample.Models;
using System.Security;
using System.Runtime.InteropServices;
using Prism.Mvvm;

namespace Sample.ViewModels
{
    internal class AutorisationWindowViewModel : BindableBase
    {
        private ApplicationContext db;

        // Свойства для привязки к полям ввода
        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        // Private backing field for the secure password
        private SecureString _securePassword = new SecureString();

        // Public property to expose the password
        public SecureString SecurePassword
        {
            get { return _securePassword; }
            set
            {
                _securePassword = value;
                OnPropertyChanged("SecurePassword");
            }
        }

        private string _repeatPassword;
        public string RepeatPassword
        {
            get { return _repeatPassword; }
            set
            {
                _repeatPassword = value;
                OnPropertyChanged("RepeatPassword");
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage");
                OnPropertyChanged("HasErrorMessage"); // Обновляем видимость ErrorMessage
            }
        }

        // Свойство для видимости сообщения об ошибке
        public bool HasErrorMessage
        {
            get { return !string.IsNullOrEmpty(_errorMessage); }
        }

        public AutorisationWindowViewModel()
        {
            db = new ApplicationContext();
            RegisterCommand = new RelayCommand(Register);
        }

        // Команда для обработки нажатия кнопки "Зарегистрироваться"
        public ICommand RegisterCommand { get; private set; }

        private void Register(object obj)
        {
            ErrorMessage = ""; // Сбрасываем сообщение об ошибке

            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Пожалуйста, заполните все поля.";
                return;
            }

            if (Password != RepeatPassword)
            {
                ErrorMessage = "Пароли не совпадают.";
                return;
            }

            // Здесь можно добавить логику для проверки формата email и сложности пароля
            string password = ConvertSecureStringToString(SecurePassword);

            // Создаем нового пользователя
            User user = new User(Login, Email, password);  // Убедитесь, что класс User и конструктор соответствуют

            try
            {
                db.Users.Add(user);
                db.SaveChanges();

                MessageBox.Show("Регистрация успешна!");

                // Закрываем окно регистрации (если нужно)
                // (Window)obj).Close();  //  obj - это окно, переданное командой
                ((Window)obj).Hide(); // Скрываем окно регистрации вместо закрытия. Это позволяет избежать проблем, когда окно создается заново
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Ошибка при регистрации: {ex.Message}";
            }
        }

        public string Password
        {
            get { return ConvertSecureStringToString(SecurePassword); }
            set
            {
                SecurePassword = ConvertStringToSecureString(value);
                OnPropertyChanged("Password");
            }
        }
        // Helper method to convert SecureString to string
        private string ConvertSecureStringToString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
        // Helper method to convert string to SecureString
        private SecureString ConvertStringToSecureString(string password)
        {
            if (password == null)
            {
                return null;
            }

            SecureString securePassword = new SecureString();
            foreach (char c in password)
            {
                securePassword.AppendChar(c);
            }

            securePassword.MakeReadOnly();
            return securePassword;
        }
        new public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Вспомогательный класс для команд (реализация ICommand)
    public class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException("execute");
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
