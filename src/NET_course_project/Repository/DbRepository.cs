using System;
using System.Windows;
using System.Data.Entity;
using System.Configuration;
using NET_course_project.Misc;
using NET_course_project.Model;

namespace NET_course_project.Repository
{
    public static class DbRepository
    {
        public static event Action ChangesSaved;
        public static ToDoListDbContext DbContext { get; private set; } = null;


        public static bool Initialize(User user)
        {
            try
            {
                string baseConnStr = ConfigurationManager.ConnectionStrings["conn_str"].ConnectionString;
                baseConnStr = baseConnStr.Replace("{USER_ID}", user.Login);
                baseConnStr = baseConnStr.Replace("{PASSWORD}", user.Password);
                DbContext = new ToDoListDbContext(baseConnStr);
                ReloadLocal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        public static ToDo AddToDo(ToDo toAdd)
        {
            ToDo tmp = DbContext.ToDos.Add(toAdd);
            SaveChanges();
            return tmp;
        }

        public static Project AddProject(Project toAdd)
        {
            Project tmp = DbContext.Projects.Add(toAdd);
            SaveChanges();
            return tmp;
        }

        public static Tag AddTag(Tag toAdd)
        {
            Tag tmp = DbContext.Tags.Add(toAdd);
            SaveChanges();
            return tmp;
        }

        public static ToDo_Tag AddToDo_Tag(ToDo_Tag toAdd)
        {
            ToDo_Tag tmp = DbContext.ToDos_Tags.Add(toAdd);
            SaveChanges();
            return tmp;
        }

        public static void SaveChanges()
        {
            try
            {
                DbContext.SaveChanges();
                //ReloadLocal();
                ChangesSaved?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void ReloadLocal()
        {
            DbContext.ToDos.Load();
            DbContext.Projects.Load();
            DbContext.Priorities.Load();
            DbContext.Tags.Load();
        }
    }
}
