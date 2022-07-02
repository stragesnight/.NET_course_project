using System;
using System.Windows;
using NET_course_project.ViewModel;

namespace NET_course_project.View
{
    /// <summary>
    /// Interaction logic for AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
            this.DataContext = new AuthorizationWindowViewModel();
        }

        public void CloseWindow(object sender, RoutedEventArgs e)
            => this.Close();
    }
}
