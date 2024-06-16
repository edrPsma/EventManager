using System;
using System.Collections.Generic;
using Ev.Utils;

namespace Ev
{
    public sealed class EventManager : MonoSingleton<IEventManager, EventManager>, IEventManager
    {
        Dictionary<object, IEventSource> _sourceDic = new Dictionary<object, IEventSource>();
        IEventSource _defultSource;
        public string SourceName => _defultSource.SourceName;
        public int EventCount => _defultSource.EventCount;

        protected override void OnInit()
        {
            _defultSource = new EventSource(nameof(EventManager));
        }

        public IEventSource CreateOrGetEvenntSource(object key)
        {
            lock (_sourceDic)
            {
                if (!_sourceDic.ContainsKey(key))
                {
                    IEventSource source = new EventSource(key.ToString());
                    _sourceDic.Add(key, source);
                }

                return _sourceDic[key];
            }
        }

        public void RemoveEventSource(object key)
        {
            lock (_sourceDic)
            {
                _sourceDic.Remove(key);
            }
        }

        public void Update()
        {
            lock (_sourceDic)
            {
                foreach (var item in _sourceDic)
                {
                    item.Value.Update();
                }
            }
            _defultSource.Update();
        }

        public void Trigger<TEvent>() where TEvent : new()
        {
            _defultSource.Trigger<TEvent>();
        }

        public void Trigger<TEvent>(TEvent e)
        {
            _defultSource.Trigger(e);
        }

        public void Trigger<TEvent>(object eventName, TEvent e)
        {
            _defultSource.Trigger(eventName, e);
        }

        public void TriggerNow<TEvent>() where TEvent : new()
        {
            _defultSource.TriggerNow<TEvent>();
        }

        public void TriggerNow<TEvent>(TEvent e)
        {
            _defultSource.TriggerNow(e);
        }

        public void TriggerNow<TEvent>(object eventName, TEvent e)
        {
            _defultSource.TriggerNow(eventName, e);
        }

        public IUnRegister Register<TEvent>(OnEvent<TEvent> onEvent) where TEvent : new()
        {
            return _defultSource.Register(onEvent);
        }

        public IUnRegister Register<TEvent>(object eventName, OnEvent<TEvent> onEvent)
        {
            return _defultSource.Register(eventName, onEvent);
        }

        public void UnRegister<TEvent>(OnEvent<TEvent> onEvent)
        {
            _defultSource.UnRegister(onEvent);
        }

        public void UnRegister<TEvent>(object eventName, OnEvent<TEvent> onEvent)
        {
            _defultSource.UnRegister(eventName, onEvent);
        }

        public void UnRegisterAll<TEvent>()
        {
            _defultSource.UnRegisterAll<TEvent>();
        }

        public void UnRegisterAll<TEvent>(object eventName)
        {
            _defultSource.UnRegisterAll<TEvent>(eventName);
        }

        public object[] GetAllEventName()
        {
            return _defultSource.GetAllEventName();
        }

        public void Clear()
        {
            _defultSource.Clear();
        }

        public void SetDefultEventSource(IEventSource source)
        {
            if (source == null) return;

            Clear();

            _defultSource = source;
        }
    }
}
