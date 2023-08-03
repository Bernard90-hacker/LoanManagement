
using LoanManagement.core.Repositories.Loan_Management;
using LoanManagement.core.Repositories.Users_Management;

namespace LoanManagement.core
{
	public interface IUnitOfWork : IDisposable
	{
		IApplicationRepository Applications { get; }
		IUtilisateurRepository Utilisateurs { get; }
		IDepartementRepository Departements { get;}
		IDirectionRepository Directions { get;}
		IEmployeRepository Employes { get; }
		IHabilitationProfilRepository HabilitationProfils { get; }
		IJournalRepository Journaux { get; }
		IMenuRepository Menus { get; }
		IMotDePasseRepository MotDePasses { get;  }
		IParamGlobalRepository ParamMotDePasses { get; }
		IProfilRepository Profils { get; }
		ITypeJournalRepository TypeJournaux { get; }
		IClientRepository Clients { get; }
		ICompteRepository Comptes { get; }
		IDeroulementRepository Deroulements { get; }
		IDossierClientRepository DossierClients { get; }
		IEmployeurRepository Employeurs { get; }
		IEtapeDeroulementRepository Etapes { get; }
		IMembreOrganeRepository MembreOrganes { get; }
		INatureQuestionRepository Natures { get; }
		IOrganeDecisionRepository OrganeDecisions { get; }
		IPeriodicitePaiementRepository PeriodicitePaiements { get; }
		IPretAccordRepository PretAccords { get; }
		IRoleOrganeRepository Roles { get; }
		ISanteClientRepository InfoSantes { get; }
		IStatutDossierClientRepository StatutDossierClients { get; }
		IStatutMaritalRepository StatutMaritals { get; }
		ITypePretRepository TypePrets { get;  }

		Task<int> CommitAsync();
		Task<IDbContextTransaction> BeginTransactionAsync();
		Task CommitAsync(IDbContextTransaction transaction);
		Task RollbackAsync(IDbContextTransaction transaction);
		Task CreateSavepointAsync(IDbContextTransaction transaction, string savePointName);
		Task RollbackSavepointAsync(IDbContextTransaction transaction, string savePointName);
		Task ReleaseSavepointAsync(IDbContextTransaction transaction, string savePointName);
	}
}
