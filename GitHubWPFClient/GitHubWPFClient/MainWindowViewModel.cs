using GitHubApiWrapper;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace GitHubWPFClient
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _userName;
        private User _user;
        private string _statusMessage;

        IGitHubWrapper _wrapper;

        private ICommand _getUserCommand;
        private ICommand _exitCommand;

        public string UserName
        {
            get { return _userName; }
            set
            {
                if(_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }

        public User User
        {
            get { return _user; }
            set
            {
                if (_user != value)
                {
                    _user = value;
                    OnPropertyChanged(nameof(User));
                }
            }
        }

        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                if (_statusMessage != value)
                {
                    _statusMessage = value;
                    OnPropertyChanged(nameof(StatusMessage));
                }
            }
        }

        public ICommand GetUserCommand
        {
            get
            {
                if(_getUserCommand == null)
                {
                    _getUserCommand = new RelayCommand(param => GetUser(), param => true);
                }

                return _getUserCommand;
            }
        }

        public ICommand ExitCommand
        {
            get
            {
                if(_exitCommand == null)
                {
                    _exitCommand = new RelayCommand(param => Exit(), param => true);
                }

                return _exitCommand;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel(IGitHubWrapper wrapper)
        {
            _wrapper = wrapper;
        }

        private void GetUser()
        {
            if (!string.IsNullOrWhiteSpace(_userName))
            {
                try
                {
                    User = _wrapper.GetUser(_userName);
                    StatusMessage = string.Empty;
                }
                catch (ArgumentException)
                {
                    User = null;
                    StatusMessage = "Invalid username";
                }
            }
        }

        private void Exit()
        {
            Environment.Exit(0);
        }

        private void OnPropertyChanged(string propertyName)
        {
            if(this.PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, e);
            }
        }
    }
}
