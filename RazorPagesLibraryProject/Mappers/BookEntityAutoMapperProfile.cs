using AutoMapper;
using RazorPagesLibraryProject.DTOes;
using RazorPagesLibraryProject.Entities;

namespace RazorPagesLibraryProject.Mappers
{
    public class BookEntityAutoMapperProfile : Profile
    {
        public BookEntityAutoMapperProfile()
        {
            //CreateMap<EventEntity, EventEntityCreateGetDTO>().ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt.HasValue ? s.CreatedAt.Value.ToString("yyyy/MM/dd HH:mm:ss") : null)).ForMember(dest => dest.EventDate, opt => opt.MapFrom(s => s.EventDate.ToString("yyyy/MM/dd HH:mm:ss")));
            CreateMap<BookCreateDTO, BookEntity>();
            //CreateMap<EventEntityGetDTO, EventEntity>();
            CreateMap<BookEntity, BookGetDTO>();
            CreateMap<BookUpdateDTO, BookEntity>();
            //CreateMap<EventEntity, EventEntityDeletedGetDTO>();
        }
    }
}
