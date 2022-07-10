using System;
using System.Windows;
using NET_course_project.Misc;
using ToDoListCommon.Misc;
using ToDoListCommon.Repository;

namespace NET_course_project.ViewModel
{
    /// <summary>
    /// Модель представлення вікна авторизації.
    /// Реалізує метод для безпоередньої авторизації користувача в системі.
    /// </summary>
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

        private bool _shouldClose = false;
        public bool ShouldClose
        {
            get => _shouldClose;
            set
            {
                _shouldClose = value;
                OnPropertyChanged("ShouldClose");
            }
        }

        private bool _isNotBusy = true;
        public bool IsNotBusy
        {
            get => _isNotBusy;
            set
            {
                _isNotBusy = value;
                OnPropertyChanged("IsNotBusy");
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
            (_authorize= new RelayCommand(x => HandleAuthorize(), true));


        public AuthorizationWindowViewModel()
        {
            CreatedUser = new User();
        }

        private void HandleAuthorize()
        {
            IsNotBusy = false;

            if (DbRepository.Initialize(CreatedUser))
            {
                Application.Current.Dispatcher.Invoke(new Action(() => {
                    MainWindow window = new MainWindow(CreatedUser);
                    window.Show();
                    ShouldClose = true;
                }));
            }

            IsNotBusy = true;
        }
    }
}
