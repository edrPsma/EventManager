using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateEventSourceException : Exception
{
    public DuplicateEventSourceException(string key) :
    base(string.Format("重复的事件源,key：<color=red>{0}</color>,key值应保证唯一", key))
    {

    }
}
