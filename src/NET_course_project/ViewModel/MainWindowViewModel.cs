using System;
using System.Linq;
using System.Data.Entity;
using NET_course_project.Misc;
using NET_course_project.Model;
using NET_course_project.Repository;
using System.Collections.Generic;

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
                OnPropertyChanged("IsSomeToDoSelected");
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

        public bool IsSomeToDoSelected => _selectedToDo != null;

        private List<ToDo> _ongoing1DayToDos = null;
        public List<ToDo> Ongoing1DayToDos
        {
            get => _ongoing1DayToDos;
            set
            {
                _ongoing1DayToDos = value;
                OnPropertyChanged("Ongoing1DayToDos");
            }
        }

        private List<ToDo> _ongoing3DayToDos = null;
        public List<ToDo> Ongoing3DayToDos
        {
            get => _ongoing3DayToDos;
            set
            {
                _ongoing3DayToDos = value;
                OnPropertyChanged("Ongoing3DayToDos");
            }
        }

        private List<ToDo> _ongoing7DayToDos = null;
        public List<ToDo> Ongoing7DayToDos
        {
            get => _ongoing7DayToDos;
            set
            {
                _ongoing7DayToDos = value;
                OnPropertyChanged("Ongoing7DayToDos");
            }
        }


        private RelayCommand _addToDoCommand = null;
        public RelayCommand AddToDoCommand => _addToDoCommand ??
            (_addToDoCommand = new RelayCommand(x => HandleAddToDo((int)x)));

        private RelayCommand _removeToDoCommand = null;
        public RelayCommand RemoveToDoCommand => _removeToDoCommand ??
            (_removeToDoCommand = new RelayCommand(x => HandleRemoveToDo((int)x)));

        private RelayCommand _addProjectCommand = null;
        public RelayCommand AddProjectCommand => _addProjectCommand ??
            (_addProjectCommand = new RelayCommand(x => HandleAddProject()));

        private RelayCommand _removeProjectCommand = null;
        public RelayCommand RemoveProjectCommand => _removeProjectCommand ??
            (_removeProjectCommand = new RelayCommand(x => HandleRemoveProject((int)x)));

        private RelayCommand _saveChangesCommand = null;
        public RelayCommand SaveChangesCommand => _saveChangesCommand ??
            (_saveChangesCommand = new RelayCommand(x => HandleSaveChanges()));

        private RelayCommand _addTagCommand = null;
        public RelayCommand AddTagCommand => _addTagCommand ??
            (_addTagCommand = new RelayCommand(x => HandleAddTag((int)x)));

        private RelayCommand _removeTagCommand = null;
        public RelayCommand RemoveTagCommand => _removeTagCommand ??
            (_removeTagCommand = new RelayCommand(x => HandleRemoveTag((int)x)));

        public MainWindowViewModel()
        {
            RecalculateOngoingToDos();
            DbRepository.ChangesSaved += OnChangesSaved;
        }

        private void HandleAddToDo(int projectId)
        {
            DialogService.ShowDialog("AddToDoDialog", projectId);
        }

        private void HandleRemoveToDo(int toDoId)
        {
            ToDo toDo = DbContext.ToDos.FirstOrDefault(x => x.Id == toDoId);
            if (toDo == null)
                return;

            DbContext.ToDos.Remove(toDo);
            DbRepository.SaveChanges();
        }

        private void HandleAddProject()
        {
            DialogService.ShowDialog("AddProjectDialog", null);
        }

        private void HandleRemoveProject(int projectId)
        {
            Project project = DbContext.Projects.FirstOrDefault(x => x.Id == projectId);
            if (project == null)
                return;

            DbContext.ToDos.RemoveRange(project.ToDos);
            DbContext.Projects.Remove(project);
            DbRepository.SaveChanges();
        }

        private void HandleSaveChanges()
        {
            DbRepository.SaveChanges();
        }

        private void HandleAddTag(int toDoId)
        {
            DialogService.ShowDialog("AddTagDialog", toDoId, result => {
                HandleSaveChanges();
            });
        }

        private void HandleRemoveTag(int todo_tagId)
        {
            ToDo_Tag tdt = DbContext.ToDos_Tags.FirstOrDefault(x => x.Id == todo_tagId);
            if (tdt == null)
                return;

            DbContext.ToDos_Tags.Remove(tdt);
            DbRepository.SaveChanges();
        }

        private void RecalculateOngoingToDos()
        {
            Ongoing1DayToDos = DbRepository.DbContext.ToDos.Where(x =>
                !x.Completed
                && DbFunctions.DiffDays(DateTime.Now, x.DueTo) >= 0
                && DbFunctions.DiffDays(DateTime.Now, x.DueTo) <= 1).ToList();

            Ongoing3DayToDos = DbRepository.DbContext.ToDos.Where(x =>
                !x.Completed
                && DbFunctions.DiffDays(DateTime.Now, x.DueTo) > 1
                && DbFunctions.DiffDays(DateTime.Now, x.DueTo) <= 3).ToList();

            Ongoing7DayToDos = DbRepository.DbContext.ToDos.Where(x =>
                !x.Completed
                && DbFunctions.DiffDays(DateTime.Now, x.DueTo) > 3
                && DbFunctions.DiffDays(DateTime.Now, x.DueTo) <= 7).ToList();
        }

        private void OnChangesSaved()
        {
            RecalculateOngoingToDos();
        }
    }
}
