namespace WebApi.UserApi.Services;

using Models;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsersAsync();

    Task<User?> GetUserByIdAsync(int id);

    Task<User> AddUserAsync(User user);

    Task<User?> UpdateUserAsync(int id, User user);

    Task<bool> DeleteUserAsync(int id);
}
