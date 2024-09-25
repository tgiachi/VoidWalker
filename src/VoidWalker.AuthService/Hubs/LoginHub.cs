using Microsoft.AspNetCore.SignalR;
using VoidWalker.AuthService.Interfaces;
using VoidWalker.Engine.Network.Packets;

namespace VoidWalker.AuthService.Hubs;

public class LoginHub : Hub
{
    private readonly ILoginService _loginService;

    private readonly ILogger _logger;

    public LoginHub(ILoginService loginService, ILogger<LoginHub> logger)
    {
        _loginService = loginService;
        _logger = logger;
    }

    public async Task LoginAsync(string username, string password)
    {
        var result = await _loginService.LoginAsync(username, password);

        if (!result.Success)
        {
            await Clients.Caller.SendAsync("LoginResult", new LoginResponsePacket(false, null, null, null));
            return;
        }


        await Clients.Caller.SendAsync(
            "LoginResult",
            new LoginResponsePacket(true, result.Token, result.ExpiresAt, Context.ConnectionId)
        );
    }
}
