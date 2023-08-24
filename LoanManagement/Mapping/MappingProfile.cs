using LoanManagement.API.Dto;
using LoanManagement.API.Ressources;
using LoanManagement.API.Ressources.Loan_Management;
using LoanManagement.core.Models.Loan_Management;

namespace LoanManagement.API.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Direction, DirectionRessource>();
			CreateMap<DirectionRessource, Direction>();
			CreateMap<SaveDepartementRessource, Departement>();
			CreateMap<Departement, DepartementRessource>()
				.ForMember(dest => dest.DirectionCode, act => act.MapFrom(src => src.Direction.Code));
			CreateMap<Utilisateur, UtilisateurRessource>();
			CreateMap<UtilisateurRessource, Utilisateur>();
			CreateMap<MotDePasseRessource, MotDePasse>();
			CreateMap<MotDePasse, MotDePasseRessource>();
			CreateMap<UserUpdateRessource, Utilisateur>();
			CreateMap<ProfilRessource, Profil>();
			CreateMap<Profil, ProfilRessource>();
			CreateMap<HabilitationProfil, HabilitationProfilRessource>();
			CreateMap<HabilitationProfilRessource, HabilitationProfil>();
			CreateMap<ParamGlobalRessource, ParamGlobal>();
			CreateMap<UpdateParamMotDePasseRessource, ParamGlobal>();
			CreateMap<ParamGlobal, ParamGlobalRessource>();
			CreateMap<Employe, GetEmployeResource>();
			CreateMap<EmployeRessource, Employe>();
			CreateMap<Application, GetApplicationRessource>();
			CreateMap<ApplicationRessource, Application>();
			CreateMap<MenuRessource, Menu>();
			CreateMap<Menu, MenuRessource>();
			CreateMap<SaveApplicationRessource, Application>();
			CreateMap<TypeJournalRessource, TypeJournal>();
			CreateMap<TypeJournal, TypeJournalRessource>();
			CreateMap<Journal, JournalRessource>();
			CreateMap<JournalRessource, Journal>();
			CreateMap<ClientRessource, Client>();
			CreateMap<Client, ClientRessource>();
			CreateMap<Compte, CompteRessource>();
			CreateMap<Compte, GetCompteDto>();
			CreateMap<CompteRessource, Compte>();
			CreateMap<TypePretRessource, TypePret>();
			CreateMap<TypePret, TypePretRessource>();
			CreateMap<Deroulement, DeroulementRessource>();
			CreateMap<DeroulementRessource, Deroulement>();
			CreateMap<Deroulement, GetDeroulementRessource>();
			CreateMap<UpdateDeroulementRessource, Deroulement>();
			CreateMap<RoleOrgane, RoleOrganeRessource>();
			CreateMap<RoleOrganeRessource, RoleOrgane>();
			CreateMap<MembreOrgane, MembreOrganeRessource>();
			CreateMap<MembreOrganeRessource, MembreOrgane>();
			CreateMap<OrganeDecision, OrganeDecisionRessource>();
			CreateMap<OrganeDecisionRessource, OrganeDecision>();
			CreateMap<TypePretRessource, TypePret>();
			CreateMap<TypePret, TypePretRessource>();
			CreateMap<UpdateRoleOrganeRessource, RoleOrgane>();
			CreateMap<EtapeDeroulementRessource, EtapeDeroulement>();
			CreateMap<EtapeDeroulement, EtapeDeroulementRessource>();
			CreateMap<DossierClient, SaveDossierClientResource>();
			CreateMap<DossierClient, DossierClientRessource>();
			CreateMap<SaveDossierClientResource, DossierClient>();
			CreateMap<InfoSanteClientRessource, InfoSanteClient>();
			CreateMap<InfoSanteClient, InfoSanteClientRessource>();
			CreateMap<TypePret, TypePretRessource>();
			CreateMap<TypePretRessource, TypePret>();
			CreateMap<OrganeDecision, OrganeDecisionRessource>();
			CreateMap<OrganeDecisionRessource, OrganeDecision>();
			CreateMap<PretAccordRessource, PretAccord>();
			CreateMap<PretAccord, PretAccordRessource>();
			CreateMap<EmployeurRessource, Employeur>();
			CreateMap<Employeur, EmployeurRessource>();
			CreateMap<SmtpRessource, ParamGlobal>();

			CreateMap<Utilisateur, GetUtilisateurRessource>();
		}
	}
}
