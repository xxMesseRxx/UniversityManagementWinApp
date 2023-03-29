using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UniversityWPF.Model;

public class Group : INotifyPropertyChanged
{
    public int GroupId
	{
		get { return _groupId; }
		set
		{
			_groupId = value;
			OnPropertyChanged();
		}
	}
    public int CourseId
	{
		get { return _courseId; }
		set
		{
			_courseId = value;
			OnPropertyChanged();
		}
	}
    public string Name
	{
		get { return _name; }
		set
		{
			_name = value;
			OnPropertyChanged();
		}
	}
    public Course Course
	{
		get { return _course; }
		set
		{
			_course = value;
			OnPropertyChanged();
		}
	}
    public List<Student> Students
	{
		get { return _students; }
		set
		{
			_students = value;
			OnPropertyChanged();
		}
	}

	private int _groupId;
	private int _courseId;
	private string _name = null!;
	private Course _course = null!;
	private List<Student> _students = new List<Student>();

	public event PropertyChangedEventHandler? PropertyChanged;
	public void OnPropertyChanged([CallerMemberName] string prop = "")
	{
		if (PropertyChanged != null)
			PropertyChanged(this, new PropertyChangedEventArgs(prop));
	}
}
