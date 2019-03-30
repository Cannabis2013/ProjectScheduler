﻿using System;


namespace ProjectNameSpace
{
    public class TimeObject
    {
        public TimeObject(int hours, string userName)
        {
            this.sethours(hours);
            this.userName = userName ?? null;
            registerDate = DateTime.Now;
        }

        public DayOfWeek getday() => day;

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

        private readonly DayOfWeek day;
        private readonly DateTime registerDate;
        private int hours;

        public DateTime registeredDate() => registerDate;

        public string userName { get; set; } = null;
    }
}