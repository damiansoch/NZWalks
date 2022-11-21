using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string password);

        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserByIDAsync(Guid id);
    }
}
