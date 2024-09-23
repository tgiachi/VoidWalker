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

    private readonly JwtConfigData _jwtConfig;


    public LoginService(ILogger<LoginService> logger, IBaseDataAccess<UserEntity> userDao, IOptions<JwtConfigData> jwtConfig)
    {
        _logger = logger;
        _userDao = userDao;
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
}
