using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Projecthandler.Templates_and_interfaces;
using Templates;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace ProjectRelated
{
    [Serializable]
    public class HourRegistrationModel : AbstractModel<ListViewItem,HourRegistrationModel>
    {
        private readonly DateTime originalRegistrationDate;
        private string regId;
        private string parentActivityId;
        private string userName;
        private string activityTextContent;

        public int Hours { get; set; }

        public HourRegistrationModel(string title,int hours, string userName, string text, string activityId)
        {
            regId = title;
            this.Hours = hours;
            this.userName = userName;
            this.activityTextContent = text;
            parentActivityId = activityId;

            originalRegistrationDate = DateTime.Now;
            Childrens = new List<HourRegistrationModel>();
        }

        public string RegistrationId
        {
            get => regId;
            set => regId = value;
        }

        public DateTime OriginRegistrationDate() => originalRegistrationDate;

        public string ParentActivityId
        {
            get => parentActivityId;
            set => parentActivityId = value;
        }

        public string ModelIdentity
        {
            get => userName;
            set => userName = value;
        }

        public string Description
        {
            get => activityTextContent;
            set => activityTextContent = value;
        }

        public string CorrespondingProjectId(ProjectManager pManager)
        {
            return pManager.getActivityModel(ParentActivityId).ParentProjectId;
        }

        public override ListViewItem ItemModel()
        {
            var model = new ListViewItem(RegistrationId) { Text = regId };
            model.SubItems.Add(userName);
            model.SubItems.Add(Hours.ToString());
            model.SubItems.Add(originalRegistrationDate.ToString("dd/MM/yyyy"));
            model.SubItems.Add(parentActivityId);
            model.StateImageIndex = 0;

            return model;
        }
    }
}