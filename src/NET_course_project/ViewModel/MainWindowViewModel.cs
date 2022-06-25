using System;
using NET_course_project.Model;
using NET_course_project.Repository;

namespace NET_course_project.ViewModel
{
    public class MainWindowViewModel
    {
        public ToDoListDbContext DbContext { get; private set; } = null;
        public ToDo SelectedToDo { get; set; } = null;


        public MainWindowViewModel()
        {
            DbContext = new ToDoListDbContext();
        }
    }
}
