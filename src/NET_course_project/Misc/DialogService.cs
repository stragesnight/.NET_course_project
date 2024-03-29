﻿using System;
using System.Windows;
using NET_course_project.View;

namespace NET_course_project.Misc
{
    /// <summary>
    /// Класс для контрольованого відображення діалогових та звичайних вікон
    /// з можливістю передачі даних між ними.
    /// </summary>
    public static class DialogService
    {
        public static void ShowDialog(string dialogName, object state, Action<object> callback = null)
        {
            try
            {
                BaseDialogWindow dialogWindow = new BaseDialogWindow();
                Type type = Type.GetType($"NET_course_project.View.{dialogName}");
                FrameworkElement content = Activator.CreateInstance(type, state) as FrameworkElement;
                if (!(content.DataContext is IDialog))
                    throw new Exception("Dialog view model class must implement IDialog interface");

                dialogWindow.Content = content;
                dialogWindow.Width = content.Width;
                dialogWindow.Height = content.Height;
                dialogWindow.Title = content.Name.Replace("_", " ");

                EventHandler closeHandler = null;
                closeHandler = (s, e) => {
                    callback?.Invoke((content.DataContext as IDialog).ResultState);
                    dialogWindow.Closed -= closeHandler;
                };
                dialogWindow.Closed += closeHandler;

                dialogWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void ShowWindow(string windowName)
        {
            try
            {
                Type type = Type.GetType($"NET_course_project.View.{windowName}");
                Window window = Activator.CreateInstance(type) as Window;
                window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
