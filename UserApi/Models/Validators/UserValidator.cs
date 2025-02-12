namespace WebApi.UserApi.Models.Validators;

using FluentValidation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.FirstName)
            .NotEmpty().WithMessage("First Name is required.")
            .MaximumLength(128).WithMessage("First Name must not exceed 128 characters.");

        RuleFor(user => user.LastName)
            .MaximumLength(128).WithMessage("Last Name must not exceed 128 characters.");

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be a valid email address.");

        RuleFor(user => user.DateOfBirth)
            .NotEmpty().WithMessage("Date of Birth is required.")
            .Must(BeAtLeast18YearsOld).WithMessage("User must be at least 18 years old.");

        RuleFor(user => user.PhoneNumber)
            .NotEmpty().WithMessage("Phone Number is required.")
            .Matches("^[0-9]{10}$").WithMessage("Phone Number must be a valid 10-digit number.");
    }

    private bool BeAtLeast18YearsOld(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;

        if (dateOfBirth.Date > today.AddYears(-age)) age--;

        return age >= 18;
    }
}
