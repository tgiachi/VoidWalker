using Microsoft.EntityFrameworkCore;
using VoidWalker.AuthService.Entities;
using VoidWalker.Engine.Database.Context;

namespace VoidWalker.AuthService.Context;

public class AuthServiceDbContext : BaseDbContext
{
    public DbSet<UserEntity> Users { get; set; }

    public DbSet<RoleEntity> Roles { get; set; }
}
