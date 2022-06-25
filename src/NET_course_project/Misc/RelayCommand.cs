using System;
using System.Windows.Input;

namespace NET_course_project.Misc
{
    /// <summary>
    /// Клас, що реалізує інтерфейс ICommand.
    /// Відповідальний за зберігання та виконання методів-команд.
    /// </summary>
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object param) => _canExecute == null || _canExecute(param);
        public void Execute(object param) => _execute?.Invoke(param);
    }
}
