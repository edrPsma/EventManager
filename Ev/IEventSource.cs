using System;

namespace Ev
{
    /// <summary>
    /// 事件源接口
    /// </summary>
    public interface IEventSource
    {
        /// <summary>
        /// 事件源名称
        /// </summary>
        /// <value>事件源名称</value>
        string SourceName { get; }
        /// <summary>
        /// 正在处理的事件数量
        /// </summary>
        /// <value>正在处理的事件数量</value>
        int EventCount { get; }
        /// <summary>
        /// 事件轮询
        /// </summary>
        void Update();
        /// <summary>
        /// 触发事件，在下一帧执行，线程安全
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        void Trigger<TEvent>() where TEvent : new();
        /// <summary>
        /// 触发事件，在下一帧执行，线程安全
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <param name="e">事件实例</param>
        void Trigger<TEvent>(TEvent e);
        /// <summary>
        /// 触发事件，在下一帧执行，线程安全
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="e">事件实例</param>
        /// <typeparam name="TEvent">事件类型</typeparam>
        void Trigger<TEvent>(object eventName, TEvent e);

        /// <summary>
        /// 立即触发事件，线程不安全
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        void TriggerNow<TEvent>() where TEvent : new();
        /// <summary>
        /// 立即触发事件，线程不安全
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <param name="e">事件实例</param>
        void TriggerNow<TEvent>(TEvent e);
        /// <summary>
        /// 立即触发事件，线程不安全
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="e">事件实例</param>
        /// <typeparam name="TEvent">事件类型</typeparam>
        void TriggerNow<TEvent>(object eventName, TEvent e);

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="onEvent">事件触发后的回调方法</param>
        /// <returns></returns>
        IUnRegister Register<TEvent>(OnEvent<TEvent> onEvent) where TEvent : new();
        /// <summary>
        /// 注册事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventName">事件名</param>
        /// <param name="onEvent">事件触发后回调方法</param>
        /// <returns></returns>
        IUnRegister Register<TEvent>(object eventName, OnEvent<TEvent> onEvent);

        /// <summary>
        /// 注销事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="onEvent">事件触发后的回调方法</param>
        void UnRegister<TEvent>(OnEvent<TEvent> onEvent);

        /// <summary>
        /// 注销事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="onEvent">事件触发后的回调方法</param>
        /// <typeparam name="TEvent">事件类型</typeparam>
        void UnRegister<TEvent>(object eventName, OnEvent<TEvent> onEvent);

        /// <summary>
        /// 注销所有注册的事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        void UnRegisterAll<TEvent>();

        /// <summary>
        /// 注销所有注册的事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <typeparam name="TEvent">事件类型</typeparam>
        void UnRegisterAll<TEvent>(object eventName);

        /// <summary>
        /// 获取注册的所有事件名
        /// </summary>
        /// <returns>事件名数组</returns>
        object[] GetAllEventName();

        /// <summary>
        /// 清空订阅的事件
        /// </summary>
        void Clear();
    }
}
