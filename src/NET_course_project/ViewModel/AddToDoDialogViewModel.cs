using System;
using NET_course_project.Misc;
using NET_course_project.Model;
using NET_course_project.Repository;

namespace NET_course_project.ViewModel
{
    public class AddToDoDialogViewModel : Observable, IDialog
    {
        public ToDoListDbContext DbContext => DbRepository.DbContext;

        private ToDo _toDo = null;
        public ToDo CreatedToDo
        {
            get => _toDo;
            set
            {
                _toDo = value;
                OnPropertyChanged("CreatedToDo");
            }
        }

        public object InitialState { get; set; }
        public object ResultState
        {
            get => CreatedToDo;
            set { if (value is ToDo) CreatedToDo = value as ToDo; }
        }


        private RelayCommand _addToDoCommand = null;
        public RelayCommand AddToDoCommand => _addToDoCommand ??
            (_addToDoCommand = new RelayCommand(x => DbRepository.AddToDo(CreatedToDo)));


        public AddToDoDialogViewModel(int projectId)
        {
            _toDo = new ToDo();
            _toDo.ProjectId = projectId;
            _toDo.DueTo = DateTime.Now;
        }
    }
}
