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
            this.DataContext = new MainWindowViewModel();
        }

        private void SelectParent(object sender, RoutedEventArgs e)
        {
            ((sender as FrameworkElement).Parent as FrameworkElement).Focus();
        }
    }
}
