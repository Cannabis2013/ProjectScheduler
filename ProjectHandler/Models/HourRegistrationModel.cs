using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Templates;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace ProjectRelated
{
    [Serializable]
    public class HourRegistrationModel : AbstractModel
    {
        private readonly DateTime originalRegistrationDate;
        private string activityTextContent;
        private int hours;


        public HourRegistrationModel(string identity,int hours, string userName, string text, AbstractModel ParentActivity)
        {
            ModelIdentity = identity;
            this.Hours = hours;
            this.UserName = userName;
            this.activityTextContent = text;

            ParentActivity.AddSubModel(this);

            originalRegistrationDate = DateTime.Now;
            SubModels = new List<AbstractModel>();
        }

        public string UserName { get; }

        public int Hours
        {
            get => hours;
            set => hours = value;
        }

        public DateTime OriginRegistrationDate() => originalRegistrationDate;
        
        public string ShortDescription
        {
            get => activityTextContent;
            set => activityTextContent = value;
        }

        public string CorrespondingProjectId(ProjectManager pManager)
        {
            return pManager.Model(ParentModelIdentity()).ParentModelIdentity();
        }

        public override ListViewItem ItemModel()
        {
            var model = new ListViewItem(ModelIdentity);
            model.SubItems.Add(UserName);
            model.SubItems.Add(Hours.ToString());
            model.SubItems.Add(originalRegistrationDate.ToString("dd/MM/yyyy"));
            model.SubItems.Add(ParentModelIdentity());
            model.StateImageIndex = 0;

            return model;
        }
    }
}