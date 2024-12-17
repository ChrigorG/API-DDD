namespace Domain.Interface
{
    public interface IUser
    {
        Task<bool> AddUser(string email, string password, int age, string cellPhone);
    }
}
