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
    public class GroupService : IGroupService, INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler? PropertyChanged;

		public ObservableCollection<Group> Groups
        { 
            get
            {
                return _groups;
            }
            set
            {
				_groups = value;
                OnPropertyChanged();
            }
        }
		public RelayCommand SaveChangesCommand { get { return _saveChangesCommand; } }

		private RelayCommand _saveChangesCommand;
		private UniversityContext _db;
        private ObservableCollection<Group> _groups;
        private IServiceProvider _serviceProvider;

        public GroupService(IServiceProvider provider)
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
            if (obj is Group group)
            {
                if (string.IsNullOrEmpty(group.Name))
                {
					MessageBox.Show("Please enter a name");
					Groups.Remove(group);
				}
				else if (group.CourseId == 0)
				{
					MessageBox.Show("Please choose a course");
					Groups.Remove(group);
				}
				else if (group.GroupId == 0)
                {
					try
					{
						_db.SaveChanges();
						group.OnPropertyChanged("GroupId");
					}
					catch (DbUpdateException)
					{
						MessageBox.Show($"Group with \"{group.Name}\" name already exist");
                        Groups.Remove(group);
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
						MessageBox.Show($"Group with \"{group.Name}\" name already exist");
                        _db.Entry(group).Reload();
						group.OnPropertyChanged("Name");
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
					MessageBox.Show($"You can't remove group that has got students");
					SetActualDbContext();
                }
            }
        }
        private void SetActualDbContext()
        {
			_db = _serviceProvider.GetRequiredService<UniversityContext>();
            _db.Groups.Load();
			Groups = _db.Groups.Local.ToObservableCollection();
			Groups.CollectionChanged += (sender, e) => { SaveChangesInDb(e); };
		}
	}
}
