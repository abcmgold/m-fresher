using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto;
using MISA.WebFresher042023.Demo.Core.HandleException;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Service
{
    public abstract class BaseService<TEntity, TEntityDto, TEntityUpdateDto, TEntityCreateDto> : IBaseService<TEntityDto, TEntityUpdateDto, TEntityCreateDto>
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;

        protected BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<int> DeleteAsync(string id)
        {
             var res = await _baseRepository.DeleteAsync(id);

            return res;
        }

        public async Task<int> DeleteMultiAsync(string id)
        {
            var res = await _baseRepository.DeleteMultiAsync(id);

            return res;
        }

        public async Task<List<TEntityDto>> GetAsync()
        {
            var results = await _baseRepository.GetAsync();

            List<TEntityDto> entityDtos  = new();

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

        public virtual async Task<int> InsertAsync(TEntityCreateDto entityCreate)
        {

            var entity = _mapper.Map<TEntity>(entityCreate);

            var result = await _baseRepository.InsertAsync(entity);

            return result;
        }

        public virtual async Task<int> UpdateAsync(Guid id, TEntityUpdateDto entityUpdate)
        {
            var entity = _mapper.Map<TEntity>(entityUpdate);

            var result = await _baseRepository.UpdateAsync(id, entity);

            return result;
        }


    }
}
