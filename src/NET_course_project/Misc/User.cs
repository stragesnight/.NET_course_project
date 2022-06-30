using System;

namespace NET_course_project.Misc
{
    public class User : Observable
    {
        private string _login = null;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        private string _password = null;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
    }
}
