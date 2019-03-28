using System;

namespace Projecthandler.Custom_events
{
    public class MyEventArgs : EventArgs
    {
        public MyEventArgs(string arg1, string arg2)
        {
            this.arg1 = arg1;
            this.arg2 = arg2;
        }

        public string arg1;
        public string arg2;
    }

    
}
