using System;
using System.Windows.Input;

namespace HTOL.Command
{
    public class RelayCommand : ICommand
    {
        private Action DoExcute;
        private Func<bool> CanExcute;

        public RelayCommand(Action doexcute, Func<bool> canexcute)
        {
            DoExcute = doexcute;
            CanExcute = canexcute;
        }

        public RelayCommand(Action doexcute)
        {
            DoExcute = doexcute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return CanExcute?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            DoExcute?.Invoke();
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private Action<T> DoExcute;
        private Func<T, bool> CanExcute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<T> doexcute, Func<T, bool> canexcute)
        {
            DoExcute = doexcute;
            CanExcute = canexcute;
        }

        public RelayCommand(Action<T> doexcute)
        {
            DoExcute = doexcute;
        }

        public bool CanExecute(object parameter)
        {
            return CanExcute?.Invoke((T)parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            DoExcute?.Invoke((T)parameter);
        }
    }
}