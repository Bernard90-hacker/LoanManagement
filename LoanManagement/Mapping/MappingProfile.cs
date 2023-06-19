using LoanManagement.API.Dto;
using LoanManagement.API.Ressources;

namespace LoanManagement.API.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<LoginRessource, LoginDto>();
			CreateMap<Direction, DirectionRessource>();
			CreateMap<DirectionRessource, Direction>();
			CreateMap<DepartementRessource, Departement>();
			CreateMap<Departement, DepartementRessource>();
		}
	}
}
