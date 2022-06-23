using System;
using System.Data.Entity;

namespace NET_course_project.Repository
{
    /// <summary>
    /// Клас-ініціалізатор для контексту бази даних.
    /// </summary>
    public class ToDoListDbInitializer : DropCreateDatabaseAlways<ToDoListDbContext>
    {
        protected override void Seed(ToDoListDbContext context)
        {
            // TODO: корректно ініціалізувати базу даних
            base.Seed(context);
        }
    }
}
