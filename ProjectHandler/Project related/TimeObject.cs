using System;
using System.Globalization;

// ReSharper disable FieldCanBeMadeReadOnly.Local


namespace Projecthandler.Project_related
{
    public class TimeObject
    {
        private readonly int originalRegistrationWeek;
        private int latestEditedWeek;


        public TimeObject(TimeObject copy)
        {
            Owner = copy.Owner;
            UserName = copy.UserName;
            Hours = copy.Hours;
            originalRegistrationWeek = copy.Week();
        }

        public Activity Owner { get; set; }

        public int Hours { get; set; }

        public string UserName { get; set; }

        public int Week()
        {
            return originalRegistrationWeek == latestEditedWeek ? originalRegistrationWeek : latestEditedWeek;
        }
    }
}