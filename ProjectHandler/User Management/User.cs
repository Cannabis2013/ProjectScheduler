using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace VirtualUserDomain
{
    public class User
    {
        public User(string userName, string passWord, UserRole role)
        {
            
            this.UserName = userName;
            this.passWord = passWord;
            this.Role = role;
        }

        public string fullName { get; set; }
        public UserRole Role { get; }
        public string LocalAdress { get; set; }


        internal string UserName { get; }
        internal string passWord { get; }
        public enum UserRole { Admin, leader, employee };
    }
}
