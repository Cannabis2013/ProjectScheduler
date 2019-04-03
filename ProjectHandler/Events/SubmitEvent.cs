using System;
using System.Collections.Generic;
using NUnit.Framework;
using ProjectNameSpace;

namespace Projecthandler.Events
{
    public class SubmitEvent : EventArgs
    {
        private readonly Project p = null;
        private readonly  Activity a = null;

        public Project project() => p;
        public Activity activity() => a;

        public SubmitEvent(Project p) => this.p = p;
        public SubmitEvent(Activity a) => this.a = a;
    }
}
