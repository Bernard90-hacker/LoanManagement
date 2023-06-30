using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace LoanManagement.Data.SqlServer.Repositories.Users_Management
{
	public class ApplicationRepository : Repository<Application>, IApplicationRepository
	{
		private LoanManagementDbContext _context;
		public ApplicationRepository(LoanManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<PagedList<Application>> GetAll(ApplicationParameters parameters)
		{
			var apps = (await _context.Applications
				.ToListAsync()).AsQueryable();

			return PagedList<Application>.ToPagedList(
			apps, parameters.PageNumber, parameters.PageSize);
		}

		public async Task<IEnumerable<Application>> GetAll()
		{
			return await _context.Applications.ToListAsync();
		}

		public async Task<Application?> GetApplicationByCode(string code)
		{
			return await _context.Applications.Where(
				x => x.Code == code).FirstOrDefaultAsync();
		}

		public async Task<Application?> GetApplicationById(int id)
		{
			return await _context.Applications
					.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
		}
		public async Task<IEnumerable<Application>?> GetApplicationByStatus(int statut)
		{
			return await _context.Applications
					.Where(x => x.Statut.Equals(statut)).ToListAsync();
		}

		public async Task<IEnumerable<Application?>> GetApplicationByVersion(string version)
		{
			return await _context.Applications
					.Where(x => x.Version.Equals(version)).ToListAsync();
		}
	}
}
