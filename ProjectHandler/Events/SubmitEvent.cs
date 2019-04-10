using System;
using ProjectRelated;

namespace Projecthandler.Events
{
    public class SubmitEvent : EventArgs
    {
        private readonly RegistrationObject R;
        private readonly Activity A;
        private readonly Project P;

        public SubmitEvent(Project p) => P = p;

        public SubmitEvent(Activity a) => A = a;

        public SubmitEvent(RegistrationObject r) => R = r;

        public Project Project() => P;
        public Activity Activity() => A;
        public RegistrationObject RegistrationObject() => R;
    }
}