﻿using System;
using System.Windows;
using System.Windows.Controls;
using NET_course_project.ViewModel;

namespace NET_course_project.View
{
    /// <summary>
    /// Interaction logic for AddTagDialog.xaml
    /// </summary>
    public partial class AddTagDialog : UserControl
    {
        public AddTagDialog(object initialState)
        {
            InitializeComponent();
            this.DataContext = new AddTagDialogViewModel((int)initialState);
        }

        public void CloseWindow()
            => (this.Parent as Window).Close();

        public void CloseWindow(object sender, RoutedEventArgs e)
            => CloseWindow();
    }
}
