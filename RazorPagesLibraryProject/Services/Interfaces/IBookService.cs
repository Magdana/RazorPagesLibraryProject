using RazorPagesLibraryProject.DTOes;

namespace RazorPagesLibraryProject.Services.Interfaces
{
    public interface IBookService
    {
        Task<BookGetDTO> Add(BookCreateDTO entitydto);
        Task<BookGetDTO> Update(BookUpdateDTO entityDto);
        Task<BookGetDTO> GetById(int id);
        Task<ResponseDTO<BookGetDTO>> GetAll(int pageNumber, int pageSize);
        //Task<string> Delete(BookDeleteDTO entityDto);
        //Task<string> DeleteRange(IEnumerable<BookDeleteDTO> entityDtoes);
        Task<ResponseDTO<BookGetDTO>> GetFilteredByDate(DateTime? createdAt);
        Task<ResponseDTO<BookGetDTO>> Search(string keyword);
        Task<List<BookGetDTO>> GetAllAsync();
    }
}
