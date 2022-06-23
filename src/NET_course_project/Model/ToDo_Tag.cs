using System;

namespace NET_course_project.Model
{
    /// <summary>
    /// Проміжний клас між справою та тегом. Реалізує зв'язок багато-до-багатьох.
    /// </summary>
    public class ToDo_Tag
    {
        public int Id { get; set; }
        public int ToDoId { get; set; }
        public int TagId { get; set; }

        public virtual ToDo ToDo { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
