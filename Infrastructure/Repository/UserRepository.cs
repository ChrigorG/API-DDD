using Domain.Interface;
using Entities.Entity;
using Infrastructure.Repository.Generic;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRepository : GenericRepository<UserEntity>, IUser
    {
        public UserRepository(AppDbContext db) : base(db) { }

        public async Task<bool> EmailExists(string email)
        {
            try
            {
                return await _db.User
                    .Where(x => x.Email == email)
                    .AsNoTracking()
                    .AnyAsync();
            } catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UserExists(string email, string password)
        {
            try
            {
                return await _db.User
                    .Where(x => x.Email == email && x.PasswordHash == password)
                    .AsNoTracking()
                    .AnyAsync();
            } catch (Exception)
            {
                return false;
            }
        }
    }
}
