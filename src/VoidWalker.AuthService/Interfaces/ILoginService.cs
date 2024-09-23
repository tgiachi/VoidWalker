using VoidWalker.Engine.Core.Data;
using VoidWalker.Engine.Core.Interfaces.Services;

namespace VoidWalker.AuthService.Interfaces;

public interface ILoginService : IVoidWalkerService
{

    Task<JwtTokenResult> LoginAsync(string username, string password);

}
