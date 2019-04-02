using System;
using System.Globalization;


namespace ProjectNameSpace
{
    public class TimeObject
    {
        public TimeObject(int hours, string userName)
        {
            this.hours = hours;
            this.userName = userName;

            var ciCurr = CultureInfo.CurrentCulture;
            week = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, 
                CalendarWeekRule.FirstFourDayWeek, 
                DayOfWeek.Monday);
        }

        /*
         * Copy constructor
         */

        public TimeObject(TimeObject copy)
        {
            this.userName = copy.UserName;
            this.hours = copy.Hours();
            this.week = copy.Week();
        }

        public int Hours() => hours;
        public int Week() => week;

        public void setHours(int h) => hours = h;
        public void addHours(int h) => hours += h;

        public string UserName
        {
            get => userName;
            set => userName = value;
        }


        private string userName;
        private readonly int week;
        private int hours;


    }
}
