using System;
using System.Collections.Generic;
using UnityEngine;

namespace EG.Event
{
    public class EventManager : EventSource<EventManager>, IEventManager
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
            return CreateEvenntSource<TSource>(key);
        }

        public IEventSource CreateEvenntSource<TSource>(object key)
        {
            if (mSourceDic.ContainsKey(key))
            {
                throw new DuplicateEventSourceException(key.ToString(), mSourceDic[key].GetType().ToString(), typeof(TSource).ToString());
            }

            var result = new EventSource<TSource>(key.ToString());
            mSourceDic.Add(key, result);
            return result;
        }

        public IEventSource GetEventSource<TSource>(object key)
        {
            if (!mSourceDic.ContainsKey(key)) return null;

            return mSourceDic[key];
        }

        public IEventSource GetEventSource<TSource>()
        {
            var key = typeof(TSource);
            return GetEventSource<TSource>(key);
        }
        #endregion
    }

}

