using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElementFitness.Models
{
    [Table("tblSocials")]
    public class Social
    {

        [Key]
        [Column("Socials_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SocialID { get; set; }

        [Required]
        [Column("Social_Platform")]
        [StringLength(100)]
        public string? SocialPlatform { get; set; }

        [Required]
        [Column("Social_Link")]
        [StringLength(500)]
        public string? SocialLink { get; set; }

    }
}
