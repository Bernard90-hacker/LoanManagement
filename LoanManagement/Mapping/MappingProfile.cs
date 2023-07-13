using LoanManagement.API.Dto;
using LoanManagement.API.Ressources;
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
			CreateMap<ParamMotDePasseRessource, ParamMotDePasse>();
			CreateMap<ParamMotDePasse, ParamMotDePasseRessource>();
			CreateMap<Employe, EmployeRessource>()
				.ForMember(dest => dest.Username, act => act.MapFrom(src => src.User.Username));
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

			CreateMap<Utilisateur, GetUtilisateurRessource>();
		}
	}
}
