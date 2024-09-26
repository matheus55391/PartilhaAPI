using PartilhaAPI.Models;
using PartilhaAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PartilhaAPI.Services
{
    public interface IUserService
    {
        Task<User> FindByFirebaseUidAsync(string firebaseUid);
        Task<User> CreateUserAsync(User user);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> FindByFirebaseUidAsync(string firebaseUid)
        {
            return await _userRepository.FindByFirebaseUidAsync(firebaseUid);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            user.Id = Guid.NewGuid(); // Gera um novo GUID para o ID do usuário
            user.FriendCode = Guid.NewGuid(); // Gera um UUID aleatório para o código de amigo
            return await _userRepository.CreateUserAsync(user);
        }
    }
}
