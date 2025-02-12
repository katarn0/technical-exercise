namespace WebApi.UserApi.Models.DataTransferObjects;

using System.ComponentModel.DataAnnotations;

public class CreateUserDto
{
    [MaxLength(128)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(128)]
    public string? LastName { get; set; }

    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Phone]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
    public string PhoneNumber { get; set; } = "0000000000";

    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; } = DateTime.UtcNow.AddYears(-18);
}
