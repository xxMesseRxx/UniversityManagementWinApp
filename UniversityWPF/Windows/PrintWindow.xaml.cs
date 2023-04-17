using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UniversityWPF.Model;

namespace UniversityWPF.Windows
{
	/// <summary>
	/// Interaction logic for PrintWindow.xaml
	/// </summary>
	public partial class PrintWindow : Window
	{
		public PrintWindow(IEnumerable<Student> students, string courseName, string groupName)
		{
			InitializeComponent();
			SetPages(students.ToList());
			CourseName.Text = $"Course name: {courseName}";
			GroupName.Text = $"Group name: {groupName}";
		}

		private List<List<Student>> SplitStudentsList(List<Student> students, int maxStudentsInList)
		{
			List<List<Student>> splitedList = new List<List<Student>>();

			for (int i = 0; i < students.Count; i += maxStudentsInList)
			{
				List<Student> newPiece = students.GetRange(i, Math.Min(students.Count - i, maxStudentsInList));
				splitedList.Add(newPiece);
			}

			return splitedList;
		}
		private void SetPages(List<Student> students)
		{
			if (students.Count == 0)
			{
				return;
			}

			List<List<Student>> splitedStudentsForPages = SplitStudentsList(students, 48);
			DataContext = splitedStudentsForPages[0];

			if (splitedStudentsForPages.Count > 1)
			{
				SetStudentsToAnotherPages(splitedStudentsForPages);
			}
		}
		private void SetStudentsToAnotherPages(List<List<Student>> splitedStudentsForPages)
		{
			for (int i = 1; i < splitedStudentsForPages.Count; i++)
			{
				CreateNewPageWithStudents(splitedStudentsForPages[i]);
			}
		}
		private void CreateNewPageWithStudents(List<Student> students)
		{
			PageContent pc = new PageContent();
			FixedPage fp = new FixedPage();
			var studentDataGrid = CreateNewDataGrid(students);
			FixedPage.SetRight(studentDataGrid, 210);
			FixedPage.SetTop(studentDataGrid, 20);
			fp.Children.Add(studentDataGrid);
			((IAddChild)pc).AddChild(fp);
			FxDoc.Pages.Add(pc);
		}
		private DataGrid CreateNewDataGrid(List<Student> students)
		{
			DataGrid dataGrid = new DataGrid();
			dataGrid.ItemsSource = students;
			dataGrid.Style = Resources["DataGridStyle"] as Style;
			dataGrid.CellStyle = Resources["DataGridCellStyle"] as Style;

			dataGrid.Columns.Add(new DataGridTextColumn()
			{
				Header = "First name",
				Binding = new Binding("FirstName"),
				HeaderStyle = Resources["DataGridColumnHeaderStyle"] as Style
			});
			dataGrid.Columns.Add(new DataGridTextColumn()
			{
				Header = "Last name",
				Binding = new Binding("LastName"),
				HeaderStyle = Resources["DataGridColumnHeaderStyle"] as Style
			});

			return dataGrid;
		}
	}
}
