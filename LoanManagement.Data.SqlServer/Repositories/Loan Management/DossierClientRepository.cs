using LoanManagement.core.Pagination.Loan_Management;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Data.SqlServer.Repositories.Loan_Management
{
	public class DossierClientRepository : Repository<DossierClient>, IDossierClientRepository
	{
		private LoanManagementDbContext _context;
		public DossierClientRepository(LoanManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<DossierClient>> GetAll()
		{
			return await _context.DossierClients.ToListAsync();
		}

		public async Task<PagedList<DossierClient>> GetAll(DossierClientParameters parameters)
		{
			var dossiers = (await _context.DossierClients
				.ToListAsync()).AsQueryable();

			return PagedList<DossierClient>.ToPagedList(
			dossiers, parameters.PageNumber, parameters.PageSize);
		}

		public async Task<DossierClient?> GetById(int Id)
		{
			return await _context.DossierClients.Where(x => x.Id == Id)
				.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<DossierClient?>> GetByClientId(int Id)
		{
			return await _context.DossierClients.Where(x => x.ClientId == Id)
				.ToListAsync();
		}

		public async Task<DossierClient?> GetByNumber(string numeroDossier)
		{
			return await _context.DossierClients.Where(x => x.NumeroDossier == numeroDossier)
				.FirstOrDefaultAsync();
		}

		public async Task<Employeur?> GetEmployeurByDossier(int id)
		{
			return await _context.Employeurs.Where(x => x.Id == id)
				.FirstOrDefaultAsync();
		}

		public async Task<StatutDossierClient?> GetStatut(int id)
		{
			DossierClient? dossierClient = await GetById(id);
			if (dossierClient == null) throw new Exception("Dossier inexistant");

			return await _context.StatutDossierClients.Where(x => x.DossierClientId == 
			dossierClient.Id)
				.FirstOrDefaultAsync();
		}

		public async Task<Deroulement?> GetDossierDeroulement(int typePretId, double montant)
		{
			var deroulements = await _context.Deroulements.Where(x => x.TypePretId == typePretId)
				.ToListAsync();
			var typePrets = await _context.TypePrets.ToListAsync();
			var result = (from x in deroulements
						 from y in typePrets
						 where x.TypePretId == y.Id
						 where montant >= x.Plancher
						 where montant <= x.Plafond
						 select x).FirstOrDefault();

			return result;
		}

		public async Task<IEnumerable<DossierClient>> GetClosed()
		{
			return await _context.DossierClients.Where(x => x.Cloturer == true)
				.ToListAsync();
		}

		public async Task<IEnumerable<InfoSanteClient>> GetInfoSanteByDossier(int dossierId)
		{
			var result = await _context.InfoSanteClients.Where
				(x => x.DossierClientId == dossierId)
				.ToListAsync();

			return result;
		}
	}
}
