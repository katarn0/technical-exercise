namespace WebApi.UserApi.Tests.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Moq;

using NUnit.Framework;

using Models;
using Models.DataTransferObjects;
using WebApi.UserApi.Services;
using WebApi.UserApi.Controllers;

[TestFixture]
public class UsersControllerTests
{
    private Mock<IUserService> _userServiceMock;
    private Mock<ILogger<UsersController>> _loggerMock;
    private UsersController _controller;

    [SetUp]
    public void SetUp()
    {
        _userServiceMock = new Mock<IUserService>();
        _loggerMock = new Mock<ILogger<UsersController>>();
        _controller = new UsersController(_userServiceMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetUsers_ReturnsOkResultWithUsers()
    {
        // Arrange
        var users = new List<UserDto>
            {
                new UserDto { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890", DateOfBirth = DateTime.Now.AddYears(-25) },
                new UserDto { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com", PhoneNumber = "0987654321", DateOfBirth = DateTime.Now.AddYears(-30) }
            };
        _userServiceMock.Setup(s => s.GetUsersAsync()).ReturnsAsync(users);

        // Act
        var result = await _controller.GetUsers();

        // Assert
        var okResult = result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.StatusCode, Is.EqualTo(200));
        Assert.That(okResult.Value, Is.EqualTo(users));
    }

    [Test]
    public async Task GetUserById_UserExists_ReturnsOkResultWithUser()
    {
        // Arrange
        var user = new UserDto { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890", DateOfBirth = DateTime.Now.AddYears(-25) };
        _userServiceMock.Setup(s => s.GetUserByIdAsync(1)).ReturnsAsync(user);

        // Act
        var result = await _controller.GetUserById(1);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.StatusCode, Is.EqualTo(200));
        Assert.That(okResult.Value, Is.EqualTo(user));
    }

    [Test]
    public async Task GetUserById_UserDoesNotExist_ReturnsNotFoundResult()
    {
        // Arrange
        _userServiceMock.Setup(s => s.GetUserByIdAsync(1)).ReturnsAsync((UserDto)null);

        // Act
        var result = await _controller.GetUserById(1);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task AddUser_ValidUser_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var user = new UserDto { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890", DateOfBirth = DateTime.Now.AddYears(-25) };
        var createUser = new CreateUserDto { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890", DateOfBirth = DateTime.Now.AddYears(-25) };
        _userServiceMock.Setup(s => s.AddUserAsync(createUser)).ReturnsAsync(user);

        // Act
        var result = await _controller.CreateUser(createUser);

        // Assert
        var createdAtActionResult = result as CreatedAtActionResult;
        Assert.That(createdAtActionResult, Is.Not.Null);
        Assert.That(createdAtActionResult.StatusCode, Is.EqualTo(201));
        Assert.That(createdAtActionResult.Value, Is.EqualTo(user));
    }

    [Test]
    public async Task UpdateUser_UserExists_ReturnsOkResultWithUpdatedUser()
    {
        // Arrange
        var user = new UserDto { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890", DateOfBirth = DateTime.Now.AddYears(-25) };
        var updateUser = new UpdateUserDto { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890", DateOfBirth = DateTime.Now.AddYears(-25) };
        _userServiceMock.Setup(s => s.UpdateUserAsync(1, updateUser)).ReturnsAsync(user);

        // Act
        var result = await _controller.UpdateUser(1, updateUser);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.StatusCode, Is.EqualTo(200));
        Assert.That(okResult.Value, Is.EqualTo(user));
    }

    [Test]
    public async Task UpdateUser_UserDoesNotExist_ReturnsNotFoundResult()
    {
        // Arrange
        var updateUser = new UpdateUserDto { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890", DateOfBirth = DateTime.Now.AddYears(-25) };
        _userServiceMock.Setup(s => s.UpdateUserAsync(1, updateUser)).ReturnsAsync((UserDto)null);

        // Act
        var result = await _controller.UpdateUser(1, updateUser);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task DeleteUser_UserExists_ReturnsNoContentResult()
    {
        // Arrange
        _userServiceMock.Setup(s => s.DeleteUserAsync(1)).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteUser(1);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteUser_UserDoesNotExist_ReturnsNotFoundResult()
    {
        // Arrange
        _userServiceMock.Setup(s => s.DeleteUserAsync(1)).ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteUser(1);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
