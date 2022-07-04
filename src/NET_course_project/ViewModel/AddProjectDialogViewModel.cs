using System;
using NET_course_project.Misc;
using ToDoListCommon.Misc;
using ToDoListCommon.Model;
using ToDoListCommon.Repository;

namespace NET_course_project.ViewModel
{
    /// <summary>
    /// Модель представлення діалогу створення нового проекту.
    /// Реалізує метод для безпосереднього додавання проекту до БД.
    /// </summary>
    public class AddProjectDialogViewModel : Observable, IDialog
    {
        private Project _createdProject = null;
        public Project CreatedProject
        {
            get => _createdProject;
            set
            {
                _createdProject = value;
                OnPropertyChanged("CreatedProject");
            }
        }

        private bool _shouldClose = false;
        public bool ShouldClose
        {
            get => _shouldClose;
            set
            {
                _shouldClose = value;
                OnPropertyChanged("ShouldClose");
            }
        }

        public object InitialState { get; set; }
        public object ResultState
        {
            get => CreatedProject;
            set { if (value is Project) CreatedProject = value as Project; }
        }


        private RelayCommand _addProjectCommand = null;
        public RelayCommand AddProjectCommand => _addProjectCommand ??
            (_addProjectCommand = new RelayCommand(x => HandleAddProject()));


        public AddProjectDialogViewModel()
        {
            CreatedProject = new Project();
        }

        private void HandleAddProject()
        {
            ShouldClose = DbRepository.AddProject(CreatedProject) != null;
        }
    }
}
