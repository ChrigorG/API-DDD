namespace Domain.Interface
{
    public interface IUser
    {
        Task<bool> EmailExists(string email);

        Task<bool> UserExists(string email, string password);
    }
}
