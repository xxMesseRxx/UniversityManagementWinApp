using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UniversityWPF.Model;

public class Student : INotifyPropertyChanged
{
    public int StudentId
	{
		get { return _studentId; }
		set
		{
			_studentId = value;
			OnPropertyChanged();
		}
	}
    public int GroupId
	{
		get { return _groupId; }
		set
		{
			_groupId = value;
			OnPropertyChanged();
		}
	}
    public string FirstName
	{
		get { return _firstName; }
		set
		{
			_firstName = value;
			OnPropertyChanged();
		}
	}
	public string LastName
	{
		get { return _lastName; }
		set
		{
			_lastName = value;
			OnPropertyChanged();
		}
	}
	public Group Group
	{
		get { return _group; }
		set
		{
			_group = value;
			OnPropertyChanged();
		}
	}

	private int _studentId;
	private int _groupId;
	private string _firstName = null!;
	private string _lastName = null!;
	private Group _group = null!;

	public event PropertyChangedEventHandler? PropertyChanged;
	public void OnPropertyChanged([CallerMemberName] string prop = "")
	{
		if (PropertyChanged != null)
			PropertyChanged(this, new PropertyChangedEventArgs(prop));
	}
}
