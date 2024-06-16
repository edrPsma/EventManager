using System;

namespace Ev
{
    /// <summary>
    /// 事件注销器
    /// </summary>
    /// <typeparam name="TEvent">事件类型</typeparam>
    internal struct UnRegister<TEvent> : IUnRegister
    {
        /// <summary>
        /// 事件接口
        /// </summary>
        public IEventSource Source;
        /// <summary>
        /// 回调委托
        /// </summary>
        public OnEvent<TEvent> OnEvent;

        /// <summary>
        /// 事件名
        /// </summary>
        public object EventName { get; set; }

        public UnRegister(IEventSource source, object eventName, OnEvent<TEvent> onEvent)
        {
            Source = source;
            EventName = eventName;
            OnEvent = onEvent;
        }

        void IUnRegister.UnRegister()
        {
            Source?.UnRegister(EventName, OnEvent);
            Source = null;
            OnEvent = null;
        }
    }
}
