using System;
using System.Data.Entity;
using NET_course_project.Model;

namespace NET_course_project.Repository
{
    public static class DbRepository
    {
        public static ToDoListDbContext DbContext { get; private set; } = null;


        static DbRepository()
        {
            DbContext = new ToDoListDbContext();
            DbContext.ToDos.Load();
            DbContext.Projects.Load();
            DbContext.Priorities.Load();
        }

        public static ToDo AddToDo(ToDo toAdd)
        {
            ToDo tmp = DbContext.ToDos.Add(toAdd);
            DbContext.SaveChanges();
            return tmp;
        }

        public static Project AddProject(Project toAdd)
        {
            Project tmp = DbContext.Projects.Add(toAdd);
            DbContext.SaveChanges();
            return tmp;
        }
    }
}
