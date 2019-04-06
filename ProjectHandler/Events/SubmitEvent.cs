using System;
using ProjectRelated;

namespace Projecthandler.Events
{
    public class SubmitEvent : EventArgs
    {
        private readonly Activity a;
        private readonly Project p;

        public SubmitEvent(Project p)
        {
            this.p = p;
        }

        public SubmitEvent(Activity a)
        {
            this.a = a;
        }

        public Project project()
        {
            return p;
        }

        public Activity activity()
        {
            return a;
        }
    }
}