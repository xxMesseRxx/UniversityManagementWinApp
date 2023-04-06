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
	public class GroupServiceTests
	{
		[TestMethod]
		public void GroupCollecton_Get_ListWith30GroupsExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				GroupService groupService = TestServicesCreator.GetGroupService();
				int expected = 30;

				//Act
				ObservableCollection<Group> groups = groupService.Groups;
				int actual = groups.Count();

				//Assert
				Assert.AreEqual(expected, actual);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}

		[TestMethod]
		public void SaveChangesInDb_ChangeNameToCorName_GroupNameWasChangedExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				GroupService groupService = TestServicesCreator.GetGroupService();
				ObservableCollection<Group> groups = groupService.Groups;
				string expected = "New name";

				//Act
				groups[0].Name = expected;
				groupService.SaveChangesInDb(groups[0]);

				string actual = groups[0].Name;

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
				GroupService groupService = TestServicesCreator.GetGroupService();
				ObservableCollection<Group> groups = groupService.Groups;

				//Act
				groups[0].Name = "";
				groupService.SaveChangesInDb(groups[0]);
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
				GroupService groupService = TestServicesCreator.GetGroupService();
				ObservableCollection<Group> groups = groupService.Groups;
				string existName = groups[1].Name;

				//Act
				groups[0].Name = existName;
				groupService.SaveChangesInDb(groups[0]);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}

		[TestMethod]
		public void SaveChangesInDb_AddGroupWithCorName_GroupWasAddedExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				GroupService groupService = TestServicesCreator.GetGroupService();
				ObservableCollection<Group> groups = groupService.Groups;
				string expectedName = "New group 1";
				int expectedGroupCount = 31;
				Group newGroup = new Group { Name = expectedName, CourseId = 1 };

				//Act
				groups.Add(newGroup);
				groupService.SaveChangesInDb(newGroup);

				string actualName = groups.Last().Name;
				int actualGroupCount = groups.Count();

				//Assert
				Assert.AreEqual(expectedName, actualName);
				Assert.AreEqual(expectedGroupCount, actualGroupCount);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void SaveChangesInDb_AddGroupWithEmptyName_ArgumentNullExceptionExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				GroupService groupService = TestServicesCreator.GetGroupService();
				ObservableCollection<Group> groups = groupService.Groups;
				Group newGroup = new Group { Name = "", CourseId = 1 };

				//Act
				groups.Add(newGroup);
				groupService.SaveChangesInDb(newGroup);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void SaveChangesInDb_AddGroupWithEmptyCourseId_ArgumentNullExceptionExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				GroupService groupService = TestServicesCreator.GetGroupService();
				ObservableCollection<Group> groups = groupService.Groups;
				Group newGroup = new Group { Name = "New group"};

				//Act
				groups.Add(newGroup);
				groupService.SaveChangesInDb(newGroup);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void SaveChangesInDb_AddGroupWithExistName_ArgumentExceptionExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				GroupService groupService = TestServicesCreator.GetGroupService();
				ObservableCollection<Group> groups = groupService.Groups;
				string existName = groups[1].Name;
				Group newGroup = new Group { Name = existName, CourseId = 1 };

				//Act
				groups.Add(newGroup);
				groupService.SaveChangesInDb(newGroup);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}

		[TestMethod]
		public void SaveChangesInDb_RemoveGroupWithoutStudents_30GroupsExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				GroupService groupService = TestServicesCreator.GetGroupService();
				ObservableCollection<Group> groups = groupService.Groups;
				Group newGroup = new Group { Name = "New group 1", CourseId = 1 };
				groups.Add(newGroup);
				groupService.SaveChangesInDb(newGroup);
				Group removedGroup = groups.Last();
				int expected = 30;

				//Act
				groups.Remove(removedGroup);
				groupService.SaveChangesInDb(newGroup);

				int actual = groups.Count();

				//Assert
				Assert.AreEqual(expected, actual);
			}
			finally
			{
				dbCreator.Dispose();
			}
		}
		[TestMethod]
		public void SaveChangesInDb_RemoveGroupWithStudents_GroupWasNotRemovedExpected()
		{
			var dbCreator = new TestDBCreator();
			try
			{
				//Arrange
				dbCreator.CreateTestDB();
				GroupService groupService = TestServicesCreator.GetGroupService();
				ObservableCollection<Group> groups = groupService.Groups;
				Group removedGroup = groups[1];
				int expected = 30;

				//Act
				groups.Remove(removedGroup);

				groups = groupService.Groups;
				int actual = groups.Count();

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
