using System;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Templates;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace ProjectRelated
{
    [Serializable]
    public class TimeObject : ItemModelEntity<ListViewItem>
    {
        private readonly int originalRegistrationWeek;
        private int latestEditedWeek;
        private string parentActivityId;
        private string userName;
        

        public TimeObject(int hours, string userName)
        {
            this.Hours = hours;
            this.userName = userName;

            var ciCurr = CultureInfo.CurrentCulture;
            originalRegistrationWeek = latestEditedWeek = ciCurr.Calendar.GetWeekOfYear(DateTime.Now,
                CalendarWeekRule.FirstFourDayWeek,
                DayOfWeek.Monday);
        }

        public TimeObject(int hours, string userName, Activity owner)
        {
            this.Hours = hours;
            this.userName = userName;
            owner.AddTimeObject(this);
            parentActivityId = owner.ActivityId;

            var ciCurr = CultureInfo.CurrentCulture;
            originalRegistrationWeek = latestEditedWeek = ciCurr.Calendar.GetWeekOfYear(DateTime.Now,
                CalendarWeekRule.FirstFourDayWeek,
                DayOfWeek.Monday);
        }

        public TimeObject(TimeObject copy)
        {
            parentActivityId = copy.ParentActivityId;
            userName = copy.UserName;
            Hours = copy.Hours;
            originalRegistrationWeek = copy.Week();
        }

        public string ParentActivityId
        {
            get => parentActivityId;
            set => parentActivityId = value;
        }

        public int Hours { get; set; }


        public string UserName
        {
            get => userName;
            set => userName = value;
        }

        public string CorrespondingProjectId(ProjectManager pManager)
        {
            return pManager.Activity(ParentActivityId).ParentProjectId;
        }

        public int Week()
        {
            return originalRegistrationWeek == latestEditedWeek ? originalRegistrationWeek : latestEditedWeek;
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
            model.SubItems.Add(Week().ToString());

            var origWeek = new StringBuilder("Original registered week: ");
            origWeek.Append(originalRegistrationWeek.ToString());

            origWeek.Append("(");
            origWeek.Append(latestEditedWeek.ToString());
            origWeek.Append(")");
            model.SubItems.Add(origWeek.ToString());

            model.StateImageIndex = 0;

            return model;
        }
    }
}