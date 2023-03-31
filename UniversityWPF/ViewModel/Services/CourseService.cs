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
        private ObservableCollection<Course> _courses;

        public CourseService(UniversityContext context)
        {
            _db = context;
            _db.Courses.Load();

            _courses = _db.Courses.Local.ToObservableCollection();
		}

        public ObservableCollection<Course> GetAll()
        {
            return _courses;
		}
        public void SaveChanges(object? obj = null)
        {
            if (obj is Course course)
            {
                if (course.CourseId == 0)
                {
					try
					{
						_db.SaveChanges();
						_db.Courses.ToList().Last().OnPropertyChanged("CourseId");
					}
					catch (DbUpdateException)
					{
						MessageBox.Show("Course with this name already exist");
                        _courses.Remove(course);
                    }
				}
                else
                {
                    try
                    {
						_db.SaveChanges();
					}
                    catch (DbUpdateException)
                    {
						MessageBox.Show("Course with this name already exist");
                        _db.Entry(course).Reload();
                        course.OnPropertyChanged("Name");
                    }
                }
            }
            else if (obj is NotifyCollectionChangedEventArgs arg && arg.Action == NotifyCollectionChangedAction.Remove)
            {
                _db.SaveChanges();
            }
        }
    }
}
