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
    public class EmployeeService : IEmployeeService
    {
        private string table = "Employee";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Employee> GetById(int id)
        {
            return await _unitOfWork.EmployeeRepository.GetById(id);
        }

        public async Task<ApiResponse<EmployeeResponseDTO>> Add(EmployeeRequestDTOCreate request)
        {
            var employee = _mapper.Map<Employee>(request);

            // Aquí puedes añadir cualquier lógica de negocio adicional, como validaciones

            await _unitOfWork.EmployeeRepository.Add(employee);
            await _unitOfWork.SaveChangesAsync();

            var mapper = _mapper.Map<EmployeeResponseDTO>(employee);

            return new ApiResponse<EmployeeResponseDTO>
            {
                Data = mapper,
                Success = true,
                Message = "The " + table + " was created successfully",
            };
        }

        public async Task<ApiResponse<EmployeeResponseDTO>> Update(EmployeeRequestDTOUpdate request)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetById(request.Id);
            if (employee == null)
            {
                return new ApiResponse<EmployeeResponseDTO>
                {
                    Success = false,
                    Message = "Entity not found."
                };
            }
            //Mapeo
            _mapper.Map(request, employee);
            // Si necesitas cambiar alguna propiedad específica, puedes hacerlo aquí
            _unitOfWork.EmployeeRepository.UpdateProperties(employee, p => p.FirstName!, p => p.LastName!, p => p.Email!,p=>p.Position!, p => p.Phone!);

            await _unitOfWork.SaveChangesAsync();

            var mapper = _mapper.Map<EmployeeResponseDTO>(employee);

            return new ApiResponse<EmployeeResponseDTO>
            {
                Data = mapper,
                Success = true,
                Message = "The " + table + " was updated successfully",
            };
        }

        public async Task<ApiResponse<object>> Delete(int id)
        {
            Employee employee = await _unitOfWork.EmployeeRepository.GetById(id);
            // Aquí puedes manejar la lógica de eliminación
            employee.IsActive = false; // o eliminar completamente si es necesario
            _unitOfWork.EmployeeRepository.UpdateProperties(employee, p => p.FirstName!, p => p.LastName!, p => p.Email!);
            await _unitOfWork.SaveChangesAsync();

            return new ApiResponse<object>
            {
                Success = true,
                Message = table + " deleted successfully"
            };
        }

        public async Task<RecordsResponse<EmployeeResponseDTO>> Get(QueryFilter filter)
        {
            var response = await _unitOfWork.EmployeeRepository.Get(filter);
            return response;
        }
    }
}
