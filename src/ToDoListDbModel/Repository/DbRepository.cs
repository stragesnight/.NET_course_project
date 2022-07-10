using System;
using System.Windows;
using System.Data.Entity;
using System.Configuration;
using ToDoListCommon.Misc;
using ToDoListCommon.Model;

namespace ToDoListCommon.Repository
{
    /// <summary>
    /// Репозиторій для бази даних. Реалізує методи швидкого доступу (інтерфейс) до БД
    /// з використанням ToDoListDbContext.
    /// Такод має механізм сповіщення про зміну стану БД за допомогою події.
    /// </summary>
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
                MessageBox.Show("Unable to initialize database.\nError message: " + ex.Message);
                return false;
            }

            return true;
        }

        public static ToDo AddToDo(ToDo toAdd)
        {
            try
            {
                ToDo tmp = DbContext.ToDos.Add(toAdd);
                SaveChanges();
                return tmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to add to-do.\nError message: " + ex.Message);
                return null;
            }
        }

        public static Project AddProject(Project toAdd)
        {
            try
            {
                Project tmp = DbContext.Projects.Add(toAdd);
                SaveChanges();
                return tmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to add project.\nError message: " + ex.Message);
                return null;
            }
}

        public static Tag AddTag(Tag toAdd)
        {
            try
            {
                Tag tmp = DbContext.Tags.Add(toAdd);
                SaveChanges();
                return tmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to add tag.\nError message: " + ex.Message);
                return null;
            }
        }

        public static ToDo_Tag AddToDo_Tag(ToDo_Tag toAdd)
        {
            try
            {
                ToDo_Tag tmp = DbContext.ToDos_Tags.Add(toAdd);
                SaveChanges();
                return tmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to add tag to to-do.\nError message: " + ex.Message);
                return null;
            }
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
