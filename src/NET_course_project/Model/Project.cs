using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NET_course_project.Misc;

namespace NET_course_project.Model
{
    /// <summary>
    /// Клас, що описує проект (групу справ).
    /// </summary>
    public class Project : Observable
    {
        public int Id { get; set; }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public virtual ICollection<ToDo> ToDos { get; set; }

        public Project()
        {
            ToDos = new ObservableCollection<ToDo>();
        }
    }
}
