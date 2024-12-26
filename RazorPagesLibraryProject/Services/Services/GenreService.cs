using AutoMapper;
using RazorPagesLibraryProject.DTOes;
using RazorPagesLibraryProject.Entities;
using RazorPagesLibraryProject.Repository.Interfaces;
using RazorPagesLibraryProject.Services.Interfaces;
using System.Linq.Expressions;

namespace RazorPagesLibraryProject.Services.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GenreGetDTO> Add(GenreCreateDTO entitydto)
        {
            var entity = _mapper.Map<GenreEntity>(entitydto);
            await _unitOfWork.genreRepository.Add(entity);
            var result = _mapper.Map<GenreGetDTO>(entity);
            return result;
        }

        public async Task<GenreGetDTO> Update(GenreUpdateDTO entityDto)
        {
            var entity = await _unitOfWork.genreRepository.GetById(entityDto.Id);
            if (entity == null)
            {
                throw new KeyNotFoundException("No genre was found to update!");
            }
            var entityToUpdate = _mapper.Map(entityDto, entity);
            await _unitOfWork.genreRepository.Update(entityToUpdate);
            var updatedEntityDto = _mapper.Map<GenreGetDTO>(entityToUpdate);
            return updatedEntityDto;
        }

        public async Task<GenreGetDTO> GetById(int id)
        {
            var genreEntity = await _unitOfWork.genreRepository.GetById(id);
            if (genreEntity == null)
            {
                throw new KeyNotFoundException("Genre was not found!");
            }
            var genreDto = _mapper.Map<GenreGetDTO>(genreEntity);
            return genreDto;
        }

        public async Task<string> Delete(GenreDeleteDTO entityDto)
        {
            if (entityDto != null)
            {

                var entity = await _unitOfWork.genreRepository.GetById(entityDto.Id);
                if (entity == null)
                {
                    throw new ArgumentNullException("entity was not found!");
                }
                await _unitOfWork.genreRepository.Delete(entity);
            }
            return "Genre was removed successfully!";
        }
        public async Task<ResponseDTO<GenreGetDTO>> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                throw new ArgumentException("Keyword cannot be null or whitespace.");
            }
            keyword = keyword.Trim().ToLower();

            var genreEntities = await _unitOfWork.genreRepository.GetAllWhereAsync((e) => e.Name.ToLower().Contains(keyword));
            var count = genreEntities.Count();
            if (count == 0)
            {
                return new ResponseDTO<GenreGetDTO> { Count = 0, Entities = new List<GenreGetDTO>() };
            }
            var genreDTOs = genreEntities.Select((e) => { return _mapper.Map<GenreGetDTO>(e); }).ToList();
            var result = new ResponseDTO<GenreGetDTO>
            {
                Count = count,
                Entities = genreDTOs
            };
            return result;
        }

        public async Task<List<GenreGetDTO>> GetAllAsync()
        {
            var genreEntity = await _unitOfWork.genreRepository.GetAllAsync();
            var dtoes = genreEntity.Select((e) => { return _mapper.Map<GenreGetDTO>(e); }).ToList();
            return dtoes;
        }
    }
}
}
