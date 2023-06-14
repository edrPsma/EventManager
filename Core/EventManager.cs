using System;
using System.Collections.Generic;
using UnityEngine;

namespace EG.Event
{
    public class EventManager : EventSource, IEventManager
    {
        Dictionary<object, IEventSource> mSourceDic;
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
                    var self = (mInstance as EventManager);
                    self.mSourceDic = new Dictionary<object, IEventSource>();
                }
                return mInstance;
            }
        }
        #endregion

        #region 接口实现
        public IEventSource CreateEvenntSource<TSource>()
        {
            var key = typeof(TSource);
            return CreateEvenntSource(key);
        }

        public IEventSource CreateEvenntSource(object key)
        {
            if (mSourceDic.ContainsKey(key))
            {
                throw new DuplicateEventSourceException(key.ToString());
            }

            var result = new EventSource(key.ToString());
            mSourceDic.Add(key, result);
            return result;
        }

        public IEventSource GetEventSource(object key)
        {
            if (!mSourceDic.ContainsKey(key)) return null;

            return mSourceDic[key];
        }

        public IEventSource GetEventSource<TSource>()
        {
            var key = typeof(TSource);
            return GetEventSource(key);
        }
        #endregion
    }

}

