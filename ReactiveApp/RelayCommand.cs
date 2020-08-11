﻿using System;
using System.Windows.Input;

namespace ReactiveApp
{
    public class RelayCommand : ICommand
    {
        private readonly Action action;

        public RelayCommand(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => action();

        public event EventHandler CanExecuteChanged;
    }
}