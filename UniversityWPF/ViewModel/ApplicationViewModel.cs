using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityWPF.Model;

namespace UniversityWPF.ViewModel
{
    class ApplicationViewModel
    {
        private UniversityContext _db = new UniversityContext();
        public ObservableCollection<Course> Courses { get; set; }

        public ApplicationViewModel() 
        {
            _db.Courses.Load();
            Courses = _db.Courses.Local.ToObservableCollection();
        }
    }
}
