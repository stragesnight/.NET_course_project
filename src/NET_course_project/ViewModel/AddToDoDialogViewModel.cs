using System;
using NET_course_project.Misc;
using ToDoListCommon.Misc;
using ToDoListCommon.Model;
using ToDoListCommon.Repository;

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
            get => CreatedToDo;
            set { if (value is ToDo) CreatedToDo = value as ToDo; }
        }


        private RelayCommand _addToDoCommand = null;
        public RelayCommand AddToDoCommand => _addToDoCommand ??
            (_addToDoCommand = new RelayCommand(x => HandleAddToDo()));


        public AddToDoDialogViewModel(int projectId)
        {
            CreatedToDo = new ToDo();
            CreatedToDo.ProjectId = projectId;
            CreatedToDo.DueTo = DateTime.Now;
        }

        private void HandleAddToDo()
        {
            ShouldClose = (CreatedToDo = DbRepository.AddToDo(CreatedToDo)) != null;
        }
    }
}
