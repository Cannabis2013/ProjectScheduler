using System;
using System.Windows.Forms;
using Projecthandler.Templates_and_interfaces;

namespace ProjectRelated
{
    public class ActivityEntity : AbstractModel<ListViewItem,ActivityEntity>
    {
        private readonly string title;
        private readonly DateTime startDate, endDate;

        public ActivityEntity(string title, DateTime endDate, DateTime startDate)
        {
            this.title = title;
            this.endDate = endDate;
            this.startDate = startDate;
        }

        public string activityId => title;
        public DateTime StartDate => startDate;

        public DateTime EndDate => endDate;

        public bool withinTimespan(DateTime date)
        {
            return startDate.CompareTo(date) <= 0 && endDate.CompareTo(date) >= 0;
        }

        public override ListViewItem ItemModel()
        {
            throw new NotImplementedException();
        }
    }
}