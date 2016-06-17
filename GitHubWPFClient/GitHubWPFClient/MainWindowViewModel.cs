using GitHubApiWrapper;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace GitHubWPFClient
{
    public class MainWindowViewModel : BaseVM
    {
        private string _userName;
        private IUser _user;
        private ObservableCollection<IRepository> _repositories;
        private string _statusMessage;

        private readonly IGitHubWrapper _wrapper;

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

        public IUser User
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

        public ObservableCollection<IRepository> Repositories
        {
            get { return _repositories; }
            set
            {
                if(_repositories != value)
                {
                    _repositories = value;
                    OnPropertyChanged(nameof(Repositories));
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

        public MainWindowViewModel(IGitHubWrapper wrapper)
        {
            if (wrapper == null)
            {
                throw new ArgumentNullException(nameof(wrapper));
            }

            _wrapper = wrapper;
        }

        private void GetUser()
        {
            if (!string.IsNullOrWhiteSpace(_userName))
            {
                try
                {
                    User = _wrapper.GetUser(_userName);
                    Repositories = new ObservableCollection<IRepository>(_wrapper.GetRepositoriesForUser(_userName));
                    StatusMessage = string.Empty;
                }
                catch (ArgumentException)
                {
                    User = null;
                    Repositories = null;
                    StatusMessage = "Invalid username";
                }
            }
        }

        private void Exit()
        {
            Environment.Exit(0);
        }
    }
}
