using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ev
{
    /// <summary>
    /// 注销事件触发器
    /// </summary>
    internal class UnRegisterTrigger : MonoBehaviour
    {
        List<IUnRegister> _unregisters = new List<IUnRegister>();

        /// <summary>
        /// 添加事件注销器
        /// </summary>
        /// <param name="unRegister">事件注销器</param>
        public void AddUnRegister(IUnRegister unRegister)
        {
            _unregisters.Add(unRegister);
        }

        private void OnDestroy()
        {
            foreach (var item in _unregisters)
            {
                item.UnRegister();
            }

            _unregisters.Clear();
        }
    }

    /// <summary>
    /// 事件注销器扩展类
    /// </summary>
    public static class UnRegisterExtra
    {
        /// <summary>
        /// 在游戏物体销毁时注销事件
        /// </summary>
        /// <param name="unRegister">事件注销器</param>
        /// <param name="gameObject">游戏物体</param>
        public static void Bind(this IUnRegister unRegister, GameObject gameObject)
        {
            var trigger = gameObject.GetComponent<UnRegisterTrigger>();

            if (!trigger)
            {
                trigger = gameObject.AddComponent<UnRegisterTrigger>();
            }

            trigger.AddUnRegister(unRegister);
        }

        /// <summary>
        /// 在游戏物体销毁时注销事件
        /// </summary>
        /// <param name="unRegister">事件注销器</param>
        /// <param name="component">游戏物体</param>
        public static void Bind(this IUnRegister unRegister, Component component)
        {
            Bind(unRegister, component.gameObject);
        }

        /// <summary>
        /// 在游戏物体销毁时注销事件
        /// </summary>
        /// <param name="unRegister">事件注销器</param>
        /// <param name="mono">游戏物体</param>
        public static void Bind(this IUnRegister unRegister, MonoBehaviour mono)
        {
            Bind(unRegister, mono.gameObject);
        }
    }
}
