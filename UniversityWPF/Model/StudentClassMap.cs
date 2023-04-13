using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityWPF.Model
{
	public sealed class StudentClassMap : ClassMap<Student>
	{
		public StudentClassMap() 
		{
			Map(m => m.StudentId).Ignore();
			Map(m => m.FirstName).Index(0).Name("First name");
			Map(m => m.LastName).Index(1).Name("Last Name");
			Map(m => m.GroupId).Ignore();
			Map(m => m.Group).Ignore();
		}
	}
}
