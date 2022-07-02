using System;
using System.Linq;
using System.Data.Entity;
using ToDoListCommon.Model;

namespace ToDoListCommon.Repository
{
    /// <summary>
    /// Клас-ініціалізатор для контексту бази даних.
    /// </summary>
    public class ToDoListDbInitializer : DropCreateDatabaseAlways<ToDoListDbContext>
    {
        private static readonly int _nEntries = 10;

        private void AddTestPriorities(ToDoListDbContext context)
        {
            context.Priorities.Add(new Priority { NumPriority = 0, Title = "Low" });
            context.Priorities.Add(new Priority { NumPriority = 5, Title = "Average" });
            context.Priorities.Add(new Priority { NumPriority = 10, Title = "High" });

            context.SaveChanges();
        }

        private void AddTestProjects(ToDoListDbContext context)
        {
            context.Projects.Add(new Project { Title = "Your first project"});
            context.SaveChanges();
        }

        private void AddTestTags(ToDoListDbContext context)
        {
            context.Tags.Add(new Tag { Title = $"Important!" });
            context.SaveChanges();
        }

        private void AddTestToDos(ToDoListDbContext context)
        {
            context.ToDos.Add(new ToDo {
                DueTo = DateTime.Today,
                Title = $"Your first to-do!",
                Description = $"Congratulations on setting up this application. Good luck!",
                PriorityId = 3,
                ProjectId = 1,
                Completed = false
            });

            context.SaveChanges();
        }

        private void AddTestToDos_Tags(ToDoListDbContext context)
        {
            context.ToDos_Tags.Add(new ToDo_Tag { ToDoId = 1, TagId = 1 });
            context.SaveChanges();
        }

        protected override void Seed(ToDoListDbContext context)
        {
            AddTestTags(context);
            AddTestPriorities(context);
            AddTestProjects(context);
            AddTestToDos(context);
            AddTestToDos_Tags(context);

            base.Seed(context);
        }
    }
}
