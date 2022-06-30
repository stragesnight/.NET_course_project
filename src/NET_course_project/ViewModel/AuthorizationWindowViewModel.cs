using System;
using System.Windows;
using NET_course_project.Misc;
using NET_course_project.Repository;

namespace NET_course_project.ViewModel
{
    public class AuthorizationWindowViewModel : Observable, IDialog 
    {
        private User _createdUser = null;
        public User CreatedUser
        {
            get => _createdUser;
            set
            {
                _createdUser= value;
                OnPropertyChanged("CreatedUser");
            }
        }

        public object InitialState { get; set; }
        public object ResultState
        {
            get => CreatedUser;
            set { if (value is User) CreatedUser = value as User; }
        }


        private RelayCommand _authorize = null;
        public RelayCommand AuthorizeCommand => _authorize ??
            (_authorize= new RelayCommand(x => HandleAuthorize()));


        public AuthorizationWindowViewModel()
        {
            CreatedUser = new User();
        }

        private void HandleAuthorize()
        {
            if (DbRepository.Initialize(CreatedUser))
                new MainWindow().Show();
        }
    }
}
