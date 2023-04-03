using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UniversityWPF.Model;
using UniversityWPF.Library.Interfaces;
using UniversityWPF.ViewModel;
using UniversityWPF.ViewModel.Services;
using Microsoft.EntityFrameworkCore;

namespace UniversityWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
	{
		private readonly IHost _host;

		public App()
		{
			_host = Host.CreateDefaultBuilder()
			.ConfigureServices(services =>
			{
				services.AddSingleton<MainWindow>();
				services.AddDbContext<UniversityContext>(options =>
														 options.UseSqlServer("Server=localhost;Database=University;Trusted_Connection=True;Encrypt=False;"),
														 ServiceLifetime.Transient);
				services.AddSingleton<ICourseService, CourseService>();
				services.AddSingleton<IGroupService, GroupService>();
				services.AddSingleton<ApplicationViewModel>();
			})
			.Build();
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			_host.Start();

			MainWindow = _host.Services.GetRequiredService<MainWindow>();
			MainWindow.Show();

			base.OnStartup(e);
		}

		protected override void OnExit(ExitEventArgs e)
		{
			_host.StopAsync();
			_host.Dispose();

			base.OnExit(e);
		}
	}
}
