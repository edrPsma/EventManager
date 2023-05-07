using System;

namespace EG.Event
{
    /// <summary>
    /// 事件注销器
    /// </summary>
    /// <typeparam name="TEvent">事件类型</typeparam>
    public class UnRegister<TEvent> : IUnRegister
    {
        /// <summary>
        /// 事件接口
        /// </summary>
        public IEventManager Event;
        /// <summary>
        /// 回调委托
        /// </summary>
        public Action<TEvent> onEvent;

        public object EventName { get; set; }

        void IUnRegister.UnRegister()
        {
            Event.UnRegister<TEvent>(EventName, onEvent);
            Event = null;
            onEvent = null;
        }
    }
}
