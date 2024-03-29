﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ToDoListCommon.Misc;

namespace ToDoListCommon.Model
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

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value ?? String.Empty;
                OnPropertyChanged("Title");
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value ?? String.Empty;
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

        private Priority _priority = null;
        public virtual Priority Priority
        {
            get => _priority;
            set
            {
                _priority = value;
                OnPropertyChanged("Priority");
            }
        }

        public virtual Project Project { get; set; }

        public virtual ICollection<ToDo_Tag> ToDos_Tags { get; set; }


        public ToDo()
        {
            ToDos_Tags = new ObservableCollection<ToDo_Tag>();
        }
    }
}
