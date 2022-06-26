using System;
using System.Windows;
using System.Windows.Controls;

namespace NET_course_project.View
{
    /// <summary>
    /// Interaction logic for AddToDo.xaml
    /// </summary>
    public partial class AddToDo : UserControl
    {
        public AddToDo()
        {
            InitializeComponent();
        }

        private void CreateToDoButton_Click(object sender, RoutedEventArgs e)
        {
            Window parent = this.Parent as Window;
            parent.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Window parent = this.Parent as Window;
            parent.Tag = null;
            parent.Close();
        }
    }
}
