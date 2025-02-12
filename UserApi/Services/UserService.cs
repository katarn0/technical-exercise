namespace WebApi.UserApi.Services;

using Microsoft.EntityFrameworkCore;

using Data;
using Models;
using Models.DataTransferObjects;

public class UserService : IUserService
{
    private readonly UserContext _dbContext;

    public UserService(UserContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserDto> AddUserAsync(UserDto user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto?> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync()
    {
        var users = await _dbContext.Users.ToListAsync();
        return users.Select(MapToModel).ToList();
    }

    public Task<UserDto?> UpdateUserAsync(int id, UserDto user)
    {
        throw new NotImplementedException();
    }

    private static UserDto MapToModel(User entity)
    {
        return new UserDto
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            DateOfBirth = entity.DateOfBirth,
            PhoneNumber = entity.PhoneNumber,
            Age = CalculateAge(entity.DateOfBirth)
        };
    }

    private static User MapToEntity(UserDto model)
    {
        return new User
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            DateOfBirth = model.DateOfBirth,
            PhoneNumber = model.PhoneNumber
        };
    }

    private static int CalculateAge(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;
        if (dateOfBirth.Date > today.AddYears(-age))
        {
            age--;
        }
        return age;
    }

}
