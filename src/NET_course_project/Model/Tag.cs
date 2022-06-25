using System;
using System.Collections.Generic;
using NET_course_project.Misc;

namespace NET_course_project.Model
{
    /// <summary>
    /// Клас, що описує теги для справ.
    /// </summary>
    public class Tag : Observable
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

        public virtual ICollection<ToDo_Tag> ToDos_Tags { get; set; }


        public Tag()
        {
            ToDos_Tags = new List<ToDo_Tag>();
        }
    }
}
