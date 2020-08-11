using System;
using System.Collections.ObjectModel;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Input;

namespace ReactiveApp
{
    public class MainViewModel : IDisposable
    {
        private readonly IScheduler _mainThreadScheduler;
        private readonly ISomeService _someService;
        private IDisposable _currentQuerySubscription = Disposable.Empty;

        public MainViewModel(IScheduler mainThreadScheduler, ISomeService someService)
        {
            _mainThreadScheduler = mainThreadScheduler;
            _someService = someService;
            ExecuteQuery = new RelayCommand(() =>
            {
                _currentQuerySubscription.Dispose();
                QueryResults.Clear();
                _currentQuerySubscription = _someService.ExecuteSomeQuery().ObserveOn(_mainThreadScheduler).Subscribe(it => QueryResults.Add(it));
            });
        }

        public ObservableCollection<string> QueryResults { get; } = new ObservableCollection<string>();

        public ICommand ExecuteQuery { get; }

        public void Dispose()
        {
            _currentQuerySubscription.Dispose();
        }
    }
}