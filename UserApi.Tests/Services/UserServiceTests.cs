namespace WebApi.UserApi.Tests.Services;

using FluentValidation;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Moq;

using NUnit.Framework;

using Data;
using Models;
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
        throw new NotImplementedException();
    }

    [Test]
    public void AddUserAsync_ShouldThrowValidationException_WhenInvalidUser()
    {
        throw new NotImplementedException();
    }

    [Test]
    public async Task GetUsersAsync_ShouldReturnAllUsers()
    {
        throw new NotImplementedException();
    }

    [Test]
    public async Task GetUserByIdAsync_ShouldReturnUserIfExists()
    {
        throw new NotImplementedException();
    }

    [Test]
    public async Task UpdateUserAsync_ShouldUpdateExistingUser()
    {
        throw new NotImplementedException();
    }

    [Test]
    public async Task DeleteUserAsync_ShouldRemoveUserIfExists()
    {
        throw new NotImplementedException();
    }

}
