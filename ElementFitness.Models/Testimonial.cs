using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ElementFitness.Models {
    
    [Table("tblTestimonials")]
    public class Testimonial {

        [Key]
        [Column("Testimonial_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestimonialID { get; set; }

        [Required]
        [Column("Name")]  
        [StringLength(25)]
        public string? Name { get; set; } 
        
        [Required]
        [Column("Description")]  
        [StringLength(200)]
        public string? Description { get; set; } 

        [Column("Image_Link")]    
        [DataType(DataType.ImageUrl)]
        public string? ImageLink { get; set; } 

        [Required]
        [Column("Stars")]   
        [Display(Name="Stars*")]
        public int? Stars { get; set; } 
       
        [Column("Created_On")]
        
        public DateTime? CreatedOn { get; set; } 

    
    }
}




