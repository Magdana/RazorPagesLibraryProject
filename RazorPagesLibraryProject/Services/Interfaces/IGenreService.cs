using RazorPagesLibraryProject.DTOes;

namespace RazorPagesLibraryProject.Services.Interfaces
{
    public interface IGenreService
    {
        Task<GenreGetDTO> Add(GenreCreateDTO entitydto);
        Task<GenreGetDTO> Update(GenreUpdateDTO entityDto);
        Task<GenreGetDTO> GetById(int id);
        Task<string> Delete(GenreDeleteDTO entityDto);
        Task<ResponseDTO<GenreGetDTO>> Search(string keyword);
        Task<List<GenreGetDTO>> GetAllAsync();
    }
}
