using System;
using System.Collections.Generic;
using UnityEngine;

namespace EG.Event
{
    public class EventManager : EventSource, IEventManager
    {
        Dictionary<object, IEventSource> mSourceDic = new Dictionary<object, IEventSource>();
        #region 单例模式
        private EventManager(string sourceName) : base(sourceName) { }
        static EventManager mInstance;
        public static EventManager Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new EventManager(typeof(EventManager).ToString());
                }
                return mInstance;
            }
        }
        #endregion

        #region 接口实现
        public IEventSource CreateOrGetEvenntSource<TSource>()
        {
            var key = typeof(TSource);
            return CreateOrGetEvenntSource(key);
        }

        public IEventSource CreateOrGetEvenntSource(object key)
        {
            if (mSourceDic.ContainsKey(key))
            {
                return mSourceDic[key];
            }

            var result = new EventSource(key.ToString());
            mSourceDic.Add(key, result);
            return result;
        }
        #endregion
    }

}

