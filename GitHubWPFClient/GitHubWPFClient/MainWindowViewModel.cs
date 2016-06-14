using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubApiWrapper;

namespace GitHubWPFClient
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        //private Command GetUserCommand;

        private string _userName;
        private User _user;

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

        public event PropertyChangedEventHandler PropertyChanged;

        public void GetUser()
        {
            if (!string.IsNullOrWhiteSpace(_userName))
            {
                try
                {
                    var wrapper = new GitHubApiWrapper.GitHubApiWrapper();
                    _user = wrapper.GetUser(_userName);
                }
                catch (ArgumentException)
                {
                    _user = null;
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
