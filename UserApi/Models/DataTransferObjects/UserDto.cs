using System.ComponentModel.DataAnnotations;

namespace WebApi.UserApi.Models.DataTransferObjects;

public class UserDto
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string? LastName { get; set; }

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = "0000000000";

    public DateTime DateOfBirth { get; set; } = DateTime.UtcNow.AddYears(-18);

    public int Age { get; set; } // Calculated, not stored in DB
}
