using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityWPF.Model;

namespace UniversityWPF.Library.Interfaces
{
	public interface ICourseService
	{
		public ObservableCollection<Course> GetAll();
		public void SaveChanges(object obj);
	}
}
