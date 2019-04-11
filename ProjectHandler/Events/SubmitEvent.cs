using System;
using ProjectRelated;

namespace Projecthandler.Events
{
    public class SubmitEvent : EventArgs
    {
        private readonly HourRegistrationModel R;
        private readonly ActivityModel A;
        private readonly ProjectModel P;

        public SubmitEvent(ProjectModel p) => P = p;

        public SubmitEvent(ActivityModel a) => A = a;

        public SubmitEvent(HourRegistrationModel r) => R = r;

        public ProjectModel Project() => P;
        public ActivityModel Activity() => A;
        public HourRegistrationModel RegistrationObject() => R;
    }
}