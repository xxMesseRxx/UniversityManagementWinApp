using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityWPF.Model;

namespace UniversityWPF.Library.Interfaces
{
    public interface IGroupService
    {
        ObservableCollection<Group> Groups { get; set; }

        void SaveChangesInDb(object? obj = null);
        ObservableCollection<Group> GetGroupsByCourseId(int courseId);
	}
}
