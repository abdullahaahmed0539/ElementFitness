using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElementFitness.Models {
    
    [Table("tblPartners")]
    public class Partner {

        [Key]
        [Column("Partner_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PartnerID { get; set; }

        [Required]
        [Column("Name")]
        [StringLength(50)]  
        public string? Name { get; set; }

        [Required]
        [Column("Detail")]  
        public string? Detail { get; set; }

        [Column("Image_Link")]    
        [DataType(DataType.ImageUrl)]
        public string? ImageLink { get; set; } 

        [Required]
        [Column("Created_On")]
        public DateTime? CreatedOn { get; set; }
    }
}



