namespace WebApi.UserApi.Tests.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Moq;

using NUnit.Framework;

using Models;
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
        throw new NotImplementedException();
    }

    [Test]
    public async Task GetUserById_UserExists_ReturnsOkResultWithUser()
    {
        throw new NotImplementedException();
    }

    [Test]
    public async Task GetUserById_UserDoesNotExist_ReturnsNotFoundResult()
    {
        throw new NotImplementedException();
    }

    [Test]
    public async Task AddUser_ValidUser_ReturnsCreatedAtActionResult()
    {
        throw new NotImplementedException();
    }

    [Test]
    public async Task UpdateUser_UserExists_ReturnsOkResultWithUpdatedUser()
    {
        throw new NotImplementedException();
    }

    [Test]
    public async Task UpdateUser_UserDoesNotExist_ReturnsNotFoundResult()
    {
        throw new NotImplementedException();
    }

    [Test]
    public async Task DeleteUser_UserExists_ReturnsNoContentResult()
    {
        throw new NotImplementedException();
    }

    [Test]
    public async Task DeleteUser_UserDoesNotExist_ReturnsNotFoundResult()
    {
        throw new NotImplementedException();
    }
}
