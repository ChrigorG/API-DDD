using AutoMapper;
using Entities.Entity;
using WebAPI.DTO;

namespace WebAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeamento de NewsEntity para NewsDTO
            CreateMap<NewsEntity, NewsDTO>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.UserEntity));

            // Mapeamento de UserEntity para UserDTO
            CreateMap<UserEntity, UserDTO>();
        }
    }
}
