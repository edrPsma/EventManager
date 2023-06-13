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
        public BindableProperty(object eventName, T value, bool runInFirst = true)
        {
            self = (this as IBindableProperty);
            self.Value = value;
            mEventName = eventName;

            if (runInFirst)
            {
                (EventManager.Instance as EventManager).AddBindablePropertyCache(eventName, this);
            }
        }
        #endregion

        #region Fun
        private void Synchronize(bool trigger)
        {
            if (!trigger) return;

            EventManager.Instance.Trigger<T>(mEventName, Value);
        }
        #endregion
    }
}
