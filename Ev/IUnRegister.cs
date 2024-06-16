using System;

namespace Ev
{
    /// <summary>
    /// 事件注销器接口
    /// </summary>
    public interface IUnRegister
    {
        object EventName { get; }
        /// <summary>
        /// 注销事件
        /// </summary>
        void UnRegister();
    }
}
