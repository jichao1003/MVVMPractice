using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMDemo2
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;   // 需要执行的操作（命令体）
        private readonly Func<bool> _canExecute;    // 命令是否可以执行的逻辑

        public RelayCommand(Action action, Func<bool> canExecute)
        {
            _execute = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)    // ICommand接口方法之一，用于判断命令是否可以执行
        {
            if (_canExecute == null)
            {
                return true;	// 命令始终可以执行
            }
            else
            {
                return _canExecute();	// 调用 _canExecute() 获取判断结果
            }
        }

        public void Execute(object parameter)   // ICommand接口方法之一 用于执行命令体，调用 _execute 所存储的操作
        {
            _execute?.Invoke();
        }

        public event EventHandler CanExecuteChanged	// ICommad接口中的事件，当命令的可执行状态发生变化时，触发此事件来通知界面元素更新
        {
            add
            {
                if (_canExecute != null) { CommandManager.RequerySuggested += value; }
            }
            remove
            {
                if (_canExecute != null) { CommandManager.RequerySuggested -= value; }
            }
        }
    }
}
