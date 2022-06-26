using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NET_course_project.Misc;

namespace NET_course_project.Model
{
    /// <summary>
    /// Клас, що описує конкретну справу.
    /// </summary>
    public class ToDo : Observable
    {
        public int Id { get; set; }

        private int _priorityId;
        public int PriorityId
        {
            get => _priorityId;
            set
            {
                _priorityId = value;
                OnPropertyChanged("PriorityId");
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

        private DateTime _dueTo;
        public DateTime DueTo
        {
            get => _dueTo;
            set
            {
                _dueTo = value;
                OnPropertyChanged("DueTo");
            }
        }

        private DateTime _plannedCompletionTime;
        public DateTime PlannedCompletionTime
        {
            get => _plannedCompletionTime;
            set
            {
                _plannedCompletionTime = value;
                OnPropertyChanged("PlannedCompletionTime");
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

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        private bool _completed;
        public bool Completed
        {
            get => _completed;
            set
            {
                _completed = value;
                OnPropertyChanged("Completed");
            }
        }
        public virtual Priority Priority { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<ToDo_Tag> ToDos_Tags { get; set; }


        public ToDo()
        {
            ToDos_Tags = new ObservableCollection<ToDo_Tag>();
        }
    }
}
