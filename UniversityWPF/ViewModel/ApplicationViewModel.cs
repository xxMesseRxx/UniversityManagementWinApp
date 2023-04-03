using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityWPF.Library;
using UniversityWPF.Model;
using System.Windows;
using System.Collections.Specialized;
using UniversityWPF.Library.Interfaces;

namespace UniversityWPF.ViewModel
{
    public class ApplicationViewModel
    {
		public ICourseService CourseService { get; }
		public IGroupService GroupService { get; }

		public ApplicationViewModel(ICourseService courseService, IGroupService groupService) 
        {
            CourseService = courseService;
            GroupService = groupService;
        }
    }
}
