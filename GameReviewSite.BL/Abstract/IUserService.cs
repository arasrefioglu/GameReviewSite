﻿using GameReviewSite.Entities.Concrete;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int userId);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int userId);  // Parametre türünü int olarak değiştirin
    Task AssignRoleAsync(int userId, string role);
}
