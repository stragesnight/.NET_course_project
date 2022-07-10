using System;

namespace NET_course_project.Misc
{
    /// <summary>
    /// Інтерфейс, який повинні реалізувати моделі представленнявсіх діалогових вікон
    /// для передачі даних.
    /// </summary>
    public interface IDialog
    {
        object InitialState { get; set; }
        object ResultState { get; set; }
    }
}
