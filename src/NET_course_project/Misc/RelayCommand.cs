using System;
using System.Threading.Tasks;
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
        private bool _isAsync;

        public RelayCommand(Action<object> execute, bool isAsync = false, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
            _isAsync = isAsync;
        }

        public bool CanExecute(object param) => _canExecute == null || _canExecute(param);
        public void Execute(object param)
        {
            if (_isAsync)
                Task.Factory.StartNew(new Action(() => _execute(param)));
            else
                _execute(param);
        }
    }
}
