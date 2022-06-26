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
        public ToDoListDbContext DbContext { get; private set; } = null;

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
            new ObservableCollection<ToDo>(DbContext.ToDos.Where(x =>
                DbFunctions.DiffDays(DateTime.Now, x.DueTo) >= 0 && DbFunctions.DiffDays(DateTime.Now, x.DueTo) <= 1).ToList());

        public ObservableCollection<ToDo> Ongoing3DayToDos =>
            new ObservableCollection<ToDo>(DbContext.ToDos.Where(x =>
                DbFunctions.DiffDays(DateTime.Now, x.DueTo) > 1 && DbFunctions.DiffDays(DateTime.Now, x.DueTo) <= 3).ToList());

        public ObservableCollection<ToDo> Ongoing7DayToDos =>
            new ObservableCollection<ToDo>(DbContext.ToDos.Where(x =>
                DbFunctions.DiffDays(DateTime.Now, x.DueTo) > 3 && DbFunctions.DiffDays(DateTime.Now, x.DueTo) <= 7).ToList());


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
                Description = $"ToDoDescription{nToDos + 1}",
                DueTo = DateTime.Now,
                PlannedCompletionTime = DateTime.Now,
                PriorityId = 1,
                ProjectId = projectId,
                Completed = false
            };

            SelectedToDo = DbContext.ToDos.Add(toDo);
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
