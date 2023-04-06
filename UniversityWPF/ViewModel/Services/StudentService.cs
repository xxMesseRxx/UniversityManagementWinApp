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

		private UniversityContext _db;
        private ObservableCollection<Student> _students;
        private IServiceProvider _serviceProvider;

        public StudentService(IServiceProvider provider)
        {
			_serviceProvider = provider;

			SetActualDbContext();
		}

		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
		public void SaveChangesInDb(object? obj = null)
        {
            if (obj is Student student)
            {
				if (student.StudentId == 0)
				{
					AddStudentSaveChanges(student);
				}
				else
                {
					EditingStudentSaveChanges(student);
                }
            }
            else if (obj is NotifyCollectionChangedEventArgs arg && arg.Action == NotifyCollectionChangedAction.Remove)
            {
				RemoveActionSaveChanges();
			}
        }

		private void AddStudentSaveChanges(Student student)
		{
			if (string.IsNullOrEmpty(student.FirstName))
			{
				Students.Remove(student);
				throw new ArgumentNullException("First name", "You didn't enter a first name");
			}
			else if (string.IsNullOrEmpty(student.LastName))
			{
				Students.Remove(student);
				throw new ArgumentNullException("Last name", "You didn't enter a last name");
			}
			else if (student.GroupId == 0)
			{
				Students.Remove(student);
				throw new ArgumentNullException("Group name", "You didn't choose a group");
			}
			else
			{
				_db.SaveChanges();
				student.OnPropertyChanged("StudentId");
			}
		}
		private void EditingStudentSaveChanges(Student student)
		{
			if (string.IsNullOrEmpty(student.FirstName))
			{
				ReloadEntity(student);
				throw new ArgumentNullException("First name", "You didn't enter a first name");
			}
			else if (string.IsNullOrEmpty(student.LastName))
			{
				ReloadEntity(student);
				throw new ArgumentNullException("Last name", "You didn't enter a last name");
			}
			else
			{
				_db.SaveChanges();
			}
		}
		private void RemoveActionSaveChanges()
		{
			_db.SaveChanges();
		}
		private void ReloadEntity(Student student)
		{
			_db.Entry(student).Reload();
			student.OnPropertyChanged("FirstName");
			student.OnPropertyChanged("LastName");
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
