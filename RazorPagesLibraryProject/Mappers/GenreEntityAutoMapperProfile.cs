using AutoMapper;
using RazorPagesLibraryProject.DTOes;
using RazorPagesLibraryProject.Entities;

namespace RazorPagesLibraryProject.Mappers
{
    public class GenreEntityAutoMapperProfile : Profile
    {
        public GenreEntityAutoMapperProfile()
        {
            CreateMap<GenreEntity, GenreGetDTO>();
            CreateMap<GenreCreateDTO, GenreEntity>();
            CreateMap<GenreUpdateDTO, GenreEntity>();
        }
    }
}
