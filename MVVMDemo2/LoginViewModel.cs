using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace MVVMDemo2
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private LoginModel _loginModel;
        private Login _loginView;

        public LoginViewModel(Login view)
        {
            _loginModel = new LoginModel();
            _loginView = view;
        }

        // 绑定到登录界面文本框的属性，用于获取和设置用户名和密码 
        public string UserName
        {
            get { return _loginModel.UserName; }
            set
            {
                _loginModel.UserName = value;
                OnPropertyChanged(UserName);
            }
        }

        public string Password
        {
            get { return _loginModel.Password; }
            set
            {
                _loginModel.Password = value;
                OnPropertyChanged(Password);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)	// 触发属性更改通知的方法
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoginFunc()	// 处理登录操作
        {
            if (UserName == "admin" && Password == "123456")
            {
                MainWindow main = new MainWindow();
                main.Show();
                _loginView.Close();
            }
            else
            {
                MessageBox.Show("用户名或密码错误");
                UserName = "";
                Password = "";
            }
        }

        private bool CanLoginExecute()
        {
            return true;
        }

        public ICommand LoginAction	// 绑定到登录按钮的命令属性
        {
            get
            {
                return new RelayCommand(LoginFunc, CanLoginExecute);	//（执行体，判断条件）
            }
        }
    }
}
