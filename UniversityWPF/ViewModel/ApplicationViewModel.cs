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
using UniversityWPF.ViewModel.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UniversityWPF.ViewModel
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler? PropertyChanged;

		public ObservableCollection<Group> GroupsWithCourseId
		{
			get
			{
				return _groupsWithCourseId;
			}
			set
			{
				_groupsWithCourseId = value;
				OnPropertyChanged();
			}
		}
		public ObservableCollection<Student> StudentsWithGroupId
		{
			get
			{
				return _studentsWithGroupId;
			}
			set
			{
				_studentsWithGroupId = value;
				OnPropertyChanged();
			}
		}
		public ICourseService CourseService { get; }
		public IGroupService GroupService { get; }
		public IStudentService StudentService { get; }
        public RelayCommand CourseSaveChangesCommand 
        {
            get
            {
                return _courseSaveChangesCommand ??
                    (_courseSaveChangesCommand = new RelayCommand((obj) =>
                    {
                        try
                        {
                            CourseService.SaveChangesInDb(obj);
                        }
                        catch (ArgumentNullException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        catch (ArgumentException ex)
                        {
							MessageBox.Show(ex.Message);
						}
                    }));
            }
        }
		public RelayCommand GroupSaveChangesCommand
		{
			get
			{
				return _groupSaveChangesCommand ??
					(_groupSaveChangesCommand = new RelayCommand((obj) =>
					{
						try
						{
							GroupService.SaveChangesInDb(obj);
						}
						catch (ArgumentNullException ex)
						{
							MessageBox.Show(ex.Message);
						}
						catch (ArgumentException ex)
						{
							MessageBox.Show(ex.Message);
						}
					}));
			}
		}
		public RelayCommand SetGroupsByCourseIdCommand
		{
			get
			{
				return _setGroupsByCourseIdCommand ??
					(_setGroupsByCourseIdCommand = new RelayCommand((courseId) =>
					{
						if (courseId is int id)
						{
							GroupsWithCourseId = GroupService.GetGroupsByCourseId(id);
						}
					}));
			}
		}
		public RelayCommand StudentSaveChangesCommand
		{
			get
			{
				return _studentSaveChangesCommand ??
					(_studentSaveChangesCommand = new RelayCommand((obj) =>
					{
						try
						{
							StudentService.SaveChangesInDb(obj);
						}
						catch (ArgumentNullException ex)
						{
							MessageBox.Show(ex.Message);
						}
						catch (ArgumentException ex)
						{
							MessageBox.Show(ex.Message);
						}
					}));
			}
		}
		public RelayCommand SetStudentsByGroupIdCommand
		{
			get
			{
				return _setStudentsByGroupIdCommand ??
					(_setStudentsByGroupIdCommand = new RelayCommand((groupId) =>
					{
						if (groupId is int id)
						{
							StudentsWithGroupId = StudentService.GetStudentsByGroupId(id);
						}
					}));
			}
		}

		private ObservableCollection<Group> _groupsWithCourseId;
		private ObservableCollection<Student> _studentsWithGroupId;
		private RelayCommand _courseSaveChangesCommand;
		private RelayCommand _groupSaveChangesCommand;
		private RelayCommand _setGroupsByCourseIdCommand;
		private RelayCommand _studentSaveChangesCommand;
		private RelayCommand _setStudentsByGroupIdCommand;

		public ApplicationViewModel(ICourseService courseService,
                                    IGroupService groupService,
                                    IStudentService studentService) 
        {
            CourseService = courseService;
            GroupService = groupService;
            StudentService = studentService;
        }

		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
