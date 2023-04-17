using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UniversityWPF.Model;

public class Course : INotifyPropertyChanged
{
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
    public string? Description
	{
		get { return _description; }
		set
		{
			_description = value;
			OnPropertyChanged();
		}
	}
    public List<Group> Groups 
	{
		get { return _groups; }
		set
		{
			_groups = value;
			OnPropertyChanged();
		}
	}

    private int _courseId;
	private string _name = null!;
    private string? _description;
	private List<Group> _groups = new List<Group>();

	public event PropertyChangedEventHandler? PropertyChanged;
	public void OnPropertyChanged([CallerMemberName] string prop = "")
	{
		if (PropertyChanged != null)
			PropertyChanged(this, new PropertyChangedEventArgs(prop));
	}
}
