using System;
using System.Linq;
using System.Data.Entity;
using NET_course_project.Misc;
using NET_course_project.Model;
using NET_course_project.Repository;

namespace NET_course_project.ViewModel
{
    public class MainWindowViewModel : Observable
    {
        public ToDoListDbContext DbContext { get; private set; } = null;

        private ToDo_Project _selectedToDo_Project = null;
        public ToDo_Project SelectedToDo_Project
        {
            get => _selectedToDo_Project;
            set
            {
                _selectedToDo_Project = value;
                OnPropertyChanged("SelectedToDo_Project");
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


        private RelayCommand _addToDoCommand = null;
        public RelayCommand AddToDoCommand => _addToDoCommand ??
            (_addToDoCommand = new RelayCommand(x => HandleAddToDo((int)x)));

        private RelayCommand _addProjectCommand = null;
        public RelayCommand AddProjectCommand => _addProjectCommand ??
            (_addProjectCommand = new RelayCommand(x => HandleAddProject()));

        public MainWindowViewModel()
        {
            DbContext = new ToDoListDbContext();
            DbContext.ToDos.Load();
            DbContext.Projects.Load();
        }

        private void HandleAddToDo(int projectId)
        {
            Project project = DbContext.Projects.FirstOrDefault(x => x.Id == projectId);
            if (project == null)
                return;

            int nToDos = DbContext.ToDos.Count();
            ToDo toDo = new ToDo {
                Title = $"ToDoTitle{nToDos + 1}",
                DueTo = DateTime.Now,
                PriorityId = 1,
                Description = $"ToDoDescription{nToDos + 1}"
            };
            toDo = DbContext.ToDos.Add(toDo);
            DbContext.SaveChanges();

            SelectedToDo_Project = DbContext.ToDos_Projects.Add(new ToDo_Project { ToDoId = toDo.Id, ProjectId = projectId });
            DbContext.SaveChanges();
        }

        private void HandleAddProject()
        {
            int nProjects = DbContext.Projects.Count();
            Project project = new Project { Title = $"Project{nProjects + 1}" };
            SelectedProject = DbContext.Projects.Add(project);
            DbContext.SaveChanges();
        }
    }
}
