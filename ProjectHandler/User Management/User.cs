﻿// ReSharper disable once CheckNamespace

namespace VirtualUserDomain
{
    public class User
    {
        private readonly string fName;
        private readonly string pass;
        private readonly string uName;

        public enum Availability
        {
            NotAvailable,
            PartlyAvailable,
            Available
        }

        public enum UserRole
        {
            Admin,
            Employee
        }

        
        public User(string userName, string passWord, UserRole role, string fullName)
        {
            uName = userName;
            pass = passWord;
            this.Role = role;
            fName = fullName;
        }

        public UserRole Role { get; }
        public string LocalAddress { get; set; }
        
        public static string _roleStringRepresentation(UserRole r)
        {
            return r == UserRole.Admin ? "Administrator" : "Employee";
        }

        public string UserName()
        {
            return uName;
        }

        public string FullName()
        {
            return fName;
        }

        public string PassWord()
        {
            return pass;
        }
    }
}