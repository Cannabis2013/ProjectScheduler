// ReSharper disable once CheckNamespace

namespace VirtualUserDomain
{
    public class User
    {
        /*
         * Public properties section ends
         */

        /*
         * Public member fields
         */

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

        // Private member fields begins

        private readonly string fName;
        private readonly string pass;

        /*
         * Public member fields ends
         */

        /*
         * Private member fields begins
         */

        private readonly string t;
        /*
         * Constructor section begins
         */

        public User(string userName, string passWord, UserRole role, string fullName)
        {
            t = userName;
            pass = passWord;
            this.role = role;
            fName = fullName;
        }

        public UserRole role { get; }
        public string localAddress { get; set; }

        /*
         * Constructor section ends
         */

        /*
         * Public static properties section begins
         */

        public static string _roleStringRepresentation(UserRole r)
        {
            return r == UserRole.Admin ? "Administrator" : "Employee";
        }

        /*
         * Public static properties section ends
         */

        public string userName()
        {
            return t;
        }

        public string fullName()
        {
            return fName;
        }

        public string passWord()
        {
            return pass;
        }

        /*
         * Private member fields ends
         */
    }
}