﻿using System.Windows;
using System.Windows.Controls;
using NET_course_project.ViewModel;

namespace NET_course_project.View
{
    /// <summary>
    /// Interaction logic for AddProjectDialog.xaml
    /// </summary>
    public partial class AddProjectDialog : UserControl
    {
        public AddProjectDialog(object _)
        {
            InitializeComponent();
            this.DataContext = new AddProjectDialogViewModel();
        }

        public void CloseWindow(object sender = null, RoutedEventArgs e = null)
            => (this.Parent as Window).Close();
    }
}
