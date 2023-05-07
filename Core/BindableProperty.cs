using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EG.Event
{
    public class BindableProperty<T> where T : IEquatable<T>
    {
        #region 字段
        private T mValue = default(T);
        private readonly object mEventName;
        #endregion

        #region 属性
        /// <summary>
        /// 值
        /// </summary>
        public T Value
        {
            get => mValue;
            set
            {
                if (!value.Equals(mValue))
                {
                    mValue = value;
                    Synchronize(Synchronization);
                }
            }
        }

        public bool Synchronization { get; set; } = true;
        #endregion

        #region 构造函数
        public BindableProperty(object eventName, T value, bool runInFirst = true)
        {
            mValue = value;
            mEventName = eventName;

            if (runInFirst)
            {
                (EventManager.Instance as EventManager).AddBindablePropertyCache(eventName, value);
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
