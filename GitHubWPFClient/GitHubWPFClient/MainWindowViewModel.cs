using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubApiWrapper;
using System.Windows.Input;

namespace GitHubWPFClient
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _userName;
        private User _user;
        private ICommand _getUserCommand;

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

        public event PropertyChangedEventHandler PropertyChanged;

        public void GetUser()
        {
            if (!string.IsNullOrWhiteSpace(_userName))
            {
                try
                {
                    var wrapper = new GitHubApiWrapper.GitHubApiWrapper();
                    User = wrapper.GetUser(_userName);
                }
                catch (ArgumentException)
                {
                    User = null;
                }
            }
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
