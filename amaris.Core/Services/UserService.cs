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
    public class UserService : IUserService
    {
        private string table = "User";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordService passwordService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        public async Task<User> GetByUserName(string userName)
        {
            return await _unitOfWork.UserRepository.GetByUserName(userName);
        }

        public async Task<ApiResponse<UserResponseDTO>> Add(UserRequestDTO request)
        {
            User oUser = await _unitOfWork.UserRepository.GetByUserName(request.UserName);

            if(oUser == null)
            {
                var user = _mapper.Map<User>(request);

                user.Password = _passwordService.Hash(request.Password);
                user.IsActive = true;

                await _unitOfWork.UserRepository.Add(user);
                await _unitOfWork.SaveChangesAsync();

                var mapper = _mapper.Map<UserResponseDTO>(user);

                return new ApiResponse<UserResponseDTO>()
                {
                    Data = mapper,
                    Success = true,
                    Message = "The " + table + " was created successfully",
                };
            }
            else
            {
                return new ApiResponse<UserResponseDTO>()
                {
                    Message = "Username already exist" ,
                    Success = false
                };
            } 
        }

        public async Task<ApiResponse<UserResponseDTO>> Update(UserRequestDTO request)
        {
            var user = _mapper.Map<User>(request);

            if (user.Password != null)
            {
                user.Password = _passwordService.Hash(request.Password);
                _unitOfWork.UserRepository.UpdateProperties(user, p => p.Email!, p => p.FirstName!, p => p.LastName!, p => p.Password);
            }
            else
            {
                _unitOfWork.UserRepository.UpdateProperties(user, p => p.Email!, p => p.FirstName!, p => p.LastName!);
            }

            await _unitOfWork.SaveChangesAsync();

            var mapper = _mapper.Map<UserResponseDTO>(user);

            return new ApiResponse<UserResponseDTO>()
            {
                Data = mapper,
                Success = true,
                Message = "The " + table + " was update successfully",
            };
        }

        public async Task<ApiResponse<object>> Delete(int id)
        {
            User oUser = await _unitOfWork.UserRepository.GetById(id);

            oUser.IsActive = false;
            _unitOfWork.SaveChanges();

            return new ApiResponse<object>()
            {
                Success = true,
                Message = table + " deleted successfully"
            };
        }

        public async Task<RecordsResponse<UserResponseDTO>> Get(QueryFilter filter)
        {
            var response = await _unitOfWork.UserRepository.Get(filter);

            return response;
        }
    }
}