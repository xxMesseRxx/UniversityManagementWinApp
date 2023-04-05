using Microsoft.EntityFrameworkCore;
using System;
using UniversityWPF.Model;
using System.Collections.ObjectModel;
using UniversityWPF.Library.Interfaces;
using System.Collections.Specialized;
using System.Windows;
using UniversityWPF.Library;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace UniversityWPF.ViewModel.Services
{
    public class CourseService : ICourseService, INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler? PropertyChanged;

		public ObservableCollection<Course> Courses
        { 
            get
            {
                return _courses;
            }
            set
            {
                _courses = value;
                OnPropertyChanged();
            }
        }
		public RelayCommand SaveChangesCommand { get { return _saveChangesCommand; } }

		private RelayCommand _saveChangesCommand;
		private UniversityContext _db;
        private ObservableCollection<Course> _courses;
        private IServiceProvider _serviceProvider;

        public CourseService(IServiceProvider provider)
        {
			_serviceProvider = provider;

			SetActualDbContext();

			_saveChangesCommand = new RelayCommand(SaveChangesInDb);
		}

		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
		public void SaveChangesInDb(object? obj = null)
        {
            if (obj is Course course)
            {
				if (course.CourseId == 0)
				{
					AddCourseSaveChanges(course);
				}
				else
				{
					EditingCourseSaveChanges(course);
				}
            }
            else if (obj is NotifyCollectionChangedEventArgs arg && arg.Action == NotifyCollectionChangedAction.Remove)
            {
				RemoveActionSaveChanges();
            }
        }

		private void AddCourseSaveChanges(Course course)
		{
			if (string.IsNullOrEmpty(course.Name))
			{
				Courses.Remove(course);
				throw new ArgumentNullException("Course name", "You didn't enter a name");
			}
			else
			{
				try
				{
					_db.SaveChanges();
					course.OnPropertyChanged("CourseId");
				}
				catch (DbUpdateException)
				{
					Courses.Remove(course);
					throw new ArgumentException($"Course with \"{course.Name}\" name already exist", "Course name");
				}
			}
		}
		private void EditingCourseSaveChanges(Course course)
		{
			if (string.IsNullOrEmpty(course.Name))
			{
				_db.Entry(course).Reload();
				course.OnPropertyChanged("Name");
				throw new ArgumentNullException("Course name", "You didn't enter a name");
			}
			else
			{
				try
				{
					_db.SaveChanges();
				}
				catch (DbUpdateException)
				{
					string oldName = course.Name;
					_db.Entry(course).Reload();
					course.OnPropertyChanged("Name");
					throw new ArgumentException($"Course with \"{oldName}\" name already exist", "Course name");
				}
			}
		}
		private void RemoveActionSaveChanges()
		{
			try
			{
				_db.SaveChanges();
			}
			catch (DbUpdateException)
			{
				SetActualDbContext();
			}
		}
        private void SetActualDbContext()
        {
			_db = _serviceProvider.GetRequiredService<UniversityContext>();
            _db.Courses.Load();
			Courses = _db.Courses.Local.ToObservableCollection();
			Courses.CollectionChanged += (sender, e) => { SaveChangesInDb(e); };
		}
	}
}
