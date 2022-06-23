using System;
using System.Data.Entity;
using NET_course_project.Model;

namespace NET_course_project.Repository
{
    /// <summary>
    /// Клас, що реалізує підтримку EntityFramework.
    /// Використовується іншими елементами коду для взаємодії з базою даних.
    /// </summary>
    public class ToDoListDbContext : DbContext
    {
        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ToDo_Tag> ToDos_Tags { get; set; }
        public DbSet<ToDo_Project> ToDos_Projects { get; set; }


        // тимчасова заглушка ініціалізатора
        public ToDoListDbContext() : base("ToDoListDb")
        {
            Database.SetInitializer(new ToDoListDbInitializer());
        }
    }
}
