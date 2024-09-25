using Microsoft.EntityFrameworkCore;
using VoidWalker.AuthService.Context;
using VoidWalker.Engine.Database.Impl;
using VoidWalker.Engine.Database.Interfaces.Entities;

namespace VoidWalker.AuthService.Dao;

public class AuthServiceDataAccess<TEntity> : AbstractBaseDataAccess<TEntity, AuthServiceDbContext> where TEntity : class, IBaseDbEntity
{
    public AuthServiceDataAccess(ILogger<TEntity> logger, IDbContextFactory<AuthServiceDbContext> contextFactory) : base(logger, contextFactory)
    {

    }
}
