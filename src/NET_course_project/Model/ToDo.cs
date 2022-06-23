using System;
using System.Collections.Generic;

namespace NET_course_project.Model
{
    /// <summary>
    /// Клас, що описує конкретну справу.
    /// </summary>
    public class ToDo
    {
        public int Id { get; set; }
        public int PriorityId { get; set; }
        public DateTime DueTo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

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
