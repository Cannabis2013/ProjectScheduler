// ReSharper disable once CheckNamespace

namespace VirtualUserDomain
{
    public class User
    {
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

        
        public User(string userName, string passWord, UserRole role)
        {
            uName = userName;
            pass = passWord;
            this.Role = role;
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

        public string PassWord()
        {
            return pass;
        }
    }
}