using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectNameSpace
{
    public class ActivityEntity
    {
        public ActivityEntity(int sWeek, int eWeek, string title)
        {
            this.sWeek = sWeek;
            this.eWeek = eWeek;
            this.title = title;
        }

        public bool withinTimespan(int val) => val >= eWeek && val <= sWeek;

        public string activityId => title;
        public int startWeek => sWeek;
        public int endWeek => eWeek;

        private readonly string title;
        private readonly int sWeek;
        private readonly int eWeek;
    }
}
