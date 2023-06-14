using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EG.Event
{
    public class BindableProperty<T> : IBindableProperty where T : IEquatable<T>
    {
        #region 字段
        private readonly object mEventName;
        private readonly IEventSource mEventSource;
        private IBindableProperty self;
        #endregion

        object IBindableProperty.Value { get; set; } = default(T);

        #region 属性
        /// <summary>
        /// 值
        /// </summary>
        public T Value
        {
            get => (T)self.Value;
            set
            {
                if (!value.Equals(self.Value))
                {
                    self.Value = value;
                    Synchronize(Synchronization);
                }
            }
        }

        public bool Synchronization { get; set; } = true;
        #endregion

        #region 构造函数
        public BindableProperty(object eventName, T value, bool runInFirst = true) : this(EventManager.Instance, eventName, value, runInFirst) { }

        public BindableProperty(IEventSource source, object eventName, T value, bool runInFirst = true)
        {
            self = (this as IBindableProperty);
            self.Value = value;
            mEventName = eventName;
            mEventSource = source;

            if (runInFirst)
            {
                source.AddBindablePropertyCache(eventName, this);
            }
        }
        #endregion

        #region Fun
        private void Synchronize(bool trigger)
        {
            if (!trigger) return;

            mEventSource.Trigger<T>(mEventName, Value);
        }
        #endregion
    }
}
