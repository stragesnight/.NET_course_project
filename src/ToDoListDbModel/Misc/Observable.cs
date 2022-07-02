using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ToDoListCommon.Misc
{
    /// <summary>
    /// Допоміжний клас, що описує об'єкт, спостерігаючий за змінами в своїх властивостях.
    /// </summary>
    public abstract class Observable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
