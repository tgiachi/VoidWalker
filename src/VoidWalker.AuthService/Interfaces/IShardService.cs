using VoidWalker.Engine.Core.Data.Shared;
using VoidWalker.Engine.Core.Interfaces.Services;

namespace VoidWalker.AuthService.Interfaces;

public interface IShardService : IVoidWalkerService
{
    Task<IEnumerable<ShardObject>> GetShardsAsync();
}
