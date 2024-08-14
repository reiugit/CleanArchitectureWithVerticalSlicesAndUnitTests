namespace CleanArchitectureWithVerticalSlicesAndUnitTests.Features.Users;

public record RegisterUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password);
