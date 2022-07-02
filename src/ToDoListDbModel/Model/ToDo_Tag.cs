using System;
using ToDoListCommon.Misc;

namespace ToDoListCommon.Model
{
    /// <summary>
    /// Проміжний клас між справою та тегом. Реалізує зв'язок багато-до-багатьох.
    /// </summary>
    public class ToDo_Tag : Observable
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

        private int _tagId;
        public int TagId
        {
            get => _tagId;
            set
            {
                _tagId = value;
                OnPropertyChanged("TagId");
            }
        }

        public virtual ToDo ToDo { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
