﻿using System;
using System.Linq;
using NET_course_project.Misc;
using NET_course_project.Model;
using NET_course_project.Repository;

namespace NET_course_project.ViewModel
{
    public class AddTagDialogViewModel : Observable, IDialog
    {
        private Tag _createdTag = null;
        public Tag CreatedTag
        {
            get => _createdTag;
            set
            {
                _createdTag = value;
                OnPropertyChanged("CreatedTag");
            }
        }

        public object InitialState { get; set; }
        public object ResultState
        {
            get => CreatedTag;
            set { if (value is Tag) CreatedTag = value as Tag; }
        }


        private RelayCommand _addTagCommand = null;
        public RelayCommand AddTagCommand => _addTagCommand ??
            (_addTagCommand = new RelayCommand(x => HandleAddTag()));

        private int _toDoId = 0;


        public AddTagDialogViewModel(int toDoId)
        {
            _createdTag = new Tag();
            _toDoId = toDoId;
        }

        private void HandleAddTag()
        {
            Tag tag = DbRepository.DbContext.Tags.FirstOrDefault(x => x.Title == _createdTag.Title);
            _createdTag = tag == null ? DbRepository.AddTag(_createdTag) : tag;

            DbRepository.DbContext.ToDos_Tags.Add(new ToDo_Tag {
                ToDoId = _toDoId,
                TagId = _createdTag.Id
            });
        }
    }
}