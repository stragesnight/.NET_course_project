using System;

namespace NET_course_project.Repository
{
    /// <summary>
    /// Репозиторій для бази даних.
    /// Реалізує методи швидкого доступу до БД.
    /// </summary>
    public static class Repository
    {
        private static ToDoListDbContext _dbContext = null;

        static Repository()
        {
            _dbContext = new ToDoListDbContext();
        }

        // TODO: реалізувати методи для взаємодії з базою даних
    }
}
