using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;

namespace TextAnalyzer.Client.ViewModels.Commands
{
    public class Command<T> : ICommand
    {
        private int _busy;
        private readonly Func<T, bool> _canExecute;
        private readonly Action<T> _action;

        public Command(Action<T> action, Func<T, bool> canExecute = null)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return CanExecuteInternal(parameter);
        }

        public void Execute(object parameter)
        {
            if (!CanExecuteInternal(parameter))
            {
                return;
            }

            int storedBusy = Interlocked.CompareExchange(ref _busy, 1, 0);
            if (storedBusy == 1)
            {
                return;
            }


            try
            {
                _action.Invoke((T)parameter);
            }
            finally
            {
                Interlocked.Exchange(ref _busy, 0);
                RaiseCanExecuteChanged();
            }
        }

        protected bool CanExecuteInternal(object parameter)
        {
            int storedBusy = Interlocked.CompareExchange(ref _busy, 1, 1);
            if (storedBusy == 1)
            {
                return false;
            }

            bool canExecute = _canExecute?.Invoke((T)parameter) ?? true;
            return canExecute;
        }

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        [DebuggerNonUserCode]
        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }
    }

    public class Command : Command<object>
    {
        public Command(Action<object> action, Func<object, bool> canExecute = null) : base(action, canExecute)
        {
        }

        public Command(Action execute) : this(data => execute())
        {
        }
    }
}
