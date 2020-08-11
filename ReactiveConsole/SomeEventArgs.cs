using System;

namespace ReactiveConsole
{
    public class SomeEventArgs : EventArgs
    {
        public SomeEventArgs(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}