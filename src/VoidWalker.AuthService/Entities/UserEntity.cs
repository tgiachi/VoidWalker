using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VoidWalker.Engine.Database.Base;

namespace VoidWalker.AuthService.Entities;

[Table("users")]
public class UserEntity : BaseDbEntity
{
    [MaxLength(200)] public string Username { get; set; }

    [MaxLength(200)] public string PasswordHash { get; set; }

    [MaxLength(200)] public string Email { get; set; }

    public bool IsEmailVerified { get; set; }

    public bool IsActive { get; set; } 

    public virtual ICollection<RoleEntity> Roles { get; set; }
}
