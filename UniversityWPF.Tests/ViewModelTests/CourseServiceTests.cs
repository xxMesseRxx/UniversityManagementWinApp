using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityWPF.Library;
using UniversityWPF.Model;
using UniversityWPF.ViewModel.Services;

namespace UniversityWPF.Tests.ViewModelTests
{
	[TestClass]
	public class CourseServiceTests
	{
		[TestMethod]
		public void CoursesCollecton_Get_ListWith9CoursesExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				CourseService courseService = TestServicesCreator.GetCourseService();
				int expected = 9;

				//Act
				ObservableCollection<Course> courses = courseService.Courses;
				int actual = courses.Count();

				//Assert
				Assert.AreEqual(expected, actual);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}

		[TestMethod]
		public void SaveChangesCommand_ChangeNameToCorName_CourseNameWasChangedExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				CourseService courseService = TestServicesCreator.GetCourseService();
				RelayCommand saveCmd = courseService.SaveChangesCommand;
				ObservableCollection<Course> courses = courseService.Courses;
				string expected = "New name";

				//Act
				courses[0].Name = expected;
				saveCmd.Execute(courses[0]);

				string actual = courses[0].Name;

				//Assert
				Assert.AreEqual(expected, actual);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}
		[TestMethod]
		public void SaveChangesCommand_ChangeNameToEmpty_CourseNameWasNotChangedExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				CourseService courseService = TestServicesCreator.GetCourseService();
				RelayCommand saveCmd = courseService.SaveChangesCommand;
				ObservableCollection<Course> courses = courseService.Courses;
				string expected = courses[0].Name;

				//Act
				courses[0].Name = "";
				saveCmd.Execute(courses[0]);

				string actual = courses[0].Name;

				//Assert
				Assert.AreEqual(expected, actual);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}
	}
}
