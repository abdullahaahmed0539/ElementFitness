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
        public string? FirstName { get; set; }

        [Required]
        [Column("Last_Name")]
        public string? LastName { get; set; } 


        [Required]
        [Column("Job_Title")]  
        [StringLength(20)]  
        public string? JobTitle { get; set; } 

        [Required]
        [Column("Bio")]  
        [StringLength(250)]
        public string? Bio { get; set; } 

        [Column("Image_Link")]    
        [DataType(DataType.ImageUrl)]
        public string? ImageLink { get; set; } 
       
        [Required]
        [Column("Created_On")]
        
        public DateTime? CreatedOn { get; set; } 

    
    }
}




