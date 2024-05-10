using GameReviewSite.Entities.Concrete;
using Microsoft.AspNetCore.Identity;

public class UserService : IUserService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IRepository<User> _userRepository;

    public UserService(
         UserManager<IdentityUser> userManager,
         RoleManager<IdentityRole> roleManager,
         IRepository<User> userRepository)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        return await _userRepository.GetByIdAsync(userId);
    }

    public async Task AddUserAsync(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        await _userRepository.AddAsync(user);
        await _userRepository.SaveAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        _userRepository.Update(user);
        await _userRepository.SaveAsync();
    }

    public async Task DeleteUserAsync(int userId)
    {
        var user = await GetUserByIdAsync(userId);
        if (user != null)
        {
            _userRepository.RemoveAsync(user);
            await _userRepository.SaveAsync();
        }
    }

    public async Task AssignRoleAsync(int userId, string roleName)
    {
        // Varsayılan rol isimlerini kullan
        var roles = new[] { "Admin", "User" };

        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user != null)
        {
            // Rolün var olduğundan emin ol (Yalnızca admin veya user olabilir bu senaryoda)
            roleName = roles.FirstOrDefault(r => r.Equals(roleName, StringComparison.OrdinalIgnoreCase));
            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Geçersiz rol ismi.", nameof(roleName));
            }

            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                // Rol mevcut değilse, oluştur
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // Kullanıcıya rol atayın
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Rol ataması yapılamadı.");
            }
        }
        else
        {
            throw new InvalidOperationException("Kullanıcı bulunamadı.");
        }
    }

}
