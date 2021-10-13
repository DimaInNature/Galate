using System;
using System.Windows.Input;

namespace Galate.Services.Command
{
    public sealed class DelegateCommandService : ICommand
    {
        private Action<object> _execute;

        private Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => (_canExecute != null) ? _canExecute(parameter) : true;
        
        public void Execute(object parameter)
        {
            _execute?.Invoke(parameter);
        }

        public DelegateCommandService(Action<object> executeAction) : this(executeAction, null) => _execute = executeAction;

        public DelegateCommandService(Action<object> executeAction, Func<object, bool> canExecuteFunc)
        {
            _canExecute = canExecuteFunc;
            _execute = executeAction;
        }
    }
}
