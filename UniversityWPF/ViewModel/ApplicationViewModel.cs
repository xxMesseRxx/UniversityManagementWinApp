using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityWPF.Library;
using UniversityWPF.Model;
using System.Windows;
using System.Collections.Specialized;
using UniversityWPF.Library.Interfaces;

namespace UniversityWPF.ViewModel
{
    public class ApplicationViewModel
    {
        public ObservableCollection<Course> Courses { get; set; }
        public RelayCommand SaveChangesCommand { get { return _saveChangesCommand; } }

		private RelayCommand _saveChangesCommand;
		private ICourseService _courseService;

        public ApplicationViewModel(ICourseService courseService) 
        {
            _courseService = courseService;

            _saveChangesCommand = new RelayCommand(_courseService.SaveChanges);

            Courses = _courseService.GetAll();
            Courses.CollectionChanged += (sender, e) => { SaveChangesCommand.Execute(e); };
        }
    }
}
