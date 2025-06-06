using Sample.ForDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ViewModels
{
    internal class AutorisationWindowViewModel
    {
       private ApplicationContext db;

        
        public AutorisationWindowViewModel()
        {
            db = new ApplicationContext();
        }


        /*Тут типо должна быть логика авторизации и регистрации, но ты пока ее не сделала(
         * Региастрация идет форма для заполнения, логин имайл и пароль
         * Там типо проверки на то то то
         * При нажатии на кнопку ты запускаешь проверку на корректные данные и в коце вызываешь это
        */
        //User user=new User(login,email,password);
        //db.Users.Add(user);
        //db.SaveChanges();
    }
}
