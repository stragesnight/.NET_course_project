using System;
using System.Linq;
using System.Data.Entity;
using System.Diagnostics;
using System.Collections.Generic;
using NET_course_project.Misc;
using ToDoListCommon.Misc;
using ToDoListCommon.Model;
using ToDoListCommon.Repository;

namespace NET_course_project.ViewModel
{
				/// <summary>
				/// Модель представлення головного вікна додатка.
				/// Реалізує різноманітні методи для маніпуляції набором справ.
				/// Також має властивості, які контролюють зміст представлення.
				/// </summary>
    public class MainWindowViewModel : Observable
    {
        public ToDoListDbContext DbContext => DbRepository.DbContext;

        private User _user = null;
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged("User");
                IsUserPriviliged = User?.Login.ToLower().Contains("admin") ?? true;
            }
        }

        private bool _isUserPriviliged = false;
        public bool IsUserPriviliged
        {
            get => _isUserPriviliged;
            set
            {
                _isUserPriviliged = value;
                OnPropertyChanged("IsUserPriviliged");
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

        private List<FilterTask<ToDo>> _filters = null;
        public List<FilterTask<ToDo>> Filters
        {
            get => _filters;
            set
            {
                _filters = value;
                OnPropertyChanged("ToDoFilters");
            }
        }

        private FilterTask<ToDo> _selectedFilter = null;
        public FilterTask<ToDo> SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                _selectedFilter = value;
                OnPropertyChanged("SelectedFilter");
                FilterString = String.Empty;
            }
        }

        private string _filterString = null;
        public string FilterString
        {
            get => _filterString;
            set
            {
                _filterString = value;
                OnPropertyChanged("FilterString");
                RefilterToDos();
            }
        }

        private List<ToDo> _filteredToDos = null;
        public List<ToDo> FilteredToDos
        {
            get => _filteredToDos;
            set
            {
                _filteredToDos = value;
                OnPropertyChanged("FilteredToDos");
            }
        }

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

        private RelayCommand _showHelpCommand = null;
        public RelayCommand ShowHelpCommand => _showHelpCommand ??
            (_showHelpCommand = new RelayCommand(x => HandleShowHelp()));

        private RelayCommand _logoutCommand = null;
        public RelayCommand LogoutCommand => _logoutCommand ??
            (_logoutCommand = new RelayCommand(x => HandleLogout()));

        private RelayCommand _showAboutCommand = null;
        public RelayCommand ShowAboutCommand => _showAboutCommand ??
            (_showAboutCommand = new RelayCommand(x => HandleShowAbout()));

        public MainWindowViewModel()
        {
            Filters = new List<FilterTask<ToDo>> {
                new FilterTask<ToDo>("Title", toDo => String.IsNullOrEmpty(FilterString) ||
                    toDo.Title.ToLower().Contains(FilterString.ToLower())),

                new FilterTask<ToDo>("Description", toDo => String.IsNullOrEmpty(FilterString) ||
                    toDo.Description.ToLower().Contains(FilterString.ToLower())),

                new FilterTask<ToDo>("Priority", toDo => String.IsNullOrEmpty(FilterString) ||
                    toDo.Priority.Title.ToLower().Contains(FilterString.ToLower())),

                new FilterTask<ToDo>("Project", toDo => String.IsNullOrEmpty(FilterString) ||
                    toDo.Project.Title.ToLower().Contains(FilterString.ToLower())),

                new FilterTask<ToDo>("Tag", toDo => String.IsNullOrEmpty(FilterString) ||
                    toDo.ToDos_Tags.FirstOrDefault(x => x.Tag.Title.ToLower().Contains(FilterString.ToLower())) != null),

                new FilterTask<ToDo>("Due date", toDo => String.IsNullOrEmpty(FilterString) ||
                    toDo.DueTo.ToString().Contains(FilterString))
            };

            SelectedFilter = Filters[0];

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

        private void HandleShowHelp()
        {
            try
            {
                Process.Start("https://github.com/stragesnight/.NET_course_project/");
            }
            catch (Exception)
            { }
        }

        private void HandleLogout()
        {
            DialogService.ShowWindow("AuthorizationWindow");
            ShouldClose = true;
        }

        private void HandleShowAbout()
        {
            DialogService.ShowWindow("AboutWindow");
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

        private void RefilterToDos()
        {
            FilteredToDos = SelectedFilter.FilterItems(DbContext.ToDos.Local);
        }

        private void OnChangesSaved()
        {
            RecalculateOngoingToDos();
        }
    }
}
