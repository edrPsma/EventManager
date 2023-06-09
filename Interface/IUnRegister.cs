using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EG
{
    /// <summary>
    /// 事件注销器接口
    /// </summary>
    public interface IUnRegister
    {
        object EventName { get; set; }
        /// <summary>
        /// 注销事件
        /// </summary>
        void UnRegister();
    }
}
