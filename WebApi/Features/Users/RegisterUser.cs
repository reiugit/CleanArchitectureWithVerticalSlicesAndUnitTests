namespace CleanArchitectureWithVerticalSlicesAndUnitTests.Features.Users;

public class RegisterUser(IUserRepository userRepository, IPasswordHasher passwordHasher)
{
    public async Task<User> Handle(RegisterUserRequest request)
    {
        var existingUser = await userRepository.GetByEmail(request.Email);
        if (existingUser is not null)
        {
            // for brevity no result pattern is used here
            throw new Exception("Email already exists");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = passwordHasher.HashPassword(request.Password)
        };

        await userRepository.Add(user);

        return user;
    }
}
