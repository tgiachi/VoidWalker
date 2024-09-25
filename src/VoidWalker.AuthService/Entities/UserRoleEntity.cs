using System.ComponentModel.DataAnnotations.Schema;
using VoidWalker.Engine.Database.Base;

namespace VoidWalker.AuthService.Entities;

[Table("user_roles")]
public class UserRoleEntity : BaseDbEntity
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    public virtual UserEntity User { get; set; }

    public virtual RoleEntity Role { get; set; }
}
