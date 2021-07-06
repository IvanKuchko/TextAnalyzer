using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TextAnalyzer.Client.ViewModels.Commands
{
    public class AsyncCommand : AsyncCommand<object>
    {
        [DebuggerNonUserCode]
        public AsyncCommand(Func<Task> action, Func<bool> canExecute = null, CancellationToken token =  default)
            : base(_ => action(), canExecute == null ? null as Func<object, bool> : _ => canExecute(), token)
        {
        }
    }

    public class AsyncCommand<T> : ICommand
    {
        private readonly Func<T, Task> _action;
        private readonly Func<T, bool> _canExecute;
        private readonly CancellationToken _token;
        private int _busy;

        public AsyncCommand(Func<T, Task> action, Func<T, bool> canExecute = null, CancellationToken token = default)
        {
            _action = action;
            _canExecute = canExecute;
            _token = token;
        }

        public event EventHandler CanExecuteChanged;

        [DebuggerNonUserCode]
        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        [DebuggerNonUserCode]
        public async Task ExecuteAsync(object parameter)
        {
            if (!CanExecuteInternal(parameter) && _token.IsCancellationRequested)
            {
                return;
            }

            int storedBusy = Interlocked.CompareExchange(ref _busy, 1, 0);
            if (storedBusy == 1)
            {
                return;
            }
            RaiseCanExecuteChanged();


            try
            {
                await _action((T)parameter);
            }
            finally
            {
                Interlocked.Exchange(ref _busy, 0);
                RaiseCanExecuteChanged();
            }
        }

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecuteInternal(parameter);
        }

        async void ICommand.Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        private bool CanExecuteInternal(object parameter)
        {
            if (_token.IsCancellationRequested) return false;

            int storedBusy = Interlocked.CompareExchange(ref _busy, 1, 1);
            if (storedBusy == 1)
            {
                return false;
            }

            bool canExecute = _canExecute?.Invoke((T)parameter) ?? true;
            return canExecute;
        }
    }
}
