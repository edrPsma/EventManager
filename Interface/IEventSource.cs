using System;

namespace EG.Event
{
    public interface IEventSource
    {
        string SourceName { get; }
        /// <summary>
        /// 触发事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        void Trigger<TEvent>() where TEvent : new();
        /// <summary>
        /// 触发事件
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <param name="e">事件实例</param>
        void Trigger<TEvent>(TEvent e);
        /// <summary>
        /// 触发事件，用于基础数据类型
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="e">事件实例</param>
        /// <typeparam name="TEvent">事件类型</typeparam>
        void Trigger<TEvent>(object eventName, TEvent e);

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="onEvent">事件触发后的回调方法</param>
        /// <returns></returns>
        IUnRegister Register<TEvent>(Action<TEvent> onEvent) where TEvent : new();
        /// <summary>
        /// 注册事件，用于简单事件，即参数只有基础类型的事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="onEvent">事件触发后回调方法</param>
        /// <typeparam name="TEvent">事件类型，此处为基础数据类型</typeparam>
        /// <returns></returns>
        IUnRegister Register<TEvent>(object eventName, Action<TEvent> onEvent);

        /// <summary>
        /// 注销事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="onEvent">事件触发后的回调方法</param>
        void UnRegister<TEvent>(Action<TEvent> onEvent);

        /// <summary>
        /// 注销事件，用于简单事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="onEvent">事件触发后的回调方法</param>
        /// <typeparam name="TEvent">事件类型，此处为基础数据类型</typeparam>
        void UnRegister<TEvent>(object eventName, Action<TEvent> onEvent);

        void AddBindablePropertyCache(object eventName, IBindableProperty bindableProperty);
    }
}
