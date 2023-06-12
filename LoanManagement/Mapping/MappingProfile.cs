using LoanManagement.API.Dto;
using LoanManagement.API.Ressources;

namespace LoanManagement.API.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<User, GetUserDto>();
			CreateMap<RegisterRessource, User>();
			CreateMap<LoginRessource, LoginDto>();
		}
	}
}
