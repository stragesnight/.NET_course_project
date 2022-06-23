using System;
using System.Collections.Generic;

namespace NET_course_project.Model
{
    // Клас, що описує теги для справ.
    public class Tag
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<ToDo_Tag> ToDos_Tags { get; set; }


        public Tag()
        {
            ToDos_Tags = new List<ToDo_Tag>();
        }
    }
}
