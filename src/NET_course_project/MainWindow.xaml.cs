using System;
using System.Windows;
using NET_course_project.ViewModel;
using ToDoListCommon.Misc;

namespace NET_course_project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(User user)
        {
            InitializeComponent();
            if (this.DataContext != null)
                (this.DataContext as MainWindowViewModel).User = user;
            else
                this.DataContextChanged += (s, e) => (this.DataContext as MainWindowViewModel).User = user;
        }

        private void SelectParent(object sender, RoutedEventArgs e)
        {
            ((sender as FrameworkElement).Parent as FrameworkElement).Focus();
        }

        private void CloseWindow(object sender = null, RoutedEventArgs e = null)
            => this.Close();
    }
}
