using System;
using NET_course_project.Misc;

namespace NET_course_project.Model
{
    /// <summary>
    /// Проміжний клас між справою та проектом. Реалізує зв'язок багато-до-багатьох.
    /// </summary>
    public class ToDo_Project : Observable
    {
        public int Id { get; set; }

        private int _toDoId;
        public int ToDoId
        {
            get => _toDoId;
            set
            {
                _toDoId = value;
                OnPropertyChanged("ToDoId");
            }
        }

        private int _projectId;
        public int ProjectId
        {
            get => _projectId;
            set
            {
                _projectId = value;
                OnPropertyChanged("ProjectId");
            }
        }

        public virtual ToDo ToDo { get; set; }
        public virtual Project Project { get; set; }
    }
}
