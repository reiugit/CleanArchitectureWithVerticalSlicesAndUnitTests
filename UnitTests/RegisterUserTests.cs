using CleanArchitectureWithVerticalSlicesAndUnitTests.Features.Users;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace UnitTests;

public class RegisterUserTests
{
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly IPasswordHasher _passwordHasher = Substitute.For<IPasswordHasher>();
    private readonly RegisterUser _registerUser;
    private readonly RegisterUserRequest _request;

    public RegisterUserTests()
    {
        _registerUser = new RegisterUser(_userRepository, _passwordHasher);
        _request = new RegisterUserRequest(
            "Test",
            "User",
            "test@test.com",
            "password");
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenEmailAlreadyExists()
    {
        // Arrange
        _userRepository.GetByEmail(_request.Email).Returns(new User());

        // Act
        Func<Task> act = async () => await _registerUser.Handle(_request);

        // Assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Email already exists");
        await _userRepository.Received(1).GetByEmail(_request.Email);
        await _userRepository.DidNotReceive().Add(Arg.Any<User>());
    }

    [Fact]
    public async Task Handle_ShouldCreateUser_WhenEmailDoesNotExist()
    {
        // Arrange
        _userRepository.GetByEmail(_request.Email).ReturnsNull();
        _passwordHasher.HashPassword(_request.Password).Returns("hashedpassword");

        // Act
        var user = await _registerUser.Handle(_request);

        // Assert
        user.Should().NotBeNull();

        user.Email.Should().Be(_request.Email);
        user.FirstName.Should().Be(_request.FirstName);
        user.LastName.Should().Be(_request.LastName);
        user.PasswordHash.Should().Be("hashedpassword");
        
        await _userRepository.Received(1).Add(user);
    }
}
