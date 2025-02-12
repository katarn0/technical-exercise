namespace WebApi.UserApi.Services;

using Models.DataTransferObjects;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetUsersAsync();

    Task<UserDto?> GetUserByIdAsync(int id);

    Task<UserDto> AddUserAsync(CreateUserDto user);

    Task<UserDto?> UpdateUserAsync(int id, UserDto user);

    Task<bool> DeleteUserAsync(int id);
}
