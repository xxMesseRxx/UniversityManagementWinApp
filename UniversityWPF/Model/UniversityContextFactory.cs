using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityWPF.Model
{
	public class UniversityContextFactory : IDesignTimeDbContextFactory<UniversityContext>
	{
		public UniversityContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();

			optionsBuilder.UseSqlServer("Server=localhost;Database=University;Trusted_Connection=True;Encrypt=False;");

			return new UniversityContext(optionsBuilder.Options);
		}
	}
}
