namespace Projecthandler.User_Management
{
    public class User
    {

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

        private readonly string fName;
        private readonly string pass;
        private readonly string t;

        public User(string userName, string passWord, UserRole role, string fullName)
        {
            t = userName;
            pass = passWord;
            Role = role;
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
            return t;
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