using System.Windows;
using UniversityWPF.Model;

namespace UniversityWPF.Windows.CourseWindows
{
	/// <summary>
	/// Interaction logic for EditCourse.xaml
	/// </summary>
	public partial class EditCourseWindow : Window
	{
		public string CourseName
		{
			get { return NameBox.Text; }
		}
		public string CourseDescription
		{
			get { return DescriptionBox.Text; }
		}

		public EditCourseWindow(Course course)
		{
			InitializeComponent();
			DataContext = course;
		}

		private void BtnSave_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
		}
	}
}
