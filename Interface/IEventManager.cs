using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EG.Event
{
    public interface IEventManager
    {
        /// <summary>
        /// 创建或获取事件源，使用Type作为key值
        /// </summary>
        /// <typeparam name="TSource">事件源类型</typeparam>
        /// <returns>事件源</returns>
        IEventSource CreateOrGetEvenntSource<TSource>();

        /// <summary>
        /// 创建或获取事件源
        /// </summary>
        /// <param name="key">事件源编号</param>
        /// <returns>事件源</returns>
        IEventSource CreateOrGetEvenntSource(object key);
    }

}