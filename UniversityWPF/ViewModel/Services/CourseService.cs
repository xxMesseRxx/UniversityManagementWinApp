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
using UniversityWPF.Library;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Hosting;
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

		private void SaveChangesInDb(object? obj = null)
        {
            if (obj is Course course)
            {
                if (course.CourseId == 0)
                {
					try
					{
						_db.SaveChanges();
						course.OnPropertyChanged("CourseId");
					}
					catch (DbUpdateException)
					{
						MessageBox.Show($"Course with \"{course.Name}\" name already exist");
                        Courses.Remove(course);
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
						MessageBox.Show($"Course with \"{course.Name}\" name already exist");
                        _db.Entry(course).Reload();
                        course.OnPropertyChanged("Name");
                    }
                }
            }
            else if (obj is NotifyCollectionChangedEventArgs arg && arg.Action == NotifyCollectionChangedAction.Remove)
            {
                try
                {
					_db.SaveChanges();
				}
                catch (DbUpdateException)
                {
					MessageBox.Show($"You can't remove course that has got groups");
					SetActualDbContext();
                }
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
