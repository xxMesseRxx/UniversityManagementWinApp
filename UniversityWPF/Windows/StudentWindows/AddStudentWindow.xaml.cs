﻿using System;
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

namespace UniversityWPF.Windows.StudentWindows
{
	/// <summary>
	/// Interaction logic for AddStudentWindow.xaml
	/// </summary>
	public partial class AddStudentWindow : Window
	{
		public AddStudentWindow(Student student, IEnumerable<Group> groups)
		{
			InitializeComponent();
			DataContext = student;
			this.GroupsBox.ItemsSource = groups;
		}

		private void BtnSave_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
		}
	}
}
