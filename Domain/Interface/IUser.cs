namespace Domain.Interface
{
    public interface IUser
    {
        Task<bool> EmailExists(string email);
    }
}
