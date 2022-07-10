using System;
using System.Text.RegularExpressions;

namespace ToDoListCommon.Misc
{
    /// <summary>
    /// Клас, що реалізує методи для валідації вводу користувача
    /// </summary>
    public static class InputValidator
    {
        private static Regex _alphaNumericProxy = null;
        private static Regex _alphaNumeric => _alphaNumericProxy ??
            (_alphaNumericProxy = new Regex(@"^(\w)+$", RegexOptions.IgnoreCase | RegexOptions.Compiled));

        public static bool IsValidUsername(string str) => _alphaNumeric.IsMatch(str);
        public static bool IsValidPassword(string str) => _alphaNumeric.IsMatch(str);
    }
}
