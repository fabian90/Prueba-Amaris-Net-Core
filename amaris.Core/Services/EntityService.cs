using amaris.Core.Interfaces.Repositories;
using AutoMapper;
using Commons.RequestFilter;
using Commons.Response;
using ifxNetworks.Core.DTOs.Request;
using ifxNetworks.Core.DTOs.Response;
using ifxNetworks.Core.Entities;
using ifxNetworks.Core.Interfaces.Services;

namespace ifxNetworks.Core.Services
{
    public class EntityService : IEntityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EntityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Entity> GetById(int id)
        {
            return await _unitOfWork.EntityRepository.GetById(id);
        }

        public async Task<ApiResponse<EntityResponseDTO>> Add(EntityRequestDTOCreate request)
        {
            var entity = _mapper.Map<Entity>(request);
            entity.IsActive = true; // Assume that a new entity is active by default

            await _unitOfWork.EntityRepository.Add(entity);
            await _unitOfWork.SaveChangesAsync();

            var response = _mapper.Map<EntityResponseDTO>(entity);
            return new ApiResponse<EntityResponseDTO>
            {
                Data = response,
                Success = true,
                Message = "The entity was created successfully."
            };
        }

        public async Task<ApiResponse<EntityResponseDTO>> Update(EntityRequestDTOUpdate request)
        {
            var entity = await _unitOfWork.EntityRepository.GetById(request.Id);
            if (entity == null)
            {
                return new ApiResponse<EntityResponseDTO>
                {
                    Success = false,
                    Message = "Entity not found."
                };
            }
            _mapper.Map(request, entity);
            _unitOfWork.EntityRepository.UpdateProperties(entity, p => p.Name!);
            await _unitOfWork.SaveChangesAsync();

            var response = _mapper.Map<EntityResponseDTO>(entity);
            return new ApiResponse<EntityResponseDTO>
            {
                Data = response,
                Success = true,
                Message = "The entity was updated successfully."
            };
        }

        public async Task<ApiResponse<object>> Delete(int id)
        {
            var entity = await _unitOfWork.EntityRepository.GetById(id);

            if (entity == null)
            {
                return new ApiResponse<object>
                {
                    Success = false,
                    Message = "Entity not found."
                };
            }

            entity.IsActive = false; // o eliminar completamente si es necesario
            _unitOfWork.EntityRepository.UpdateProperties(entity, p => p.Name!);
            await _unitOfWork.SaveChangesAsync();

            return new ApiResponse<object>
            {
                Success = true,
                Message = "The entity was deleted successfully."
            };
        }

        public async Task<RecordsResponse<EntityResponseDTO>> Get(QueryFilter filter)
        {
            var response = await _unitOfWork.EntityRepository.Get(filter);
            return response;
        }
    }
}
