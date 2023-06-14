using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateEventSourceException : Exception
{
    public DuplicateEventSourceException(string key, string oldEventSourceType, string newEventSourceType) :
    base(string.Format("重复的事件源,key：<color=red>{0}</color>,已注册的事件源类型：<color=red>{1}</color>，新的事件源类型：<color=red>{2}</color>,key值应保证唯一", key, oldEventSourceType, newEventSourceType))
    {

    }
}
