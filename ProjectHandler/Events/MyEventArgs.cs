using System;

namespace Projecthandler.Events
{
    public class MyEventArgs : EventArgs
    {
        public string Arg1;
        public string Arg2;

        public MyEventArgs(string arg1, string arg2)
        {
            Arg1 = arg1;
            Arg2 = arg2;
        }
    }
}