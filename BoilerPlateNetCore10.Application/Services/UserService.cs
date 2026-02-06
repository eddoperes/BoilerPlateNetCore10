using BoilerPlateNetCore10.Application.DTOs;
using BoilerPlateNetCore10.Application.Interfaces;
using BoilerPlateNetCore10.Application.Services.Super;
using BoilerPlateNetCore10.Domain.Entities;
using BoilerPlateNetCore10.Domain.Interfaces;
using Mapster;
using System;
using System.Threading.Tasks;

namespace BoilerPlateNetCore10.Application.Services
{
    public class UserService : CrudService<UserDTO, User>, IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(IUserRepository));
        }

        public new async Task<UserDTO> AddAsync(UserDTO userDTO)
        {
            var item = userDTO.Adapt<User>();
            var newDTO = (await _userRepository.CreateAsync(item)).Adapt<UserDTO>();
            newDTO.SensitiveData = null;
            return newDTO;
        }

        public new async Task<UserDTO?> UpdateAsync(UserDTO userDTO)
        {
            var item = userDTO.Adapt<User>();
            var updatedDTO = (await _userRepository.UpdateAsync(item)).Adapt<UserDTO>();
            updatedDTO.SensitiveData = null;
            return updatedDTO;
        }

        public async Task<bool> ExistsLoginAsync(string login)
        {
            return await _userRepository.ExistsLoginAsync(login);
        }

        public async Task<bool> ExistsLoginWithOtherIdAsync(long id, string login)
        {
            return await _userRepository.ExistsLoginWithOtherIdAsync(id, login);
        }


    }
}
