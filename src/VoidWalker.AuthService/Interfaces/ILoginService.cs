using VoidWalker.Engine.Core.Data;
using VoidWalker.Engine.Core.Data.Auth;
using VoidWalker.Engine.Core.Interfaces.Services;
using VoidWalker.Engine.Core.Interfaces.Services.Base;

namespace VoidWalker.AuthService.Interfaces;

public interface ILoginService : IVoidWalkerService
{
    Task<JwtTokenResult> LoginAsync(string username, string password);

    Task<Guid> RegisterUserAsync(string username, string password, string email, params string[] roles);
}
