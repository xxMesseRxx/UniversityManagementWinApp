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
		public void SaveChangesInDb_ChangeNameToCorName_CourseNameWasChangedExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				CourseService courseService = TestServicesCreator.GetCourseService();
				ObservableCollection<Course> courses = courseService.Courses;
				string expected = "New name";

				//Act
				courses[0].Name = expected;
				courseService.SaveChangesInDb(courses[0]);

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
		[ExpectedException(typeof(ArgumentNullException))]
		public void SaveChangesInDb_ChangeNameToEmpty_ArgumentNullExceptionExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				CourseService courseService = TestServicesCreator.GetCourseService();
				ObservableCollection<Course> courses = courseService.Courses;

				//Act
				courses[0].Name = "";
				courseService.SaveChangesInDb(courses[0]);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void SaveChangesInDb_ChangeNameToExist_ArgumentExceptionExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				CourseService courseService = TestServicesCreator.GetCourseService();
				ObservableCollection<Course> courses = courseService.Courses;
				string existName = courses[1].Name;

				//Act
				courses[0].Name = existName;
				courseService.SaveChangesInDb(courses[0]);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}

		[TestMethod]
		public void SaveChangesInDb_AddCourseWithCorName_CourseWasAddedExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				CourseService courseService = TestServicesCreator.GetCourseService();
				ObservableCollection<Course> courses = courseService.Courses;
				string expectedName = "New course 1";
				int expectedCourseCount = 10;
				Course newCourse = new Course { Name = expectedName };

				//Act
				courses.Add(newCourse);
				courseService.SaveChangesInDb(newCourse);

				string actualName = courses.Last().Name;
				int actualCourseCount = courses.Count();

				//Assert
				Assert.AreEqual(expectedName, actualName);
				Assert.AreEqual(expectedCourseCount, actualCourseCount);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void SaveChangesInDb_AddCourseWithEmptyName_ArgumentNullExceptionExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				CourseService courseService = TestServicesCreator.GetCourseService();
				ObservableCollection<Course> courses = courseService.Courses;
				Course newCourse = new Course { Name = "" };

				//Act
				courses.Add(newCourse);
				courseService.SaveChangesInDb(newCourse);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void SaveChangesInDb_AddCourseWithExistName_ArgumentExceptionExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				CourseService courseService = TestServicesCreator.GetCourseService();
				ObservableCollection<Course> courses = courseService.Courses;
				string existName = courses[3].Name;
				Course newCourse = new Course { Name = existName };

				//Act
				courses.Add(newCourse);
				courseService.SaveChangesInDb(newCourse);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}

		[TestMethod]
		public void SaveChangesInDb_RemoveCourseWithoutGroups_8CoursesExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				CourseService courseService = TestServicesCreator.GetCourseService();
				ObservableCollection<Course> courses = courseService.Courses;
				Course removedCourse = courses[2];
				int expected = 8;

				//Act
				courses.Remove(removedCourse);

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
		[ExpectedException(typeof(InvalidOperationException))]
		public void SaveChangesInDb_RemoveCourseWithGroups_InvalidOperationExceptionExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				CourseService courseService = TestServicesCreator.GetCourseService();
				ObservableCollection<Course> courses = courseService.Courses;
				Course removedCourse = courses[5];

				//Act
				courses.Remove(removedCourse);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}
	}
}
