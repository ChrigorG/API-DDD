namespace Application.Interface
{
    public interface IUserApplication
    {

        Task<bool> EmailExists(string email);

        Task<bool> UserExists(string email, string password);
    }
}
