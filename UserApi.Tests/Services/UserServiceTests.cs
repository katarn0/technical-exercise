namespace WebApi.UserApi.Tests.Services;

using FluentValidation;

using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

using NUnit.Framework;

using Data;
using Models;
using Models.DataTransferObjects;
using Models.Validators;
using WebApi.UserApi.Services;

[TestFixture]
public class UserServiceTests
{

    private UserContext _dbContext;
    private UserService _userService;
    private IValidator<User> _userValidator;


    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<UserContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new UserContext(options);
        _dbContext.Database.EnsureCreated();

        _userValidator = new UserValidator();
        _userService = new UserService(_dbContext, _userValidator);
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
    [Test]
    public async Task AddUserAsync_ShouldAddUser()
    {
        // Arrange.
        var user = new CreateUserDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            DateOfBirth = new DateTime(1990, 1, 1),
            PhoneNumber = "1234567890"
        };

        // Act.
        var result = await _userService.AddUserAsync(user);

        // Assert.
        Assert.That(result, Is.Not.Null);
        Assert.That(result.FirstName, Is.EqualTo(user.FirstName));
        Assert.That(result.LastName, Is.EqualTo(user.LastName));
        Assert.That(result.Email, Is.EqualTo(user.Email));
        Assert.That(result.DateOfBirth, Is.EqualTo(user.DateOfBirth));
        Assert.That(result.PhoneNumber, Is.EqualTo(user.PhoneNumber));

        var usersInDb = await _dbContext.Users.ToListAsync();
        Assert.That(usersInDb.Count, Is.EqualTo(1));
    }

    [Test]
    public void AddUserAsync_ShouldThrowValidationException_WhenInvalidUser()
    {
        // Arrange.
        var user = new CreateUserDto
        {
            FirstName = "",
            LastName = "Doe",
            Email = "invalid-email",
            DateOfBirth = DateTime.Now,
            PhoneNumber = "123"
        };

        // Act.
        var ex = Assert.ThrowsAsync<ValidationException>(async () => await _userService.AddUserAsync(user));

        // Assert.
        Assert.That(ex.Errors, Is.Not.Empty);
    }

    [Test]
    public async Task GetUsersAsync_ShouldReturnAllUsers()
    {
        // Arrange.
        var userEntity = new User
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            DateOfBirth = new DateTime(1990, 1, 1),
            PhoneNumber = "1234567890"
        };

        _dbContext.Users.Add(userEntity);
        await _dbContext.SaveChangesAsync();

        // Act.
        var users = (await _userService.GetUsersAsync()).ToList();

        // Assert.
        Assert.That(users.Count, Is.EqualTo(1));
        Assert.That(users[0].FirstName, Is.EqualTo(userEntity.FirstName));
    }

    [Test]
    public async Task GetUserByIdAsync_ShouldReturnUserIfExists()
    {
        // Arrange.
        var userEntity = new User
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            DateOfBirth = new DateTime(1990, 1, 1),
            PhoneNumber = "1234567890"
        };

        _dbContext.Users.Add(userEntity);
        await _dbContext.SaveChangesAsync();

        // Act.
        var user = await _userService.GetUserByIdAsync(userEntity.Id);

        // Assert.
        Assert.That(user, Is.Not.Null);
        Assert.That(user.FirstName, Is.EqualTo(userEntity.FirstName));
    }

    [Test]
    public async Task UpdateUserAsync_ShouldUpdateExistingUser()
    {
        // Arrange.
        var userEntity = new User
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            DateOfBirth = new DateTime(1990, 1, 1),
            PhoneNumber = "1234567890"
        };

        _dbContext.Users.Add(userEntity);
        await _dbContext.SaveChangesAsync();

        var updatedUser = new UpdateUserDto
        {
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane.smith@example.com",
            DateOfBirth = new DateTime(1985, 5, 15),
            PhoneNumber = "0987654321"
        };

        // Act.
        var result = await _userService.UpdateUserAsync(userEntity.Id, updatedUser);

        // Assert.
        Assert.That(result, Is.Not.Null);
        Assert.That(result.FirstName, Is.EqualTo(updatedUser.FirstName));
        Assert.That(result.LastName, Is.EqualTo(updatedUser.LastName));
    }

    [Test]
    public async Task DeleteUserAsync_ShouldRemoveUserIfExists()
    {
        // Arrange.
        var userEntity = new User
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            DateOfBirth = new DateTime(1990, 1, 1),
            PhoneNumber = "1234567890"
        };

        _dbContext.Users.Add(userEntity);
        await _dbContext.SaveChangesAsync();

        // Act.
        var result = await _userService.DeleteUserAsync(userEntity.Id);
        var usersInDb = await _dbContext.Users.ToListAsync();

        // Assert.
        Assert.That(result, Is.True);

        Assert.That(usersInDb.Count, Is.EqualTo(0));
    }

}
