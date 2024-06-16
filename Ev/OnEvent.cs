using System;

namespace Ev
{
    /// <summary>
    /// 默认事件回调
    /// </summary>
    /// <param name="e"></param>
    /// <typeparam name="TEvent"></typeparam>
    public delegate void OnEvent<TEvent>(TEvent e);
}
