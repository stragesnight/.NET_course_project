using System;
using System.Windows;
using NET_course_project.ViewModel;

namespace NET_course_project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectParent(object sender, RoutedEventArgs e)
        {
            ((sender as FrameworkElement).Parent as FrameworkElement).Focus();
        }

        public void CloseWindow(object sender = null, RoutedEventArgs e = null)
            => this.Close();
    }
}
