﻿namespace CleanArchitectureWithVerticalSlicesAndUnitTests.Features.Users;

public interface IUserRepository
{
    Task<User> GetByEmail(string email);
    Task Add(User user);
}