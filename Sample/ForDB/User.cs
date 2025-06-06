using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ForDB
{
    internal class User
    {
        public int ID {  get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User()
        {
            
        }

        public User(string login, string email, string pass)
        {
            this.Login = login;
            this.Email = email; 
            this.Password = pass;
        }
    }
}
