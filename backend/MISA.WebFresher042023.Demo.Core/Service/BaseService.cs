using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto;
using MISA.WebFresher042023.Demo.Core.HandleException;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MISA.WebFresher042023.Demo.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Service
{
    /// <summary>
    /// Lớp thực thi của BaseService
    /// </summary>
    /// CreatedBy: BATUAN (20/06/2023)
    public abstract class BaseService<TEntity, TEntityDto, TEntityUpdateDto, TEntityCreateDto> : IBaseService<TEntityDto, TEntityUpdateDto, TEntityCreateDto>
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public virtual async Task<int> DeleteAsync(List<Guid> listId)
        {
            string concatenatedIds = string.Join(", ", listId);

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var res = await _baseRepository.DeleteAsync(concatenatedIds);

                await _unitOfWork.CommitAsync();

                return res;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<List<TEntityDto>> GetAsync()
        {
            var results = await _baseRepository.GetAsync();

            List<TEntityDto> entityDtos = new();

            foreach (var result in results)
            {
                var entityDto = _mapper.Map<TEntityDto>(result);

                entityDtos.Add(entityDto);
            }
            return entityDtos;
        }

        public async Task<TEntityDto> GetByIdAsync(Guid id)
        {
            var result = await _baseRepository.GetByIdAsync(id);

            if (result == null)
            {
                throw new NotFoundException();
            }

            var entityDto = _mapper.Map<TEntityDto>(result);

            return entityDto;
        }

        public virtual async Task<int> InsertAsync(List<TEntityCreateDto> entityCreates)
        {
            List<TEntity> entities = _mapper.Map<List<TEntity>>(entityCreates);

            var result = await _baseRepository.InsertAsync(entities);

            return result;
        }

        public virtual async Task<int> UpdateAsync(List<TEntityUpdateDto> entityUpdate)
        {
            List<TEntity> entities = _mapper.Map<List<TEntity>>(entityUpdate);

            var result = await _baseRepository.UpdateAsync(entities);

            return result;
        }


    }
}
