using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityWPF.Model;
using UniversityWPF.ViewModel.Services;

namespace UniversityWPF.Tests
{
	public static class TestServicesCreator
	{
		private static IServiceCollection _services;

		static TestServicesCreator()
		{
			_services = new ServiceCollection()
							.AddDbContext<UniversityContext>(options =>
															 options.UseSqlServer("Server=localhost;Database=TestUniversity;Trusted_Connection=True;Encrypt=False;"),
															 ServiceLifetime.Scoped);
		}

		public static CourseService GetCourseService()
		{
			return new CourseService(_services.BuildServiceProvider());
		}
		public static GroupService GetGroupService()
		{
			return new GroupService(_services.BuildServiceProvider());
		}
		public static StudentService GetStudentService()
		{
			return new StudentService(_services.BuildServiceProvider());
		}
	}
}
