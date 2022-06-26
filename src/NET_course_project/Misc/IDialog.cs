using System;

namespace NET_course_project.Misc
{
    public interface IDialog
    {
        object InitialState { get; set; }
        object ResultState { get; set; }
    }
}
