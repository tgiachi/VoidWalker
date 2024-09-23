using VoidWalker.AuthService.Entities;
using VoidWalker.Engine.Database.Interfaces.Dao;
using VoidWalker.Engine.Database.Interfaces.Seed;

namespace VoidWalker.AuthService.Seeds;

public class RoleAndUserSeed : IDbSeed
{
    private readonly ILogger _logger;
    private readonly IBaseDataAccess<RoleEntity> _roleDataAccess;
    private readonly IBaseDataAccess<UserEntity> _userDataAccess;
    private readonly IBaseDataAccess<UserRoleEntity> _userRoleDataAccess;


    private readonly List<string> _roles = new()
    {
        "admin",
        "gm",
        "user"
    };


    public RoleAndUserSeed(
        IBaseDataAccess<RoleEntity> roleDataAccess, IBaseDataAccess<UserEntity> userDataAccess,
        IBaseDataAccess<UserRoleEntity> userRoleDataAccess, ILogger<RoleAndUserSeed> logger
    )
    {
        _roleDataAccess = roleDataAccess;
        _userDataAccess = userDataAccess;
        _userRoleDataAccess = userRoleDataAccess;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        var roles = await SeedRolesAsync();

        await SeedUsersAsync(roles, "admin", "password", "admin@voidwalker.com");
    }

    private async Task<List<RoleEntity>> SeedRolesAsync()
    {
        var roles = new List<RoleEntity>();
        foreach (var roleName in _roles)
        {
            var role = await _roleDataAccess.GetSingleByAsync(x => x.Name == roleName);

            if (role == null)
            {
                role = new RoleEntity
                {
                    Name = roleName
                };

                await _roleDataAccess.AddAsync(role);
            }

            roles.Add(role);
        }

        return roles;
    }

    private async Task SeedUsersAsync(List<RoleEntity> roles, string username, string password, string email)
    {
        var user = await _userDataAccess.GetSingleByAsync(x => x.Username == username);


        if (user == null)
        {
            user = new UserEntity
            {
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Email = email
            };

            await _userDataAccess.AddAsync(user);


            _logger.LogInformation("Adding admin user {Username}", username);


            foreach (var role in roles.Where(s => s.Name == "admin"))
            {
                var userRole = new UserRoleEntity
                {
                    UserId = user.Id,
                    RoleId = role.Id
                };

                await _userRoleDataAccess.AddAsync(userRole);
            }
        }
    }
}
