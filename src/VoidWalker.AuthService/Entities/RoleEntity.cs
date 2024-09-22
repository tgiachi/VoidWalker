using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VoidWalker.Engine.Database.Base;

namespace VoidWalker.AuthService.Entities;

[Table("roles")]
public class RoleEntity : BaseDbEntity
{
    [MaxLength(200)] public string Name { get; set; }

    [MaxLength(200)] public string? Description { get; set; }

    public virtual ICollection<UserEntity> Users { get; set; }
}
