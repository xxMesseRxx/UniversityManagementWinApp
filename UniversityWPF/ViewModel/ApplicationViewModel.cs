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
using UniversityWPF.ViewModel.Services;

namespace UniversityWPF.ViewModel
{
    public class ApplicationViewModel
    {
        public ICourseService CourseService { get; }
		public IGroupService GroupService { get; }
		public IStudentService StudentService { get; }
        public RelayCommand CourseSaveChangesCommand 
        {
            get
            {
                return _courseSaveChangesCommand ??
                    (_courseSaveChangesCommand = new RelayCommand((obj) =>
                    {
                        try
                        {
                            CourseService.SaveChangesInDb(obj);
                        }
                        catch (ArgumentNullException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        catch (ArgumentException ex)
                        {
							MessageBox.Show(ex.Message);
						}
                    }));
            }
        }
		public RelayCommand GroupSaveChangesCommand
		{
			get
			{
				return _groupSaveChangesCommand ??
					(_groupSaveChangesCommand = new RelayCommand((obj) =>
					{
						try
						{
							GroupService.SaveChangesInDb(obj);
						}
						catch (ArgumentNullException ex)
						{
							MessageBox.Show(ex.Message);
						}
						catch (ArgumentException ex)
						{
							MessageBox.Show(ex.Message);
						}
					}));
			}
		}
		public RelayCommand StudentSaveChangesCommand
		{
			get
			{
				return _studentSaveChangesCommand ??
					(_studentSaveChangesCommand = new RelayCommand((obj) =>
					{
						try
						{
							StudentService.SaveChangesInDb(obj);
						}
						catch (ArgumentNullException ex)
						{
							MessageBox.Show(ex.Message);
						}
						catch (ArgumentException ex)
						{
							MessageBox.Show(ex.Message);
						}
					}));
			}
		}

		private RelayCommand _courseSaveChangesCommand;
		private RelayCommand _groupSaveChangesCommand;
		private RelayCommand _studentSaveChangesCommand;

		public ApplicationViewModel(ICourseService courseService,
                                    IGroupService groupService,
                                    IStudentService studentService) 
        {
            CourseService = courseService;
            GroupService = groupService;
            StudentService = studentService;
        }
    }
}
