using AutoMapper;
using RazorPagesLibraryProject.DTOes;
using RazorPagesLibraryProject.Entities;

namespace RazorPagesLibraryProject.Mappers
{
    public class BookEntityAutoMapperProfile : Profile
    {
        public BookEntityAutoMapperProfile()
        {
            CreateMap<BookEntity, BookGetDTO>().ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt.HasValue ? s.CreatedAt.Value.ToString("yyyy/MM/dd HH:mm:ss") : null));
            CreateMap<BookCreateDTO, BookEntity>();
            //CreateMap<EventEntityGetDTO, EventEntity>();
            //CreateMap<BookEntity, BookGetDTO>();
            CreateMap<BookUpdateDTO, BookEntity>();
            //CreateMap<EventEntity, EventEntityDeletedGetDTO>();
        }
    }
}
