namespace WebApi.UserApi.Controllers;

using Microsoft.AspNetCore.Mvc;

using Models.DataTransferObjects;
using Services;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserService userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        _logger.LogInformation("Fetching all users.");
        var users = await _userService.GetUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        _logger.LogInformation("Fetching user with ID: {Id}", id);
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            _logger.LogWarning("User with ID: {Id} not found.", id);
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto user)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid user model received.");
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Creating a new user.");
        try
        {
            var createdUser = await _userService.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }
        catch (FluentValidation.ValidationException ve)
        {
            _logger.LogError(ve, "Error occurred while creating a user");
            return StatusCode(400, ve.Message);
        }
        catch (ArgumentException ae)
        {
            _logger.LogError(ae, "Error occurred while creating a user");
            return StatusCode(409, ae.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating a user.");

            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto user)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        throw new NotImplementedException();
    }
}
