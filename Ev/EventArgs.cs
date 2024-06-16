using System;

namespace Ev
{
    internal struct EventArgs
    {
        public object EventName;
        public object Args;

        public EventArgs(object eventName, object args)
        {
            EventName = eventName;
            Args = args;
        }
    }
}
