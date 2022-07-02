using System;
using System.Windows;
using System.Windows.Controls;
using NET_course_project.ViewModel;

namespace NET_course_project.View
{
    /// <summary>
    /// Interaction logic for AddToDo.xaml
    /// </summary>
    public partial class AddToDoDialog : UserControl
    {
        public AddToDoDialog(object initialState)
        {
            InitializeComponent();
            if (!(initialState is int))
                throw new Exception("Invalid initial state");

            this.DataContext = new AddToDoDialogViewModel((int)initialState);
        }

        public void CloseWindow()
            => (this.Parent as Window).Close();

        public void CloseWindow(object sender, RoutedEventArgs e)
            => CloseWindow();
    }
}
