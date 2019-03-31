using System;
using System.Collections.Generic;
using NUnit.Framework;
using ProjectNameSpace;

namespace Projecthandler.Events
{
    public class SubmitEvent : EventArgs
    {
        public string pTitle { get; private set; }
        public int sWeek { get; private set; }
        public int eWeek { get; private set; }
        public string[] users { get; private set; }
        public string pLeader { get; private set; }

        public SubmitEvent(string pTitle, int sWeek, int eWeek, string[] users, string pLeader)
        {
            this.pTitle = pTitle;
            this.sWeek = sWeek;
            this.eWeek = eWeek;
            this.users = users;
            this.pLeader = pLeader;
        }
    }
}
