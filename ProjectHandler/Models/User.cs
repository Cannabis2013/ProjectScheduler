// ReSharper disable once CheckNamespace

using System.Collections.Generic;
using System.Windows.Forms;
using NUnit.Framework.Internal;
using Projecthandler.Templates_and_interfaces;
using Templates;

namespace VirtualUserDomain
{
    public class UserModel : AbstractModel<ListViewItem,UserModel>
    {
        private readonly string pass;

        private static List<string> _AlternativeItemModelHeaders = new List<string>()
        {
            "Username",
            "User role"
        };
        private static List<string> _ItemModelHeaders;

        public static List<string> ItemModelHeaders => _ItemModelHeaders;
        public static List<string> AlternativeItemModelHeaders => _AlternativeItemModelHeaders;

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

        
        public UserModel(string userName, string passWord, UserRole role)
        {
            ModelIdentity = userName;
            pass = passWord;
            this.Role = role;
        }

        public UserRole Role { get; }
        public string LocalAddress { get; set; }
        
        public static string _roleStringRepresentation(UserRole r)
        {
            return r == UserRole.Admin ? "Administrator" : "Employee";
        }

        private static string _AvailabilityStringRepresentation(Availability availability)
        {
            if (availability == Availability.Available)
                return "Available";
            if (availability == Availability.PartlyAvailable)
                return "Partly available";

            return "Not available";
        }

        public string PassWord => pass;

        public override ListViewItem ItemModel()
        {
            var model = new ListViewItem(ModelIdentity);
            model.SubItems.Add(_roleStringRepresentation(Role));

            return model;
        }
    }
}