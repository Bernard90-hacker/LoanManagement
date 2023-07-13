namespace LoanManagement.Data.SqlServer.Repositories.Loan_Management
{
	public class PretAccordRepository : Repository<PretAccord>, IPretAccordRepository
	{
		private LoanManagementDbContext _context;
		public PretAccordRepository(LoanManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<PretAccord>> GetAll()
		{
			return await _context.PretAccords.ToListAsync();
		}

		public async Task<PretAccord?> GetById(int id)
		{
			return await _context.PretAccords.Where(x => x.Id == id)
				.FirstOrDefaultAsync();
		}

		public async Task<PretAccord?> GetPretAccordForDossier(int dossierId)
		{
			var dossier = await _context.DossierClients.Where(x => x.Id == dossierId)
				.FirstOrDefaultAsync();
			if (dossier is null) throw new Exception("Dossier non trouvé");

			var result = (from x in (await _context.PretAccords.ToListAsync())
						 where x.Id == dossier.PretAccordId
						 select x).FirstOrDefault();

			return result;
		}
	}
}
