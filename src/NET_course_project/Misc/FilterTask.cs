using System;
using System.Collections.Generic;
using System.Linq;

namespace NET_course_project.Misc
{
    public class FilterTask<T>
    {
        public string Title { get; set; }
        public Func<T, bool> Predicate { get; set; }

        public FilterTask(string title, Func<T, bool> filterPredicate)
        {
            Title = title;
            Predicate = filterPredicate;
        }

        public List<T> FilterItems(IEnumerable<T> items)
        {
            return items.Where(x => Predicate(x)).ToList();
        }

        public override string ToString() => Title;
    }
}
