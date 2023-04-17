using Microsoft.EntityFrameworkCore;
using UniversityWPF.Model;

namespace UniversityWPF.Tests
{
	public class TestDBCreator : IDisposable
	{
		private UniversityContext? _db;

		public void CreateTestDB()
		{
			var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();
			optionsBuilder.UseSqlServer("Server=localhost;Database=TestUniversity;Trusted_Connection=True;Encrypt=False;");

			_db = new UniversityContext(optionsBuilder.Options);
			
			_db?.Database.EnsureCreated();
		}
		public void Dispose()
		{
			_db?.Database.EnsureDeleted();
		}
	}
}
