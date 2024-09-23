using Microsoft.EntityFrameworkCore;
using VoidWalker.AuthService.Entities;
using VoidWalker.Engine.Database.Context;

namespace VoidWalker.AuthService.Context;

public class AuthServiceDbContext : BaseDbContext
{
    public DbSet<UserEntity> Users { get; set; }

    public DbSet<RoleEntity> Roles { get; set; }

    public DbSet<UserRoleEntity> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseNpgsql(
                    @"Server=127.0.0.1;Port=5432;Database=voidwalker_auth_db;User Id=postgres;Password=password;"
                )
                .UseSnakeCaseNamingConvention();
        }

        base.OnConfiguring(optionsBuilder);
    }
}
