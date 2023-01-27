using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit.UI.Domain
{
    public sealed class User
    {
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }

        public User(string username)
        {
            Username = username;
        }
        public User(string username, string hashedPassword, string salt)
        {
            Username = username;
            HashedPassword = hashedPassword;
            Salt = salt;
        }
    }
}
