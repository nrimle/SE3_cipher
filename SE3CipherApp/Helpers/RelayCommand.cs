using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SE3CipherApp
{
    // Implements the ICommand interface to provide a command that can be bound to UI controls
    class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        // Event that notifies when changes occur that affect whether the command should execute
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value; 
            remove => CommandManager.RequerySuggested -= value; 
        }

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute)); 
            _canExecute = canExecute; 
        }

        // Determines if the command can execute with the given parameter
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(); 
        }

        // Executes the command with the given parameter
        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
