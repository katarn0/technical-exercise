namespace WebApi.UserApi.Services;

using FluentValidation;

using Microsoft.EntityFrameworkCore;

using Data;
using Models;
using Models.DataTransferObjects;

public class UserService : IUserService
{
    private readonly UserContext _dbContext;
    private readonly IValidator<User> _userValidator;

    public UserService(UserContext dbContext, IValidator<User> userValidator)
    {
        _dbContext = dbContext;
        _userValidator = userValidator;
    }

    public async Task<UserDto> AddUserAsync(CreateUserDto user)
    {
        var validationResult = _userValidator.Validate(MapToModel(user));

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        if (_dbContext.Users.Any(u => u.Email.Equals(user.Email)))
        {
            throw new ArgumentException($"Duplicate email address trying to be added: {user.Email}.");
        }

        var userEntity = MapToModel(user);
        _dbContext.Users.Add(userEntity);
        await _dbContext.SaveChangesAsync();

        return MapToDto(userEntity);
    }

    public Task<bool> DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        return user != null ? MapToDto(user) : null;
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync()
    {
        var users = await _dbContext.Users.ToListAsync();
        return users.Select(MapToDto).ToList();
    }

    public async Task<UserDto?> UpdateUserAsync(int id, UpdateUserDto user)
    {
        var userEntity = await _dbContext.Users.FindAsync(id);

        if (userEntity == null)
        {
            return null;
        }

        var validationResult = _userValidator.Validate(MapToModel(user));
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        if (_dbContext.Users.Any(u => u.Email.Equals(user.Email)))
        {
            throw new ArgumentException($"Duplicate email address trying to be added: {user.Email}.");
        }

        userEntity.FirstName = user.FirstName;
        userEntity.LastName = user.LastName;
        userEntity.Email = user.Email;
        userEntity.DateOfBirth = user.DateOfBirth;
        userEntity.PhoneNumber = user.PhoneNumber;

        await _dbContext.SaveChangesAsync();

        return MapToDto(userEntity);
    }

    private static UserDto MapToDto(User entity)
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

    private static User MapToModel(UserDto model)
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

    private static User MapToModel(CreateUserDto model)
    {
        return new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            DateOfBirth = model.DateOfBirth,
            PhoneNumber = model.PhoneNumber
        };
    }

    private static User MapToModel(UpdateUserDto model)
    {
        return new User
        {
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
