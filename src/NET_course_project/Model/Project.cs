﻿using System;
using System.Collections.Generic;

namespace NET_course_project.Model
{
    /// <summary>
    /// Клас, що описує проект (групу справ).
    /// </summary>
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<ToDo_Project> ToDos_Projects { get; set; }


        public Project()
        {
            ToDos_Projects = new List<ToDo_Project>();
        }
    }
}