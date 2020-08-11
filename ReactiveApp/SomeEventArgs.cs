using System;

namespace ReactiveApp
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