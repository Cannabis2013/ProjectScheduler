using System;

namespace Projecthandler.Project_related
{
    public class ActivityEntity
    {
        private readonly string title;

        public ActivityEntity(int sWeek, int eWeek, string title)
        {
            StartWeek = sWeek;
            EndWeek = eWeek;
            this.title = title;
        }

        public string ActivityId => title;
        public int StartWeek { get; }

        public int EndWeek { get; }

        public bool WithinTimespan(int val)
        {
            return val >= EndWeek && val <= StartWeek;
        }
    }
}