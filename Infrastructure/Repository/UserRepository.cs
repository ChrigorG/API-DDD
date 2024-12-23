using Domain.Interface;
using Entities.Entity;
using Infrastructure.Repository.Generic;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUser
    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;

        public UserRepository() 
        {
            _dbContextOptions = new DbContextOptions<AppDbContext>();
        }

        public async Task<bool> AddUser(string email, string password, int age, string cellPhone)
        {
            try
            {
                using (var db = new AppDbContext(_dbContextOptions))
                {
                    await db.ApplicationUsers.AddAsync(new ApplicationUser()
                    {
                        Email = email,
                        PasswordHash = password,
                        Age = age,
                        CellPhone = cellPhone,
                    });

                    await db.SaveChangesAsync();
                }
            } catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
