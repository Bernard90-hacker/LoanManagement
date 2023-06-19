using LoanManagement.core;
using LoanManagement.core.Repositories;
using LoanManagement.core.Repositories.Users_Management;
using LoanManagement.Data.SqlServer.Repositories;
using LoanManagement.Data.SqlServer.Repositories.Users_Management;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LoanManagement.Data.SqlServer
{
	public class UnitOfWork : IUnitOfWork
	{
		private LoanManagementDbContext _context;
		private UtilisateurRepository _utilisateurRepository;
		private ApplicationRepository _applicationRepository;
		private DepartementRepository _departementRepository;
		private DirectionRepository _directionRepository;
		private EmployeRepository _employeRepository;
		private HabilitationProfilRepository _habilitationProfilRepository;
		private JournalRepository _journalRepository;
		private MenuRepository _menuRepository;
		private MotDePasseRepository _motDePasseRepository;
		private ParamMotDePasseRepository _paramMotDePasseRepository;
		private ProfilRepository _profilRepository;
		private TypeJournalRepository _typeJournalRepository;
		public UnitOfWork(LoanManagementDbContext context) => _context = context;


		public IApplicationRepository Applications => _applicationRepository = _applicationRepository ?? new ApplicationRepository(_context);
		public IUtilisateurRepository Utilisateurs => _utilisateurRepository = _utilisateurRepository ?? new UtilisateurRepository(_context);

		public IDepartementRepository Departements => _departementRepository = _departementRepository ?? new DepartementRepository(_context);

		public IDirectionRepository Directions => _directionRepository = _directionRepository ?? new DirectionRepository(_context);

		public IEmployeRepository Employes => _employeRepository = _employeRepository ?? new EmployeRepository(_context);

		public IHabilitationProfilRepository HabilitationProfils => _habilitationProfilRepository = _habilitationProfilRepository ?? new HabilitationProfilRepository(_context);

		public IJournalRepository Journaux => _journalRepository = _journalRepository ?? new JournalRepository(_context);

		public IMotDePasseRepository MotDePasses => _motDePasseRepository = _motDePasseRepository ?? new MotDePasseRepository(_context);

		public IParamMotDePasseRepository ParamMotDePasses => _paramMotDePasseRepository = _paramMotDePasseRepository ?? new ParamMotDePasseRepository(_context);

		public IProfilRepository Profils => _profilRepository = _profilRepository ?? new ProfilRepository(_context);

		public ITypeJournalRepository TypeJournaux => _typeJournalRepository = _typeJournalRepository ?? new TypeJournalRepository(_context);
		public IMenuRepository Menus => _menuRepository = _menuRepository ?? new MenuRepository(_context);

		public async Task<int> CommitAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public async Task<IDbContextTransaction> BeginTransactionAsync()
		{
			return await _context.Database.BeginTransactionAsync();
		}

		public async Task CommitAsync(IDbContextTransaction transaction)
		{
			await transaction.CommitAsync();
		}

		public async Task RollbackAsync(IDbContextTransaction transaction)
		{
			await transaction.RollbackAsync();
		}

		public async Task CreateSavepointAsync(IDbContextTransaction transaction, string savePointName)
		{
			await transaction.CreateSavepointAsync(savePointName);
		}

		public async Task RollbackSavepointAsync(IDbContextTransaction transaction, string savePointName)
		{
			await transaction.RollbackToSavepointAsync(savePointName);
		}

		public async Task ReleaseSavepointAsync(IDbContextTransaction transaction, string savePointName)
		{
			await transaction.ReleaseSavepointAsync(savePointName);
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
