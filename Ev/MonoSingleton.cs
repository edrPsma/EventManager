using System;
using UnityEngine;

namespace Ev.Utils
{
    public class MonoSingleton<I, T> : MonoBehaviour where T : MonoBehaviour, I where I : class
    {
        private static I instance = null;
        public static I Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject(typeof(T).Name);
                        instance = obj.AddComponent<T>();
                    }
                    (instance as MonoSingleton<I, T>).Init();
                }
                return instance;
            }
        }
        protected static bool _initDone;

        public void Init()
        {
            if (_initDone) return;

            _initDone = true;
            OnInit();
        }

        protected virtual void OnInit() { }
    }
}
