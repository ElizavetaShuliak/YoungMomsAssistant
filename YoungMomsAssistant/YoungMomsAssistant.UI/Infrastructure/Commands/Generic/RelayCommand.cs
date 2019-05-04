using System;
using System.Windows.Input;

namespace YoungMomsAssistant.UI.Infrastructure.Commands.Generic {
    class RelayCommand<T> : ICommand {

        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null) {
            _execute = execute ?? throw new NullReferenceException("execute");
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) {
            if (parameter != null && parameter.GetType() != typeof(T)) {
                throw new ArgumentException("parameter");
            }

            return _canExecute?.Invoke((T)parameter) ?? true;
        }

        public void Execute(object parameter) {
            if (parameter != null && parameter.GetType() != typeof(T)) {
                throw new ArgumentException("parameter");
            }

            _execute?.Invoke((T)parameter);
        }
    }
}
