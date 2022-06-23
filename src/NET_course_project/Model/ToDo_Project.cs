using System;
using System.Collections.Generic;

namespace NET_course_project.Model
{
    /// <summary>
    /// Проміжний клас між справою та проектом. Реалізує зв'язок багато-до-багатьох.
    /// </summary>
    public class ToDo_Project
    {
        public int Id { get; set; }
        public int ToDoId { get; set; }
        public int ProjectId { get; set; }

        public virtual ToDo ToDo { get; set; }
        public virtual Project Project { get; set; }
    }
}
