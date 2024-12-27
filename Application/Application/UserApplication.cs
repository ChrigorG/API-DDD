using Application.Interface;
using Domain.Interface;

namespace Application.Application
{
    public class UserApplication : IUserApplication
    {
        private IUser _iUser;

        public UserApplication(IUser iUser) 
        {
            _iUser = iUser;
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _iUser.EmailExists(email);
        }

        public async Task<bool> UserExists(string email, string password)
        {
            return await _iUser.UserExists(email, password);
        }
    }
}
