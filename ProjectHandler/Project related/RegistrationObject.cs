using System;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Templates;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace ProjectRelated
{
    [Serializable]
    public class RegistrationObject : ItemModelEntity<ListViewItem>
    {
        private readonly DateTime originalRegistrationDate;
        private DateTime latestEditDate;
        private string parentActivityId;
        private string userName;
        private string activityTextContent;

        public int Hours { get; set; }

        public RegistrationObject(int hours, string userName, string text, Activity owner)
        {
            this.Hours = hours;
            this.userName = userName;
            this.activityTextContent = text;
            owner.AddTimeObject(this);
            parentActivityId = owner.ActivityId;

            latestEditDate = DateTime.Now;
        }

        public RegistrationObject(RegistrationObject copy, string activityTextContent)
        {
            this.activityTextContent = activityTextContent;
            parentActivityId = copy.ParentActivityId;
            userName = copy.UserName;
            Hours = copy.Hours;
            originalRegistrationDate = copy.originalRegistrationDate;
        }

        public DateTime OriginRegistrationDate() => originalRegistrationDate;
        public DateTime LastEditDate() => latestEditDate;

        public string ParentActivityId
        {
            get => parentActivityId;
            set => parentActivityId = value;
        }

        public string UserName
        {
            get => userName;
            set => userName = value;
        }

        public string CorrespondingProjectId(ProjectManager pManager)
        {
            return pManager.Activity(ParentActivityId).ParentProjectId;
        }

        public override ListViewItem ItemModel(ListMode mode = ListMode.Tile)
        {
            var model = new ListViewItem();

            var userId = new StringBuilder("User: ");
            userId.Append(UserName);
            model.Text = userId.ToString();

            var hourString = new StringBuilder("Registered hours: ");
            hourString.Append(Hours.ToString());
            model.SubItems.Add(hourString.ToString());

            model.SubItems.Add(Hours.ToString());

            var origWeek = new StringBuilder("Original registered week: ");
            origWeek.Append(originalRegistrationDate.ToString());

            origWeek.Append("(");
            origWeek.Append(latestEditDate.ToString());
            origWeek.Append(")");
            model.SubItems.Add(origWeek.ToString());

            model.StateImageIndex = 0;

            return model;
        }
    }
}