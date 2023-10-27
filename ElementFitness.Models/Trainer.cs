using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ElementFitness.Models {
    
    [Table("tblTrainers")]
    public class Trainer {

        [Key]
        [Column("Trainer_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrainerID { get; set; }

        [Required]
        [Column("First_Name")]
        [StringLength(50)]
        public string? FirstName { get; set; }

        [Required]
        [Column("Last_Name")]
        [StringLength(50)]
        public string? LastName { get; set; } 


        [Required]
        [Column("Job_Title")]  
        [StringLength(100)]  
        public string? JobTitle { get; set; } 

        [Required]
        [Column("Bio")]  
        [StringLength(400)]
        public string? Bio { get; set; } 

        [Column("Image_Link")]    
        [DataType(DataType.ImageUrl)]
        public string? ImageLink { get; set; } 
       
        [Column("Created_On")]
        
        public DateTime? CreatedOn { get; set; } 

    
    }
}




