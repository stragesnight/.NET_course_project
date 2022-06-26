using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NET_course_project.Misc;

namespace NET_course_project.Model
{
    /// <summary>
    /// Клас, що описує можливі значення пріоритетів для справ.
    /// </summary>
    public class Priority : Observable
    {
        public int Id { get; set; }

        private int _numPriority;
        public int NumPriority
        {
            get => _numPriority;
            set
            {
                _numPriority = value;
                OnPropertyChanged("NumPriority");
            }
        }

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


        public Priority()
        {
            ToDos = new ObservableCollection<ToDo>();
        }

        public override string ToString() => Title;
    }
}
