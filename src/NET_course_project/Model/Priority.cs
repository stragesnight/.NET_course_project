using System;
using System.Collections.Generic;

namespace NET_course_project.Model
{
    /// <summary>
    /// Клас, що описує можливі значення пріоритетів для справ.
    /// </summary>
    public class Priority
    {
        public int Id { get; set; }
        public int NumPriority { get; set; }
        public string Title { get; set; }

        public virtual ICollection<ToDo> ToDos { get; set; }


        public Priority()
        {
            ToDos = new List<ToDo>();
        }
    }
}
