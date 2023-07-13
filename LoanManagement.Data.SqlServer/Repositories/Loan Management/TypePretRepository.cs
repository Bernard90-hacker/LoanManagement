namespace LoanManagement.Data.SqlServer.Repositories.Loan_Management
{
	public class TypePretRepository : Repository<TypePret>, ITypePretRepository
	{
		private LoanManagementDbContext _context;
		public TypePretRepository(LoanManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<TypePret>> GetAll()
		{
			return await _context.TypePrets.ToListAsync();
		}

		public async Task<TypePret?> GetById(int id)
		{
			return await _context.TypePrets.Where(x => x.Id == id)
				.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<Deroulement>> GetDeroulements(int typePretId)
		{
			var typePret = await _context.TypePrets.Where(x => x.Id == typePretId)
				.FirstOrDefaultAsync();

			if (typePret is null) throw new Exception("Type prêt non trouvé");

			return await _context.Deroulements.Where(x => x.TypePretId == typePretId)
				.ToListAsync();
		}
	}
}
