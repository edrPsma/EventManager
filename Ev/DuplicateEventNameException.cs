using System;

namespace Ev
{
    internal class DuplicateEventNameException : Exception
    {
        public DuplicateEventNameException(string eventName) :
        base(string.Format("重复的事件名：<color=red>{0}</color>,不同的事件类型不能使用同一个事件名", eventName))
        {

        }
    }
}
