using System;

namespace ProjectRelated
{
    public class ActivityEntity
    {
        private readonly string title;
        private readonly DateTime startDate, endDate;
        private ActivityModel.ActivityType aType;

        public ActivityEntity(string title, DateTime endDate, DateTime startDate, ActivityModel.ActivityType type)
        {
            this.title = title;
            this.endDate = endDate;
            this.startDate = startDate;
            this.aType = type;
        }

        public ActivityModel.ActivityType TypeOfActivity => aType;

        public string activityId => title;
        public DateTime StartDate => startDate;

        public DateTime EndDate => endDate;

        public bool withinTimespan(DateTime date)
        {
            return startDate.CompareTo(date) <= 0 && endDate.CompareTo(date) >= 0;
        }
    }
}