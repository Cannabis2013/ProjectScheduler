namespace ProjectRelated
{
    public class ActivityEntity
    {
        private readonly string title;

        public ActivityEntity(int sWeek, int eWeek, string title)
        {
            startWeek = sWeek;
            endWeek = eWeek;
            this.title = title;
        }

        public string activityId => title;
        public int startWeek { get; }

        public int endWeek { get; }

        public bool withinTimespan(int val)
        {
            return val >= endWeek && val <= startWeek;
        }
    }
}