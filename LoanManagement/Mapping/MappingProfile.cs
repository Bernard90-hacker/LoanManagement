using LoanManagement.API.Dto;
using LoanManagement.API.Ressources;

namespace LoanManagement.API.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Direction, DirectionRessource>();
			CreateMap<DirectionRessource, Direction>();
			CreateMap<DepartementRessource, Departement>()
				.ForMember(dest => dest.Code, act => act.MapFrom(src => src.DirectionCode));
			CreateMap<Departement, DepartementRessource>()
				.ForMember(dest => dest.DirectionCode, act => act.MapFrom(src => src.Code));
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
			CreateMap<Employe, EmployeRessource>();
			CreateMap<EmployeRessource, Employe>();

			CreateMap<Utilisateur, GetUtilisateurRessource>();
		}
	}
}
