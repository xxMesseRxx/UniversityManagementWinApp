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
using UniversityWPF.Windows.StudentWindows;
using Microsoft.Win32;
using System.IO;
using CsvHelper;
using System.Globalization;
using UniversityWPF.Windows;

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
		public ObservableCollection<Student> StudentsForPrint
		{
			get
			{
				return _studentsForPrint;
			}
			set
			{
				_studentsForPrint = value;
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
							Course temporaryCourse = new Course { CourseId = c.CourseId, Name = c.Name, Description = c.Description };
							EditCourseWindow editCourseWindow = new EditCourseWindow(temporaryCourse);

							if (editCourseWindow.ShowDialog() is true)
							{
								c.Name = temporaryCourse.Name;
								c.Description = temporaryCourse.Description;
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
							Group temporaryGroup = new Group { GroupId = g.GroupId, Name = g.Name, CourseId = g.CourseId };
							EditGroupWindow editGroupWindow = new EditGroupWindow(temporaryGroup, CourseService.Courses);

							if (editGroupWindow.ShowDialog() is true)
							{
								g.Name = temporaryGroup.Name;
								g.CourseId = temporaryGroup.CourseId;
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
		public RelayCommand AddStudentCommand
		{
			get
			{
				return _addStudentCommand ??
					(_addStudentCommand = new RelayCommand((obj) =>
					{
						Student newStudent = new Student();
						AddStudentWindow addStudentWindow = new AddStudentWindow(newStudent, GroupService.Groups);

						if (addStudentWindow.ShowDialog() is true)
						{
							StudentService.Students.Add(newStudent);
							StudentSaveChanges(newStudent);
						}
					}));
			}
		}
		public RelayCommand EditStudentCommand
		{
			get
			{
				return _editStudentCommand ??
					(_editStudentCommand = new RelayCommand((student) =>
					{
						if (student is Student s)
						{
							Student temporaryStudent = new Student { StudentId = s.StudentId, FirstName = s.FirstName,
																	LastName = s.LastName, GroupId = s.GroupId};
							EditStudentWindow editStudentWindow = new EditStudentWindow(temporaryStudent, GroupService.Groups);

							if (editStudentWindow.ShowDialog() is true)
							{
								s.FirstName = temporaryStudent.FirstName;
								s.LastName = temporaryStudent.LastName;
								s.GroupId = temporaryStudent.GroupId;
								StudentSaveChanges(s);
							}
						}
					}));
			}
		}
		public RelayCommand RemoveStudentCommand
		{
			get
			{
				return _removeStudentCommand ??
					(_removeStudentCommand = new RelayCommand((student) =>
					{
						if (student is Student s)
						{
							StudentService.Students.Remove(s);
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
		public RelayCommand ExportGroupCommand
		{
			get
			{
				return _exportGroupCommand ??
					(_exportGroupCommand = new RelayCommand((group) =>
					{
						if (group is Group g)
						{
							string path = GetPathToSave("*.csv", g.Name);

							if (!string.IsNullOrEmpty(path))
							{
								WriteGroupToCsvFile(path, g);
							}
						}
					}));
			}
		}
		public RelayCommand ImportGroupCommand
		{
			get
			{
				return _importGroupCommand ??
					(_importGroupCommand = new RelayCommand((group) =>
					{
						if (group is Group g)
						{
							string path = GetPathToOpen("*.csv", g.Name);

							if (!string.IsNullOrEmpty(path))
							{
								ReadGroupFromCsvFile(path, g);
							}
						}
					}));
			}
		}
		public RelayCommand OpenPrintWindowCommand
		{
			get
			{
				return _openPrintWindowCommand ??
					(_openPrintWindowCommand = new RelayCommand((group) =>
					{
						if (group is Group g)
						{
							PrintWindow printWindow = new PrintWindow(StudentService.GetStudentsByGroupId(g.GroupId), 
																	  CourseService.Courses.First(c => c.CourseId == g.CourseId).Name,
																	  g.Name);
							printWindow.ShowDialog();							
						}
					}));
			}
		}

		private ObservableCollection<Student> _studentsForPrint;
		private ObservableCollection<Group> _groupsWithCourseId;
		private ObservableCollection<Student> _studentsWithGroupId;
		private RelayCommand _addCourseCommand;
		private RelayCommand _editCourseCommand;
		private RelayCommand _removeCourseCommand;
		private RelayCommand _addGroupCommand;
		private RelayCommand _editGroupCommand;
		private RelayCommand _removeGroupCommand;
		private RelayCommand _setGroupsByCourseIdCommand;
		private RelayCommand _addStudentCommand;
		private RelayCommand _editStudentCommand;
		private RelayCommand _removeStudentCommand;
		private RelayCommand _setStudentsByGroupIdCommand;
		private RelayCommand _exportGroupCommand;
		private RelayCommand _importGroupCommand;
		private RelayCommand _openPrintWindowCommand;

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
		private void StudentSaveChanges(Student student)
		{
			try
			{
				StudentService.SaveChangesInDb(student);
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
		private string GetPathToSave(string fileType, string defFileName = "")
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = $"({fileType})|{fileType}";
			saveFileDialog.FileName = defFileName;

			if (saveFileDialog.ShowDialog() is true)
			{
				return saveFileDialog.FileName;
			}

			return "";
		}
		private string GetPathToOpen(string fileType, string defFileName = "")
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = $"({fileType})|{fileType}";
			openFileDialog.FileName = defFileName;

			if (openFileDialog.ShowDialog() is true)
			{
				return openFileDialog.FileName;
			}

			return "";
		}
		private void WriteGroupToCsvFile(string path, Group group)
		{
			using (StreamWriter streamWriter = new StreamWriter(path))
			{
				using (CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
				{
					csvWriter.Context.RegisterClassMap<StudentClassMap>();
					csvWriter.WriteRecords(StudentService.GetStudentsByGroupId(group.GroupId));
				}
			}
		}
		private void ReadGroupFromCsvFile(string path, Group group)
		{
			using (StreamReader streamReader = new StreamReader(path))
			{
				using (CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
				{
					csvReader.Context.RegisterClassMap<StudentClassMap>();
					var students = csvReader.GetRecords<Student>();

					foreach (var item in StudentService.GetStudentsByGroupId(group.GroupId))
					{
						StudentService.Students.Remove(item);
					}

					foreach (var student in students)
					{
						student.GroupId = group.GroupId;
						StudentService.Students.Add(student);
						StudentService.SaveChangesInDb(student);
					}
				}
			}
		}
	}
}
