using System;
using System.Data.Entity;
using ToDoListCommon.Model;

namespace ToDoListCommon.Repository
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


        public ToDoListDbContext(string connStr)
        {
            Database.Connection.ConnectionString = connStr;
            //Database.SetInitializer(new ToDoListDbInitializer());
        }
    }
}
