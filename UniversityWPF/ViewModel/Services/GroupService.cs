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

		private UniversityContext _db;
        private ObservableCollection<Group> _groups;
        private IServiceProvider _serviceProvider;

        public GroupService(IServiceProvider provider)
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
            if (obj is Group group)
            {               
				if (group.GroupId == 0)
                {
					AddGroupSaveChanges(group);
				}
                else
                {
					EditingGroupSaveChanges(group);
				}
            }
            else if (obj is NotifyCollectionChangedEventArgs arg && arg.Action == NotifyCollectionChangedAction.Remove)
            {
				RemoveActionSaveChanges();
			}
        }

		private void AddGroupSaveChanges(Group group)
		{
			if (string.IsNullOrEmpty(group.Name))
			{
				Groups.Remove(group);
				throw new ArgumentNullException("Group name", "You didn't enter a group name");
			}
			else if (group.CourseId == 0)
			{
				Groups.Remove(group);
				throw new ArgumentNullException("Course name", "You didn't choose a course");
			}
			else
			{
				try
				{
					_db.SaveChanges();
					group.OnPropertyChanged("GroupId");
				}
				catch (DbUpdateException)
				{
					Groups.Remove(group);
					throw new ArgumentException($"Group with \"{group.Name}\" name already exist");
				}
			}
		}
		private void EditingGroupSaveChanges(Group group)
		{
			if (string.IsNullOrEmpty(group.Name))
			{
				ReloadEntity(group);
				throw new ArgumentNullException("Group name", "You didn't enter a group name");
			}
			else
			{
				try
				{
					_db.SaveChanges();
				}
				catch (DbUpdateException)
				{
					string oldName = group.Name;
					ReloadEntity(group);
					throw new ArgumentException($"Group with \"{oldName}\" name already exist");
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
		private void ReloadEntity(Group group)
		{
			_db.Entry(group).Reload();
			group.OnPropertyChanged("Name");
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
