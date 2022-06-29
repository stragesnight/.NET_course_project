﻿using System;
using NET_course_project.Misc;
using NET_course_project.Model;
using NET_course_project.Repository;

namespace NET_course_project.ViewModel
{
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

        public object InitialState { get; set; }
        public object ResultState
        {
            get => CreatedProject;
            set { if (value is Project) CreatedProject = value as Project; }
        }


        private RelayCommand _addProjectCommand = null;
        public RelayCommand AddProjectCommand => _addProjectCommand ??
            (_addProjectCommand = new RelayCommand(x => DbRepository.AddProject(CreatedProject)));


        public AddProjectDialogViewModel()
        {
            CreatedProject = new Project();
        }
    }
}