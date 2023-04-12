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
using UniversityWPF.Windows.CourseWindows;
using UniversityWPF.Windows.GroupWindows;

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
		public RelayCommand AddCourseCommand
		{
			get
			{
				return _addCourseCommand ??
					(_addCourseCommand = new RelayCommand((obj) =>
					{
						Course newCourse = new Course();
						AddCourseWindow addCourseWindow = new AddCourseWindow(newCourse);

						if (addCourseWindow.ShowDialog() is true)
						{
							CourseService.Courses.Add(newCourse);
							CourseSaveChanges(newCourse);
						}
					}));
			}
		}
		public RelayCommand EditCourseCommand
		{
			get
			{
				return _editCourseCommand ??
					(_editCourseCommand = new RelayCommand((course) =>
					{
						if (course is Course c)
						{
							EditCourseWindow editCourseWindow = new EditCourseWindow(c);

							if (editCourseWindow.ShowDialog() is true)
							{
								CourseSaveChanges(c);
							}
						}
					}));
			}
		}
		public RelayCommand RemoveCourseCommand
		{
			get
			{
				return _removeCourseCommand ??
					(_removeCourseCommand = new RelayCommand((course) =>
					{
						if (course is Course c)
						{
							try
							{
								CourseService.Courses.Remove(c);
							}
							catch (InvalidOperationException ex)
							{
								MessageBox.Show(ex.Message);
							}		
						}
					}));
			}
		}
		public RelayCommand AddGroupCommand
		{
			get
			{
				return _addGroupCommand ??
					(_addGroupCommand = new RelayCommand((obj) =>
					{
						Group newGroup = new Group();
						AddGroupWindow addGroupWindow = new AddGroupWindow(newGroup, CourseService.Courses);

						if (addGroupWindow.ShowDialog() is true)
						{
							GroupService.Groups.Add(newGroup);
							GroupSaveChanges(newGroup);
						}
					}));
			}
		}
		public RelayCommand EditGroupCommand
		{
			get
			{
				return _editGroupCommand ??
					(_editGroupCommand = new RelayCommand((group) =>
					{
						if (group is Group g)
						{
							EditGroupWindow editGroupWindow = new EditGroupWindow(g, CourseService.Courses);

							if (editGroupWindow.ShowDialog() is true)
							{
								GroupSaveChanges(g);
							}
						}
					}));
			}
		}
		public RelayCommand RemoveGroupCommand
		{
			get
			{
				return _removeGroupCommand ??
					(_removeGroupCommand = new RelayCommand((group) =>
					{
						if (group is Group g)
						{
							try
							{
								GroupService.Groups.Remove(g);
							}
							catch (InvalidOperationException ex)
							{
								MessageBox.Show(ex.Message);
							}
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
		private RelayCommand _addCourseCommand;
		private RelayCommand _editCourseCommand;
		private RelayCommand _removeCourseCommand;
		private RelayCommand _addGroupCommand;
		private RelayCommand _editGroupCommand;
		private RelayCommand _removeGroupCommand;
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

		private void CourseSaveChanges(Course course)
		{
			try
			{
				CourseService.SaveChangesInDb(course);
			}
			catch (ArgumentNullException ex)
			{
				MessageBox.Show(ex.Message);
			}
			catch (ArgumentException ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void GroupSaveChanges(Group group)
		{
			try
			{
				GroupService.SaveChangesInDb(group);
			}
			catch (ArgumentNullException ex)
			{
				MessageBox.Show(ex.Message);
			}
			catch (ArgumentException ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
