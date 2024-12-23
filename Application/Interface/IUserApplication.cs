namespace Application.Interface
{
    public interface IUserApplication
    {
        Task<bool> AddUser(string email, string password, int age, string cellPhone);
    }
}
