using VoidWalker.Engine.Core.Interfaces.Services;
using VoidWalker.Engine.Core.Interfaces.Services.Base;
using VoidWalker.Engine.Network.Data.Shared;

namespace VoidWalker.AuthService.Interfaces;

public interface IShardService : IVoidWalkerService
{
    Task<IEnumerable<ShardObject>> GetShardsAsync();
}
