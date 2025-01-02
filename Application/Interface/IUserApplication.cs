namespace Application.Interface
{
    public interface IUserApplication
    {
        Task<bool> EmailExists(string email);
    }
}
