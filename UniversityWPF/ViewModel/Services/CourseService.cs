using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityWPF.Model;
using System.Linq;
using System.Collections.ObjectModel;
using UniversityWPF.Library.Interfaces;
using System.Collections.Specialized;
using System.Windows;

namespace UniversityWPF.ViewModel.Services
{
    public class CourseService : ICourseService
    {
        private UniversityContext _db;

        public CourseService(UniversityContext context)
        {
            _db = context;
        }

        public ObservableCollection<Course> GetAll()
        {
            _db.Courses.Load();
            return _db.Courses.Local.ToObservableCollection();
        }
        public void SaveChanges(object? obj = null)
        {
            if (obj is null)
            {
                _db.SaveChanges();
                _db.Courses.ToList().Last().OnPropertyChanged("CourseId");
            }
            else if (obj is NotifyCollectionChangedEventArgs arg && arg.Action == NotifyCollectionChangedAction.Remove)
            {
                _db.SaveChanges();
            }
        }
    }
}
