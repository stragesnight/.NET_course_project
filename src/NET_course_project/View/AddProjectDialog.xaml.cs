using System.Windows;
using System.Windows.Controls;
using NET_course_project.Model;

namespace NET_course_project.View
{
    /// <summary>
    /// Interaction logic for AddProjectDialog.xaml
    /// </summary>
    public partial class AddProjectDialog : UserControl
    {
        public AddProjectDialog()
        {
            InitializeComponent();
        }

        private void CreateProjectButton_Click(object sender, RoutedEventArgs e)
        {
            Window parent = this.Parent as Window;
            parent.Tag = new Project { Title = projectNameTextBox.Text };
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
