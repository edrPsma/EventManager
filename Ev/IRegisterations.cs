using System;

namespace Ev
{
    /// <summary>
    /// 消息注册接口
    /// </summary>
    internal interface IRegisterations
    {
        int Count { get; }
        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="onEvent">回调</param>
        void Add(object onEvent);

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="onEvent">回调</param>
        void Remove(object onEvent);

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="e">事件实例</param>
        void Trigger(object e);
    }
}
