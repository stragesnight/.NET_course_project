using System;
using System.Linq;
using System.Data.Entity;
using NET_course_project.Model;

namespace NET_course_project.Repository
{
    /// <summary>
    /// Клас-ініціалізатор для контексту бази даних.
    /// </summary>
    public class ToDoListDbInitializer : DropCreateDatabaseIfModelChanges<ToDoListDbContext>
    {
        private static readonly int _nEntries = 10;

        private void AddTestPriorities(ToDoListDbContext context)
        {
            context.Priorities.Add(new Priority { NumPriority = 0, Title = "Low" });
            context.Priorities.Add(new Priority { NumPriority = 10, Title = "High" });

            context.SaveChanges();
        }

        private void AddTestProjects(ToDoListDbContext context)
        {
            for (int i = 1; i <= _nEntries / 2; ++i)
                context.Projects.Add(new Project { Title = $"Project{i}"});

            context.SaveChanges();
        }

        private void AddTestTags(ToDoListDbContext context)
        {
            for (int i = 1; i <= _nEntries / 5; ++i)
                context.Tags.Add(new Tag { Title = $"Tag{i}" });

            context.SaveChanges();
        }

        private void AddTestToDos(ToDoListDbContext context)
        {
            int pCount = context.Priorities.Count();
            for (int i = 1; i <= _nEntries; ++i)
            {
                context.ToDos.Add(new ToDo {
                    DueTo = DateTime.Today,
                    Title = $"ToDoTitle{i}",
                    Description = $"ToDoDescription{i}",
                    PriorityId = (i % pCount) + 1
                });
            }

            context.SaveChanges();
        }

        private void AddTestToDos_Projects(ToDoListDbContext context)
        {
            int nToDos = context.ToDos.Count();
            int nProjs = context.Projects.Count();

            int j = 1;
            for (int i = 1; i <= nProjs; ++i)
            {
                for (int x = 0; x < 2; ++x, ++j)
                    context.ToDos_Projects.Add(new ToDo_Project { ToDoId = j, ProjectId = i });
            }

            context.SaveChanges();
        }

        private void AddTestToDos_Tags(ToDoListDbContext context)
        {
            int nToDos = context.ToDos.Count();
            int nTags = context.Tags.Count();

            for (int i = 1; i <= nToDos; ++i)
            {
                for (int j = 1; j <= nTags; ++j)
                    context.ToDos_Tags.Add(new ToDo_Tag { ToDoId = i, TagId = j });
            }

            context.SaveChanges();
        }

        protected override void Seed(ToDoListDbContext context)
        {
#if DEBUG
            if (context.ToDos.Count() < 1)
            {
                AddTestTags(context);
                AddTestPriorities(context);
                AddTestToDos(context);
                AddTestProjects(context);
                AddTestToDos_Projects(context);
                AddTestToDos_Tags(context);
            }
#endif
            base.Seed(context);
        }
    }
}
