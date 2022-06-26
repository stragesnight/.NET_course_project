using System;
using System.Linq;
using System.Data.Entity;
using NET_course_project.Misc;
using NET_course_project.Model;
using NET_course_project.Repository;
using System.Collections.ObjectModel;

namespace NET_course_project.ViewModel
{
    public class MainWindowViewModel : Observable
    {
        public ToDoListDbContext DbContext => DbRepository.DbContext;

        private ToDo _selectedToDo = null;
        public ToDo SelectedToDo
        {
            get => _selectedToDo;
            set
            {
                _selectedToDo = value;
                OnPropertyChanged("SelectedToDo");
            }
        }

        private Project _selectedProject = null;
        public Project SelectedProject
        {
            get => _selectedProject;
            set
            {
                _selectedProject = value;
                OnPropertyChanged("SelectedProject");
            }
        }

        public ObservableCollection<ToDo> Ongoing1DayToDos =>
            new ObservableCollection<ToDo>(DbRepository.DbContext.ToDos.Where(x =>
                DbFunctions.DiffDays(DateTime.Now, x.DueTo) >= 0 && DbFunctions.DiffDays(DateTime.Now, x.DueTo) <= 1).ToList());

        public ObservableCollection<ToDo> Ongoing3DayToDos =>
            new ObservableCollection<ToDo>(DbRepository.DbContext.ToDos.Where(x =>
                DbFunctions.DiffDays(DateTime.Now, x.DueTo) > 1 && DbFunctions.DiffDays(DateTime.Now, x.DueTo) <= 3).ToList());

        public ObservableCollection<ToDo> Ongoing7DayToDos =>
            new ObservableCollection<ToDo>(DbRepository.DbContext.ToDos.Where(x =>
                DbFunctions.DiffDays(DateTime.Now, x.DueTo) > 3 && DbFunctions.DiffDays(DateTime.Now, x.DueTo) <= 7).ToList());


        private RelayCommand _addToDoCommand = null;
        public RelayCommand AddToDoCommand => _addToDoCommand ??
            (_addToDoCommand = new RelayCommand(x => HandleAddToDo((int)x)));

        private RelayCommand _addProjectCommand = null;
        public RelayCommand AddProjectCommand => _addProjectCommand ??
            (_addProjectCommand = new RelayCommand(x => HandleAddProject()));

        public MainWindowViewModel()
        { }

        private void HandleAddToDo(int projectId)
        {
            Project project = DbRepository.DbContext.Projects.FirstOrDefault(x => x.Id == projectId);
            if (project == null)
                return;

            DialogService.ShowDialog("AddToDoDialog", projectId, result => {
                SelectedToDo = result as ToDo;
            });
        }

        private void HandleAddProject()
        {
            DialogService.ShowDialog("AddProjectDialog", null, result => {
                SelectedProject = result as Project;
            });
        }
    }
}
