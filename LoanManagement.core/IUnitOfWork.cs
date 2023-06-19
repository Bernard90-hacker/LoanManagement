
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
		IParamMotDePasseRepository ParamMotDePasses { get; }
		IProfilRepository Profils { get; }
		ITypeJournalRepository TypeJournaux { get; }

		Task<int> CommitAsync();
		Task<IDbContextTransaction> BeginTransactionAsync();
		Task CommitAsync(IDbContextTransaction transaction);
		Task RollbackAsync(IDbContextTransaction transaction);
		Task CreateSavepointAsync(IDbContextTransaction transaction, string savePointName);
		Task RollbackSavepointAsync(IDbContextTransaction transaction, string savePointName);
		Task ReleaseSavepointAsync(IDbContextTransaction transaction, string savePointName);
	}
}
