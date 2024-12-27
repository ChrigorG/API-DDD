using Domain.Interface;
using Entities.Entity;
using Infrastructure.Repository.Generic;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRepository : GenericRepository<UserEntity>, IUser
    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;

        public UserRepository()
        {
            _dbContextOptions = new DbContextOptions<AppDbContext>();
        }

        public async Task<bool> EmailExists(string email)
        {
            try
            {
                using (var db = new AppDbContext(_dbContextOptions))
                {
                    return await db.User
                        .Where(x => x.Email == email)
                        .AsNoTracking()
                        .AnyAsync();
                }
            } catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UserExists(string email, string password)
        {
            try
            {
                using (var db = new AppDbContext(_dbContextOptions))
                {
                    return await db.User
                        .Where(x => x.Email == email && x.PasswordHash == password)
                        .AsNoTracking()
                        .AnyAsync();
                }
            } catch (Exception)
            {
                return false;
            }
        }
    }
}
