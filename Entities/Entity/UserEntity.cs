using Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entity
{
    [Table("TB_USER")]
    public class UserEntity : IdentityUser
    {
        [Column("usr_idade")]
        public int Age { get; set; }

        [Column("usr_cell_phone")]
        public string CellPhone { get; set; } = string.Empty;

        [Column("usr_type")]
        public TypesUser? Types { get; set; }
    }
}
