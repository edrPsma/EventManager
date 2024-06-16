using System;
using System.Collections.Generic;
using System.Linq;

namespace Ev
{
    internal sealed class EventSource : IEventSource
    {
        public string SourceName { get; private set; }
        public int EventCount => _eventExcuteQueue.Count;

        Dictionary<object, IRegisterations> _eventDir;
        Queue<EventArgs> _eventExcuteQueue;

        public EventSource(string name)
        {
            _eventDir = new Dictionary<object, IRegisterations>();
            _eventExcuteQueue = new Queue<EventArgs>();
            SourceName = name;
        }

        public void Update()
        {
            lock (_eventExcuteQueue)
            {
                if (_eventExcuteQueue.Count > 0)
                {
                    EventArgs args = _eventExcuteQueue.Dequeue();
                    lock (_eventDir)
                    {
                        IRegisterations registerations;
                        if (_eventDir.TryGetValue(args.EventName, out registerations))
                        {
                            registerations.Trigger(args.Args);
                        }
                    }
                }
            }
        }

        public IUnRegister Register<TEvent>(OnEvent<TEvent> onEvent) where TEvent : new()
        {
            Type eventName = typeof(TEvent);
            return Register(eventName, onEvent);
        }

        public IUnRegister Register<TEvent>(object eventName, OnEvent<TEvent> onEvent)
        {
            IRegisterations registerations;

            lock (_eventDir)
            {
                if (!_eventDir.TryGetValue(eventName, out registerations))
                {
                    registerations = new Registerations<TEvent>();
                    _eventDir.Add(eventName, registerations);
                }
            }

            if (registerations == null)
            {
                throw new DuplicateEventNameException(eventName.ToString());
            }
            registerations.Add(onEvent);

            return new UnRegister<TEvent>(this, eventName, onEvent);
        }

        public void Trigger<TEvent>() where TEvent : new()
        {
            Trigger(new TEvent());
        }

        public void Trigger<TEvent>(TEvent e)
        {
            Type eventName = typeof(TEvent);
            Trigger(eventName, e);
        }

        public void Trigger<TEvent>(object eventName, TEvent e)
        {
            lock (_eventExcuteQueue)
            {
                EventArgs args = new EventArgs(eventName, e);
                if (!_eventExcuteQueue.Contains(args))
                {
                    _eventExcuteQueue.Enqueue(args);
                }
            }
        }

        public void TriggerNow<TEvent>() where TEvent : new()
        {
            TriggerNow(new TEvent());
        }

        public void TriggerNow<TEvent>(TEvent e)
        {
            Type eventName = typeof(TEvent);
            TriggerNow(eventName, e);
        }

        public void TriggerNow<TEvent>(object eventName, TEvent e)
        {
            IRegisterations registerations;
            if (_eventDir.TryGetValue(eventName, out registerations))
            {
                registerations.Trigger(e);
            }
        }

        public void UnRegister<TEvent>(OnEvent<TEvent> onEvent)
        {
            var eventName = typeof(TEvent);
            UnRegister(eventName, onEvent);
        }

        public void UnRegister<TEvent>(object eventName, OnEvent<TEvent> onEvent)
        {
            IRegisterations registerations;

            lock (_eventDir)
            {
                if (_eventDir.TryGetValue(eventName, out registerations))
                {
                    registerations.Remove(onEvent);

                    if (registerations.Count <= 0)
                    {
                        _eventDir.Remove(eventName);
                    }
                }
            }
        }

        public void UnRegisterAll<TEvent>()
        {
            Type eventName = typeof(TEvent);
            UnRegisterAll<TEvent>(eventName);
        }

        public void UnRegisterAll<TEvent>(object eventName)
        {
            lock (_eventDir)
            {
                _eventDir.Remove(eventName);
            }
        }

        public object[] GetAllEventName()
        {
            return _eventDir.Keys.ToArray();
        }

        public void Clear()
        {
            lock (_eventDir)
            {
                _eventDir.Clear();
            }
            lock (_eventExcuteQueue)
            {
                _eventExcuteQueue.Clear();
            }
        }
    }
}
