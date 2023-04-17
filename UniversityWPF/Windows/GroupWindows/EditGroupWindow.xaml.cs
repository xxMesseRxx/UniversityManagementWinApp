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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UniversityWPF.Model;

namespace UniversityWPF.Windows.GroupWindows
{
	/// <summary>
	/// Interaction logic for EditGroupWindow.xaml
	/// </summary>
	public partial class EditGroupWindow : Window
	{
		public EditGroupWindow(Group group, IEnumerable<Course> courses)
		{
			InitializeComponent();
			DataContext = group;
			this.CoursesBox.ItemsSource = courses;
		}

		private void BtnSave_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
		}
	}
}
