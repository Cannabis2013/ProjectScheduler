using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Projecthandler.Object_wrappers
{
    public class TimeObject
    {
        private readonly DayOfWeek day;

        public TimeObject(int hours, string userID)
        {
            this.sethours(hours);
            UserID = userID ?? null;
            registerDate = DateTime.Now;
        }

        public DayOfWeek getday()
        {
            return day;
        }

        public string dayToString()
        {
            if (day == (DayOfWeek)0)
                return "Sunday";
            else if (day == (DayOfWeek)1)
                return "Monday";
            else if (day == (DayOfWeek)2)
                return "Tuesday";
            else if (day == (DayOfWeek)3)
                return "Wednesday";
            else if (day == (DayOfWeek)4)
                return "Thursday";
            else if (day == (DayOfWeek)5)
                return "Friday";
            else if (day == (DayOfWeek)6)
                return "Saturday";
            else
                return null;

        }

        public int gethours()
        {
            return hours;
        }

        internal void sethours(int h) => hours = h;
        internal void addHours(int h) => hours += h;

        private readonly DateTime registerDate;
        private int hours;

        public DateTime registeredDate() => registerDate;

        public string UserID { get; set; } = null;
    }
}
