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
    public class StudentService : IStudentService, INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler? PropertyChanged;

		public ObservableCollection<Student> Students
		{ 
            get
            {
                return _students;
            }
            set
            {
				_students = value;
                OnPropertyChanged();
            }
        }
		public RelayCommand SaveChangesCommand { get { return _saveChangesCommand; } }

		private RelayCommand _saveChangesCommand;
		private UniversityContext _db;
        private ObservableCollection<Student> _students;
        private IServiceProvider _serviceProvider;

        public StudentService(IServiceProvider provider)
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
            if (obj is Student student)
            {
                if (string.IsNullOrEmpty(student.FirstName))
                {
					MessageBox.Show("You didn't enter a first name");
					Students.Remove(student);
				}
				else if (string.IsNullOrEmpty(student.LastName))
				{
					MessageBox.Show("You didn't enter a last name");
					Students.Remove(student);
				}
				else if (student.GroupId == 0)
				{
					MessageBox.Show("You didn't choose a course");
					Students.Remove(student);
				}
				else if (student.StudentId == 0)
				{
					_db.SaveChanges();
					student.OnPropertyChanged("StudentId");
				}
				else
                {
					_db.SaveChanges();
                }
            }
            else if (obj is NotifyCollectionChangedEventArgs arg && arg.Action == NotifyCollectionChangedAction.Remove)
            {
				_db.SaveChanges();
            }
        }
        private void SetActualDbContext()
        {
			_db = _serviceProvider.GetRequiredService<UniversityContext>();
            _db.Students.Load();
			Students = _db.Students.Local.ToObservableCollection();
			Students.CollectionChanged += (sender, e) => { SaveChangesInDb(e); };
		}
	}
}
