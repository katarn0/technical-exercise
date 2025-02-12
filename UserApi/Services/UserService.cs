using WebApi.UserApi.Models;

namespace WebApi.UserApi.Services;

public class UserService : IUserService
{
    public Task<User> AddUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User?> UpdateUserAsync(int id, User user)
    {
        throw new NotImplementedException();
    }
}
