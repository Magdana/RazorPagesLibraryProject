﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RazorPagesLibraryProject.DTOes;
using RazorPagesLibraryProject.Entities;
using RazorPagesLibraryProject.Repository.Interfaces;
using RazorPagesLibraryProject.Services.Interfaces;
using System.Linq.Expressions;

namespace RazorPagesLibraryProject.Services.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BookGetDTO> Add(BookCreateDTO entitydto)
        {
            var entity = _mapper.Map<BookEntity>(entitydto);
            await _unitOfWork.bookRepository.Add(entity);
            var result = _mapper.Map<BookGetDTO>(entity);
            return result;
        }

        public async Task<BookGetDTO> Update(BookUpdateDTO entityDto)
        {
            var entity = await _unitOfWork.bookRepository.GetById(entityDto.Id);
            if (entity == null)
            {
                throw new KeyNotFoundException("No book was found to update!");
            }
            var entityToUpdate = _mapper.Map(entityDto, entity);
            await _unitOfWork.bookRepository.Update(entityToUpdate);
            var updatedEntityDto = _mapper.Map<BookGetDTO>(entityToUpdate);
            return updatedEntityDto;
        }

        public async Task<BookGetDTO> GetById(int id)
        {
            var bookEntity = await _unitOfWork.bookRepository.GetById(id);
            if (bookEntity == null)
            {
                throw new KeyNotFoundException("Book was not found!");
            }
            var bookDto = _mapper.Map<BookGetDTO>(bookEntity);
            return bookDto;
        }

        public async Task<ResponseDTO<BookGetDTO>> GetAll(int pageNumber = 1, int pageSize = 9)
        {
            var query = _unitOfWork.bookRepository.GetAll();
            var totalEntitiesCount = await query.CountAsync();
            var orderedQuery = query.OrderByDescending(d => d.CreatedAt);
            var paginatedQuery = _unitOfWork.bookRepository.GetPagedResult(orderedQuery, pageNumber, pageSize);
            var bookEntities = await paginatedQuery.ToListAsync();
            var dtoes = _mapper.Map<IEnumerable<BookGetDTO>>(bookEntities);
            var result = new ResponseDTO<BookGetDTO>
            {
                Count = totalEntitiesCount,
                Entities = dtoes
            };
            return result;
        }

        public async Task<string> Delete(BookDeleteDTO entityDto)
        {
            if (entityDto != null)
            {

                var entity = await _unitOfWork.bookRepository.GetById(entityDto.Id);
                if (entity == null)
                {
                    throw new ArgumentNullException("Book was not found!");
                }

                if (!string.IsNullOrWhiteSpace(entity.ImagePath))
                {
                    Uri imageUrl = new Uri(entity.ImagePath);
                    string relativePath = imageUrl.AbsolutePath;
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), relativePath.TrimStart('/'));
                    if (File.Exists(imagePath))
                    {
                        try
                        {
                            File.Delete(imagePath);
                        }
                        catch (Exception ex)
                        {
                            throw new IOException($"Failed to delete the image file: {imagePath}", ex);
                        }
                    }
                }
                await _unitOfWork.bookRepository.Delete(entity);
            }
            return "Book was removed successfully!";

        }

        public async Task<ResponseDTO<BookGetDTO>> GetFilteredByDate(DateTime? createdAt)
        {
            Expression<Func<BookEntity, bool>> predicate = bookEnt =>
                (!createdAt.HasValue || (bookEnt.CreatedAt >= createdAt.Value.Date && bookEnt.CreatedAt < createdAt.Value.Date.AddDays(1)));
            var bookEntities = await _unitOfWork.bookRepository.GetAllWhereAsync(predicate);
            var count = bookEntities.Count();
            var bookDTOs = bookEntities.Select((e) => { return _mapper.Map<BookGetDTO>(e); })
                            .OrderByDescending(d => d.CreatedAt).ToList();
            var result = new ResponseDTO<BookGetDTO>
            {
                Count = count,
                Entities = bookDTOs
            };
            return result;
        }

        public async Task<ResponseDTO<BookGetDTO>> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                throw new ArgumentException("Keyword cannot be null or whitespace.");
            }
            keyword = keyword.Trim().ToLower();

            var bookEntities = await _unitOfWork.bookRepository.GetAllWhereAsync((e) => e.Description.ToLower().Contains(keyword) || e.Name.ToLower().Contains(keyword));
            var count = bookEntities.Count();
            if (count == 0)
            {
                return new ResponseDTO<BookGetDTO> { Count = 0, Entities = new List<BookGetDTO>() };
            }
            var bookDTOs = bookEntities.Select((e) => { return _mapper.Map<BookGetDTO>(e); })
                            .OrderByDescending(d => d.CreatedAt).ToList();
            var result = new ResponseDTO<BookGetDTO>
            {
                Count = count,
                Entities = bookDTOs
            };
            return result;
        }

        public async Task<List<BookGetDTO>> GetAllAsync()
        {
            var bookEntity = await _unitOfWork.bookRepository.GetAllAsync();
            var dtoes = bookEntity.Select((e) => { return _mapper.Map<BookGetDTO>(e); })
                            .OrderByDescending(d => d.CreatedAt).ToList();
            return dtoes;
        }
    }
}

