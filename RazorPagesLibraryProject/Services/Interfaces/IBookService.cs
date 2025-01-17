﻿using RazorPagesLibraryProject.DTOes;

namespace RazorPagesLibraryProject.Services.Interfaces
{
    public interface IBookService
    {
        Task<BookGetDTO> Add(BookCreateDTO entitydto);
        Task<BookGetDTO> Update(BookUpdateDTO entityDto);
        Task<BookGetDTO> GetById(int id);
        Task<ResponseDTO<BookGetDTO>> GetAll(int pageNumber, int pageSize);
        Task Delete(BookGetDTO entityDto);
        Task<ResponseDTO<BookGetDTO>> GetFilteredByDate(DateTime? createdAt);
        Task<List<BookGetDTO>> Search(string keyword);
        Task<List<BookGetDTO>> GetAllAsync();
        Task<List<string>> GetLastAdded();
        Task<IEnumerable<BookGetDTO>> GetBooksByGenresAsync(int genreId);
    }
}
