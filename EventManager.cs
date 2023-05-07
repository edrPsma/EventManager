using System;
using System.Collections.Generic;
using UnityEngine;

namespace EG.Event
{
    public class EventManager : IEvent
    {
        #region 类定义
        interface IRegisterations { }
        class Registerations<TEvent> : IRegisterations
        {
            public Action<TEvent> onEvent = _ => { };
        }

        #endregion

        #region 单例模式
        private EventManager() { }
        static IEvent mInstance;
        public static IEvent Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new EventManager();
                }
                return mInstance;
            }
        }

        #endregion

        #region 成员变量
        Dictionary<object, IRegisterations> mEventDir = new Dictionary<object, IRegisterations>();
        //用于缓存BindableProperty中首次触发的事件
        Dictionary<object, object> mCacheList = new Dictionary<object, object>();
        #endregion

        #region 接口实现
        public IUnRegister Register<TEvent>(Action<TEvent> onEvent) where TEvent : new()
        {
            var type = typeof(TEvent);
            IRegisterations registerations;

            if (!mEventDir.TryGetValue(type, out registerations))
            {
                registerations = new Registerations<TEvent>();
                mEventDir.Add(type, registerations);
            }

            (registerations as Registerations<TEvent>).onEvent += onEvent;
            return new UnRegister<TEvent>() { onEvent = onEvent, Event = this, EventName = type };
        }

        /// <returns></returns>
        public IUnRegister AddListener<TEvent>(object eventName, Action<TEvent> onEvent) where TEvent : struct
        {
            IRegisterations registerations;

            if (!mEventDir.TryGetValue(eventName, out registerations))
            {
                registerations = new Registerations<TEvent>();
                mEventDir.Add(eventName, registerations);
            }

            (registerations as Registerations<TEvent>).onEvent += onEvent;

            TriggerBindablePropertyInFirst<TEvent>(eventName);

            return new UnRegister<TEvent>() { onEvent = onEvent, Event = this, EventName = eventName };
        }

        public void Trigger<TEvent>() where TEvent : new()
        {
            var e = new TEvent();
            Trigger<TEvent>(e);
        }

        public void Trigger<TEvent>(object eventName, TEvent e)
        {
            IRegisterations registerations;
            if (mEventDir.TryGetValue(eventName, out registerations))
            {
                (registerations as Registerations<TEvent>).onEvent(e);
            }
        }

        public void Trigger<TEvent>(TEvent e)
        {
            var type = typeof(TEvent);
            IRegisterations registerations;

            if (mEventDir.TryGetValue(type, out registerations))
            {
                (registerations as Registerations<TEvent>).onEvent(e);
            }
        }

        public void UnRegister<TEvent>(Action<TEvent> onEvent)
        {
            var type = typeof(TEvent);
            IRegisterations registerations;

            if (mEventDir.TryGetValue(type, out registerations))
            {
                (registerations as Registerations<TEvent>).onEvent -= onEvent;
            }
        }

        public void UnRegister<TEvent>(object key, Action<TEvent> onEvent)
        {
            IRegisterations registerations;

            if (mEventDir.TryGetValue(key, out registerations))
            {
                (registerations as Registerations<TEvent>).onEvent -= onEvent;
            }
        }
        #endregion

        #region Fun
        public void AddBindablePropertyCache(object eventName, object value)
        {
            if (mCacheList.ContainsKey(eventName)) return;

            mCacheList.Add(eventName, value);
        }

        void TriggerBindablePropertyInFirst<TEvent>(object eventName)
        {
            foreach (var item in mCacheList)
            {
                if (item.Key.Equals(eventName))
                {
                    Trigger<TEvent>(eventName, (TEvent)item.Value);
                }
            }
        }

        #endregion


    }
}
