using System;
using System.Collections.Generic;
using UnityEngine;

namespace EG.Event
{
    public class EventManager : IEventManager
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
        static IEventManager mInstance;
        public static IEventManager Instance
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
        Dictionary<object, object> mBindablePropertyCacheList = new Dictionary<object, object>();
        #endregion

        #region 接口实现
        public IUnRegister Register<TEvent>(Action<TEvent> onEvent) where TEvent : new()
        {
            var type = typeof(TEvent);
            return Register<TEvent>(type, onEvent);
        }

        public IUnRegister Register<TEvent>(object eventName, Action<TEvent> onEvent)
        {
            IRegisterations registerations;

            if (!mEventDir.TryGetValue(eventName, out registerations))
            {
                registerations = new Registerations<TEvent>();
                mEventDir.Add(eventName, registerations);
            }

            var r = (registerations as Registerations<TEvent>);
            if (r == null)
            {
                throw new DuplicateEventNameException(eventName.ToString());
            }
            r.onEvent += onEvent;

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
            Trigger<TEvent>(type, e);
        }

        public void UnRegister<TEvent>(Action<TEvent> onEvent)
        {
            var type = typeof(TEvent);
            UnRegister<TEvent>(type, onEvent);
        }

        public void UnRegister<TEvent>(object eventName, Action<TEvent> onEvent)
        {
            IRegisterations registerations;

            if (mEventDir.TryGetValue(eventName, out registerations))
            {
                (registerations as Registerations<TEvent>).onEvent -= onEvent;
            }
        }
        #endregion

        #region Fun
        public void AddBindablePropertyCache(object eventName, object value)
        {
            if (mBindablePropertyCacheList.ContainsKey(eventName)) return;

            mBindablePropertyCacheList.Add(eventName, value);
        }

        void TriggerBindablePropertyInFirst<TEvent>(object eventName)
        {
            if (mBindablePropertyCacheList.ContainsKey(eventName))
            {
                Trigger<TEvent>(eventName, (TEvent)mBindablePropertyCacheList[eventName]);
            }
        }

        #endregion


    }
}
