﻿using System;
using System.Collections.Generic;
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

        public virtual Priority Priority { get; set; }
        public virtual ICollection<ToDo_Tag> ToDos_Tags { get; set; }
        public virtual ICollection<ToDo_Project> ToDos_Projects { get; set; }


        public ToDo()
        {
            ToDos_Tags = new List<ToDo_Tag>();
            ToDos_Projects = new List<ToDo_Project>();
        }
    }
}
