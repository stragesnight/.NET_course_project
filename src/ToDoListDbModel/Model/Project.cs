using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ToDoListCommon.Misc;

namespace ToDoListCommon.Model
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

        public override string ToString() => Title;
    }
}
