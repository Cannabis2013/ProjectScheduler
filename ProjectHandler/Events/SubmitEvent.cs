using System;
using Projecthandler.Project_related;

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

        public Project Project()
        {
            return p;
        }

        public Activity Activity()
        {
            return a;
        }
    }
}