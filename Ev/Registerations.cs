using System;

namespace Ev
{
    internal class Registerations<TEvent> : IRegisterations
    {
        public event OnEvent<TEvent> onEvent = default;

        public int Count { get; private set; }

        public void Add(object e)
        {
            if (e == null) return;

            onEvent += e as OnEvent<TEvent>;
            Count++;
        }

        public void Remove(object e)
        {
            if (e == null) return;

            onEvent -= e as OnEvent<TEvent>;
            Count--;
        }

        public void Trigger(object e)
        {
            onEvent.Invoke((TEvent)e);
        }

        public void Trigger<T>(T e) where T : TEvent
        {
            onEvent.Invoke(e);
        }
    }
}
