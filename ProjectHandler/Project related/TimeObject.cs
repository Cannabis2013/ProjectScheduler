using System;
using System.Globalization;

// ReSharper disable FieldCanBeMadeReadOnly.Local


namespace ProjectNameSpace
{
    public class TimeObject
    {
        private readonly int originalRegistrationWeek;
        private int hours;
        private int latestEditedWeek;
        private Activity parent;


        private string userName;
        /*
         * Constructor section begins
         */

        public TimeObject(int hours, string userName)
        {
            this.hours = hours;
            this.userName = userName;

            var ciCurr = CultureInfo.CurrentCulture;
            originalRegistrationWeek = latestEditedWeek = ciCurr.Calendar.GetWeekOfYear(DateTime.Now,
                CalendarWeekRule.FirstFourDayWeek,
                DayOfWeek.Monday);
        }

        public TimeObject(int hours, string userName, Activity owner)
        {
            this.hours = hours;
            this.userName = userName;
            owner.addTimeObject(this);

            var ciCurr = CultureInfo.CurrentCulture;
            originalRegistrationWeek = latestEditedWeek = ciCurr.Calendar.GetWeekOfYear(DateTime.Now,
                CalendarWeekRule.FirstFourDayWeek,
                DayOfWeek.Monday);
        }

        /*
         * Copy constructor
         */

        public TimeObject(TimeObject copy)
        {
            parent = copy.owner;
            userName = copy.UserName;
            hours = copy.Hours;
            originalRegistrationWeek = copy.Week();
        }

        /*
         * Constructor section ends
         */

        /*
         * Public properties section begins
         * - {get;set;} the owner of the object, which in this case is the target activity
         * - Get the hours assigned to the target activity
         * - Get the registration week value
         */

        public Activity owner
        {
            get => parent;
            set => parent = value;
        }

        public int Hours
        {
            get => hours;
            set => hours = value;
        }


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