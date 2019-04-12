// ReSharper disable once CheckNamespace

using System.Collections.Generic;
using System.Windows.Forms;
using Templates;

namespace VirtualUserDomain
{
    public class UserModel : AbstractModel<UserManager,UserModel>
    {
        private readonly string passWord;

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
            this.passWord = passWord;
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

        public string PassWord => passWord;

        public override ListViewItem ItemModel()
        {
            var model = new ListViewItem(ModelIdentity);
            model.SubItems.Add(_roleStringRepresentation(Role));

            return model;
        }

        public override void RemoveSubModel(string SubModelId)
        {
            throw new System.NotImplementedException();
        }

        public override UserModel SubModel(string SubModelIdentity)
        {
            throw new System.NotImplementedException();
        }
    }
}