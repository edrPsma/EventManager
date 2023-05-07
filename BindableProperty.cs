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
                    Synchronization();
                }
            }
        }

        public object EventName { get; set; }
        #endregion

        #region 构造函数
        public BindableProperty(object eventName, T value, bool runInFirst = true)
        {
            mValue = value;
            EventName = eventName;

            if (runInFirst)
            {
                (EventManager.Instance as EventManager).AddBindablePropertyCache(eventName, value);
            }
        }
        #endregion

        #region Fun
        private void Synchronization()
        {
            EventManager.Instance.Trigger<T>(EventName, Value);
        }
        #endregion
    }
}
