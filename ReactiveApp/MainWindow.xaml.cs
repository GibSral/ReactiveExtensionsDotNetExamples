﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ReactiveApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainViewModel(new DispatcherScheduler(Dispatcher.CurrentDispatcher), new SomeService());
            DataContext = ViewModel;
        }

        public MainViewModel ViewModel { get; }
    }
}
