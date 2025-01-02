using Entities.Notifications;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entity
{
    [Table("TB_NEWS")]
    public class NewsEntity : Notify
    {
        [Column("ntf_id")]
        public int Id { get; set; }

        [Column("ntf_title")]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [Column("ntf_information")]
        [MaxLength(255)]
        public string Information { get; set; } = string.Empty;

        [Column("ntf_status")]
        public bool Status { get; set; }

        [Column("ntf_date_register")]
        public DateTime DateRegister { get; set; }

        [Column("ntf_date_updating")]
        public DateTime DateUpdating { get; set; }

        [ForeignKey(nameof(UserEntity))]
        [Column(Order = 1)]
        public string UserId { get; set; } = string.Empty;
        public virtual UserEntity UserEntity { get; set; } = new UserEntity();
    }
}
