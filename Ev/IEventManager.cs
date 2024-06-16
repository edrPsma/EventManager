using System;

namespace Ev
{
    public interface IEventManager : IEventSource
    {
        /// <summary>
        /// 创建或获取自定义事件源
        /// </summary>
        /// <param name="key">事件源名</param>
        /// <returns>事件源</returns>
        IEventSource CreateOrGetEvenntSource(object key);

        /// <summary>
        /// 删除事件源
        /// </summary>
        /// <param name="key">事件源名</param>
        void RemoveEventSource(object key);

        /// <summary>
        /// 设置默认事件源
        /// </summary>
        /// <param name="source">事件源</param>
        void SetDefultEventSource(IEventSource source);
    }
}
