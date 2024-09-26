using PartilhaAPI.Data;
using Microsoft.EntityFrameworkCore;
using PartilhaAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PartilhaAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> FindByFirebaseUidAsync(string firebaseUid);
        Task<User> CreateUserAsync(User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> FindByFirebaseUidAsync(string firebaseUid)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.FirebaseUid == firebaseUid);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
