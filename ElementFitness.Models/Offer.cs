using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElementFitness.Models {
    
    [Table("tblOffers")]
    public class Offer {

        [Key]
        [Column("Offer_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OfferID { get; set; }

        [Required(ErrorMessage = "Offer name is required.")]
        [Column("Name")]
        [StringLength(50)]  
        public string? Name { get; set; }

        [Required(ErrorMessage = "Offer description is required.")]
        [Column("Description")]  
        [StringLength(400)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Validity period is required.")]
        [Column("Valid_Upto")]
        [StringLength(30)]
        public string? ValidUpto { get; set; } 

        [Column("Image_Link")]    
        [DataType(DataType.ImageUrl)]
        public string? ImageLink { get; set; } 

        [Column("Created_On")]
        public DateTime? CreatedOn { get; set; }

        [Column("Active")]
        public bool? Active { get; set; } = false;    
    }
}



