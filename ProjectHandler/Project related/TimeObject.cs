using System;
using System.Globalization;
// ReSharper disable FieldCanBeMadeReadOnly.Local


namespace ProjectNameSpace
{
    public class TimeObject
    {
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
            this.parent = copy.owner;
            this.userName = copy.UserName;
            this.hours = copy.Hours;
            this.originalRegistrationWeek = copy.Week();
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

        public int Week() => (originalRegistrationWeek == latestEditedWeek) ? 
            originalRegistrationWeek : latestEditedWeek;
        

        public string UserName
        {
            get => userName;
            set => userName = value;
        }


        private string userName;
        private readonly int originalRegistrationWeek;
        private int latestEditedWeek;
        private int hours;
        private Activity parent;
    }
}
