using Microsoft.Extensions.Options;
using VoidWalker.AuthService.Entities;
using VoidWalker.AuthService.Interfaces;
using VoidWalker.Engine.Core.Data;
using VoidWalker.Engine.Database.Interfaces.Dao;

namespace VoidWalker.AuthService.Service;

public class LoginService : ILoginService
{
    private readonly ILogger _logger;

    private readonly IBaseDataAccess<UserEntity> _userDao;

    private readonly IBaseDataAccess<RoleEntity> _roleDao;

    private readonly IBaseDataAccess<UserRoleEntity> _userRoleDao;

    private readonly JwtConfigData _jwtConfig;


    public LoginService(
        ILogger<LoginService> logger, IBaseDataAccess<UserEntity> userDao, IOptions<JwtConfigData> jwtConfig,
        IBaseDataAccess<RoleEntity> roleDao, IBaseDataAccess<UserRoleEntity> userRoleDao
    )
    {
        _logger = logger;
        _userDao = userDao;
        _roleDao = roleDao;
        _userRoleDao = userRoleDao;
        _jwtConfig = jwtConfig.Value;
    }

    public async Task<JwtTokenResult> LoginAsync(string username, string password)
    {
        var user = await _userDao.GetSingleByAsync(x => x.Username == username);

        if (user == null)
        {
            _logger.LogWarning("User '{username}' not found.", username);
            return new JwtTokenResult(false, null, null);
        }

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            _logger.LogWarning("Invalid password for user '{username}'.", username);
            return new JwtTokenResult(false, null, null);
        }

        return new JwtTokenResult(true, "", DateTime.UtcNow.AddHours(1));
    }

    public async Task<Guid> RegisterUserAsync(string username, string password, string email, params string[] roles)
    {
        var exists = await _userDao.GetSingleByAsync(x => x.Username == username || x.Email == email);

        if (exists != null)
        {
            _logger.LogError("User '{username}' or email '{email}' already exists.", username, email);
            throw new Exception("User already exists.");
        }

        var user = new UserEntity
        {
            Username = username,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
        };

        await _userDao.AddAsync(user);


        foreach (var roleName in roles)
        {
            var role = await _roleDao.GetSingleByAsync(x => x.Name == roleName);

            if (role == null)
            {
                _logger.LogError("Role '{roleName}' not found.", roleName);
                throw new Exception("Role not found.");
            }

            var userRole = new UserRoleEntity
            {
                UserId = user.Id,
                RoleId = role.Id
            };

            await _userRoleDao.AddAsync(userRole);
        }


        return user.Id;
    }
}
