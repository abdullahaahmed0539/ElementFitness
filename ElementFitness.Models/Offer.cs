using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElementFitness.Models {
    
    [Table("tblOffers")]
    public class Offer {

        [Key]
        [Column("Offer_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OfferID { get; set; }

        [Required]
        [Column("Name")]
        [StringLength(50)]  
        public string? Name { get; set; }

        [Required]
        [Column("Description")]  
        public string? Description { get; set; }

        [Required]
        [Column("Valid_Upto")]
        public string? ValidUpto { get; set; } 

        [Column("Image_Link")]    
        [DataType(DataType.ImageUrl)]
        public string? ImageLink { get; set; } 

        [Required]
        [Column("Created_On")]
        public DateTime? CreatedOn { get; set; }

        [Required]
        [Column("Active")]
        public bool? Active { get; set; } = false;    
    }
}



