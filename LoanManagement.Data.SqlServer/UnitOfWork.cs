using LoanManagement.core;
using LoanManagement.core.Repositories;
using LoanManagement.core.Repositories.Users_Management;
using LoanManagement.Data.SqlServer.Repositories;
using LoanManagement.Data.SqlServer.Repositories.Loan_Management;
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
		private ParamGlobalRepository _paramMotDePasseRepository;
		private ProfilRepository _profilRepository;
		private TypeJournalRepository _typeJournalRepository;
		private ClientRepository _clientRepository;
		private CompteRepository _compteRepository;
		private DeroulementRepository _deroulementRepository;
		private DossierClientRepository _dossierClientRepository;
		private EmployeurRepository _employeurRepository;
		private EtapeDeroulementRepository _etapeDeroulementRepository;
		private SanteClientRepository _infoSanteClientRepository;
		private MembreOrganeRepository _membreOrganeRepository;
		private NatureQuestionRepository _natureQuestionRepository;
		private OrganeDecisionRepository _organeDecisionRepository;
		private PeriodicitePaiementRepository _periodicitePaiementRepository;
		private PretAccordRepository _pretAccordRepository;
		private RoleOrganeRepository _roleOrganeRepository;
		private StatutDossierClientRepository _statutDossierClientRepository;
		private StatutMaritalRepository _statutMaritalRepository;
		private TypePretRepository _typePretRepository;
		private IDbContextTransaction _transaction;
		public UnitOfWork(LoanManagementDbContext context) => _context = context;


		public IApplicationRepository Applications => _applicationRepository = _applicationRepository ?? new ApplicationRepository(_context);
		public IUtilisateurRepository Utilisateurs => _utilisateurRepository = _utilisateurRepository ?? new UtilisateurRepository(_context);

		public IDepartementRepository Departements => _departementRepository = _departementRepository ?? new DepartementRepository(_context);

		public IDirectionRepository Directions => _directionRepository = _directionRepository ?? new DirectionRepository(_context);

		public IEmployeRepository Employes => _employeRepository = _employeRepository ?? new EmployeRepository(_context);

		public IHabilitationProfilRepository HabilitationProfils => _habilitationProfilRepository = _habilitationProfilRepository ?? new HabilitationProfilRepository(_context);

		public IJournalRepository Journaux => _journalRepository = _journalRepository ?? new JournalRepository(_context);

		public IMotDePasseRepository MotDePasses => _motDePasseRepository = _motDePasseRepository ?? new MotDePasseRepository(_context);

		public IParamGlobalRepository ParamMotDePasses => _paramMotDePasseRepository = _paramMotDePasseRepository ?? new ParamGlobalRepository(_context);

		public IProfilRepository Profils => _profilRepository = _profilRepository ?? new ProfilRepository(_context);

		public ITypeJournalRepository TypeJournaux => _typeJournalRepository = _typeJournalRepository ?? new TypeJournalRepository(_context);
		public IMenuRepository Menus => _menuRepository = _menuRepository ?? new MenuRepository(_context);
		public IClientRepository Clients => _clientRepository = _clientRepository ?? new ClientRepository(_context);
		public ICompteRepository Comptes => _compteRepository = _compteRepository ?? new CompteRepository(_context);
		public IDeroulementRepository Deroulements => _deroulementRepository = _deroulementRepository ?? new DeroulementRepository(_context);
		public IDossierClientRepository DossierClients => _dossierClientRepository = _dossierClientRepository ?? new DossierClientRepository(_context);
		public IEmployeurRepository Employeurs => _employeurRepository = _employeurRepository ?? new EmployeurRepository(_context);
		public IEtapeDeroulementRepository Etapes => _etapeDeroulementRepository = _etapeDeroulementRepository ?? new EtapeDeroulementRepository(_context);
		public ISanteClientRepository InfoSantes => _infoSanteClientRepository = _infoSanteClientRepository ?? new SanteClientRepository(_context);
		public IMembreOrganeRepository MembreOrganes => _membreOrganeRepository = _membreOrganeRepository ?? new MembreOrganeRepository(_context);
		public INatureQuestionRepository Natures => _natureQuestionRepository = _natureQuestionRepository ?? new NatureQuestionRepository(_context);
		public IOrganeDecisionRepository OrganeDecisions => _organeDecisionRepository = _organeDecisionRepository ?? new OrganeDecisionRepository(_context);
		public IPeriodicitePaiementRepository PeriodicitePaiements => _periodicitePaiementRepository = _periodicitePaiementRepository ?? new PeriodicitePaiementRepository(_context);
		public IPretAccordRepository PretAccords => _pretAccordRepository = _pretAccordRepository ?? new PretAccordRepository(_context);
		public IRoleOrganeRepository Roles => _roleOrganeRepository = _roleOrganeRepository ?? new RoleOrganeRepository(_context);
		public IStatutDossierClientRepository StatutDossierClients => _statutDossierClientRepository = _statutDossierClientRepository ?? new StatutDossierClientRepository(_context);
		public IStatutMaritalRepository StatutMaritals => _statutMaritalRepository = _statutMaritalRepository ?? new StatutMaritalRepository(_context);
		public ITypePretRepository TypePrets => _typePretRepository = _typePretRepository = _typePretRepository ?? new TypePretRepository(_context);

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
			try
			{
				await transaction.CommitAsync();
			}
			catch (Exception)
			{

				await transaction.RollbackAsync();
			}
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
