using System;

namespace ReactiveConsole
{
    public class ClassWithEvent
    {
        public event EventHandler<SomeEventArgs> SomeEvent;

        public void TriggerEvent(string value) => SomeEvent?.Invoke(this, new SomeEventArgs(value));
    }
}