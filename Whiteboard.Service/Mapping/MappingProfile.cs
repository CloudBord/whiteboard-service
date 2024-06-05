using AutoMapper;
using Whiteboard.DataAccess.Models;
using Whiteboard.Service.Models;

namespace Whiteboard.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Board, BoardDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
