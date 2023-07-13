using LoanManagement.core.Pagination.Loan_Management;

namespace LoanManagement.Data.SqlServer.Repositories.Loan_Management
{
	public class MembreOrganeRepository : Repository<MembreOrgane>, IMembreOrganeRepository
	{
		private LoanManagementDbContext _context;
		public MembreOrganeRepository(LoanManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<MembreOrgane>> GetAll()
		{
			return await _context.MembreOrganes.ToListAsync();
		}

		public async Task<PagedList<MembreOrgane>> GetAll(MembreOrganeParameters parameters)
		{
			var membres = (await _context.MembreOrganes
				.ToListAsync()).AsQueryable();

			return PagedList<MembreOrgane>.ToPagedList(
			membres, parameters.PageNumber, parameters.PageSize);
		}

		public async Task<IEnumerable<Utilisateur>> GetUsersByMembreOrgane(int membreOrganeId)
		{
			var membre = await _context.MembreOrganes.Where(x => x.Id == membreOrganeId)
				.FirstOrDefaultAsync();
			if (membre is null) throw new Exception("Aucun membre organe trouvé");
			var result = from x in (await _context.Utilisateurs.ToListAsync())
						 where membre.UtilisateurId == x.Id
						 select x;
			return result;
		}

		public async Task<IEnumerable<Utilisateur>> GetUsersByOrganeDecision(int organeDecisionId)
		{
			var organeDecision = await _context.OrganeDecisions.Where(x => x.Id == organeDecisionId)
				.FirstOrDefaultAsync();
			if (organeDecision is null) throw new Exception("Aucun organe de décision n'a été trouvé");
			var organes = (await _context.OrganeDecisions.ToListAsync()).AsQueryable();
			var membres = (await _context.MembreOrganes.ToListAsync()).AsQueryable();
			var result = from x in (await _context.Utilisateurs.ToListAsync()).AsQueryable()
						 from y in organes
						 from z in membres
						 where x.Id == z.UtilisateurId && z.OrganeDecisionId == y.Id
						 select x;

			return result;

		}
	}
}
