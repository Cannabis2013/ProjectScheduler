using System;
using System.Globalization;

// ReSharper disable FieldCanBeMadeReadOnly.Local


namespace ProjectRelated
{
    [Serializable]
    public class TimeObject
    {
        private readonly int originalRegistrationWeek;
        private int latestEditedWeek;
        private Activity parent;


        private string userName;
        /*
         * Constructor section begins
         */

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

            var ciCurr = CultureInfo.CurrentCulture;
            originalRegistrationWeek = latestEditedWeek = ciCurr.Calendar.GetWeekOfYear(DateTime.Now,
                CalendarWeekRule.FirstFourDayWeek,
                DayOfWeek.Monday);
        }

        public TimeObject(TimeObject copy)
        {
            parent = copy.owner;
            userName = copy.UserName;
            Hours = copy.Hours;
            originalRegistrationWeek = copy.Week();
        }

        public Activity owner
        {
            get => parent;
            set => parent = value;
        }

        public int Hours { get; set; }


        public string UserName
        {
            get => userName;
            set => userName = value;
        }

        public int Week()
        {
            return originalRegistrationWeek == latestEditedWeek ? originalRegistrationWeek : latestEditedWeek;
        }
    }
}