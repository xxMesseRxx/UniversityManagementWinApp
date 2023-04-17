using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityWPF.Model;
using UniversityWPF.ViewModel.Services;

namespace UniversityWPF.Tests.ViewModelTests
{
	[TestClass]
	public class StudentServiceTests
	{
		[TestMethod]
		public void StudentsCollecton_Get_ListWith400StudentsExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				StudentService studentService = TestServicesCreator.GetStudentService();
				int expected = 400;

				//Act
				ObservableCollection<Student> students = studentService.Students;
				int actual = students.Count();

				//Assert
				Assert.AreEqual(expected, actual);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}

		[TestMethod]
		public void SaveChangesInDb_ChangeFirstName_FirstNameWasChangedExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				StudentService studentService = TestServicesCreator.GetStudentService();
				ObservableCollection<Student> students = studentService.Students;
				string expected = "New first name";

				//Act
				students[0].FirstName = expected;
				studentService.SaveChangesInDb(students[0]);

				string actual = students[0].FirstName;

				//Assert
				Assert.AreEqual(expected, actual);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}
		[TestMethod]
		public void SaveChangesInDb_ChangeLastName_LastNameWasChangedExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				StudentService studentService = TestServicesCreator.GetStudentService();
				ObservableCollection<Student> students = studentService.Students;
				string expected = "New last name";

				//Act
				students[0].LastName = expected;
				studentService.SaveChangesInDb(students[0]);

				string actual = students[0].LastName;

				//Assert
				Assert.AreEqual(expected, actual);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}
		[TestMethod]
		public void SaveChangesInDb_ChangeGroupId_GroupIdWasChangedExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				StudentService studentService = TestServicesCreator.GetStudentService();
				ObservableCollection<Student> students = studentService.Students;
				int expected = 1;

				//Act
				students[0].GroupId = expected;
				studentService.SaveChangesInDb(students[0]);

				int actual = students[0].GroupId;

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
		public void SaveChangesInDb_ChangeFirstNameToEmpty_ArgumentNullExceptionExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				StudentService studentService = TestServicesCreator.GetStudentService();
				ObservableCollection<Student> students = studentService.Students;

				//Act
				students[0].FirstName = "";
				studentService.SaveChangesInDb(students[0]);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void SaveChangesInDb_ChangeLastNameToEmpty_ArgumentNullExceptionExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				StudentService studentService = TestServicesCreator.GetStudentService();
				ObservableCollection<Student> students = studentService.Students;

				//Act
				students[0].LastName = "";
				studentService.SaveChangesInDb(students[0]);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}

		[TestMethod]
		public void SaveChangesInDb_AddStudentWithCorData_401StudentExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				StudentService studentService = TestServicesCreator.GetStudentService();
				ObservableCollection<Student> students = studentService.Students;
				Student newStudent = new Student { FirstName = "New first name", LastName = "New last name", GroupId = 1 };
				int expected = 401;

				//Act
				students.Add(newStudent);
				studentService.SaveChangesInDb(newStudent);

				int actual = students.Count();

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
		public void SaveChangesInDb_AddStudentWithEmptyFirstName_ArgumentNullExceptionExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				StudentService studentService = TestServicesCreator.GetStudentService();
				ObservableCollection<Student> students = studentService.Students;
				Student newStudent = new Student { FirstName = "", LastName = "New last name", GroupId = 1 };

				//Act
				students.Add(newStudent);
				studentService.SaveChangesInDb(newStudent);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void SaveChangesInDb_AddStudentWithEmptyLastName_ArgumentNullExceptionExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				StudentService studentService = TestServicesCreator.GetStudentService();
				ObservableCollection<Student> students = studentService.Students;
				Student newStudent = new Student { FirstName = "New first name", LastName = "", GroupId = 1 };

				//Act
				students.Add(newStudent);
				studentService.SaveChangesInDb(newStudent);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void SaveChangesInDb_AddStudentWithEmptyGroupId_ArgumentNullExceptionExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				StudentService studentService = TestServicesCreator.GetStudentService();
				ObservableCollection<Student> students = studentService.Students;
				Student newStudent = new Student { FirstName = "New first name", LastName = "New last name" };

				//Act
				students.Add(newStudent);
				studentService.SaveChangesInDb(newStudent);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}

		[TestMethod]
		public void SaveChangesInDb_RemoveStudent_399StudentsExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				StudentService studentService = TestServicesCreator.GetStudentService();
				ObservableCollection<Student> students = studentService.Students;
				Student removedStudent = students[5];
				int expected = 399;

				//Act
				students.Remove(removedStudent);

				int actual = students.Count();

				//Assert
				Assert.AreEqual(expected, actual);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}

		[TestMethod]
		public void GetStudentsByGroupId_CorGroupId_StudentsWithGroupIdExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				StudentService studentService = TestServicesCreator.GetStudentService();
				int expectedGroupId = 6;

				//Act
				ObservableCollection<Student> students = studentService.GetStudentsByGroupId(expectedGroupId); ;

				//Assert
				foreach (var item in students)
				{
					int actualGroupId = item.GroupId;
					Assert.AreEqual(expectedGroupId, actualGroupId);
				}
			}
			finally
			{
				dbCreator.Dispose();
			}
		}
		[TestMethod]
		public void GetStudentsByGroupId_UnexistGroupId_0StudentsExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				StudentService studentService = TestServicesCreator.GetStudentService();
				int unexistGroupId = -6;

				//Act
				ObservableCollection<Student> students = studentService.GetStudentsByGroupId(unexistGroupId); ;

				//Assert
				Assert.AreEqual(0, students.Count());
			}
			finally
			{
				dbCreator.Dispose();
			}
		}
	}
}
