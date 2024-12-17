using Entities.Notifications;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entity
{
    [Table("TB_NEWS")]
    public class News : Notify
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

        [ForeignKey(nameof(ApplicationUser))]
        [Column(Order = 1)]
        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser ApplicationUser { get; set; } = new ApplicationUser();
    }
}
