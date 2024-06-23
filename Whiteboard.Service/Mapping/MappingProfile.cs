using AutoMapper;
using Whiteboard.DataAccess.Models;
using Whiteboard.Service.DTO;
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

            CreateMap<Board, DocumentDTO>()
                .ForMember(dest => dest.BoardId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.MemberIds, opt => opt.MapFrom(src => src.MemberIds));
        }
    }
}
