namespace CleanArchitectureWithVerticalSlicesAndUnitTests.Features.Users;

public interface IPasswordHasher
{
    string HashPassword(string password);
}