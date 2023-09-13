using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElementFitness.Models {
    
    [Table("tblPrograms")]
    public class Program {

        [Key]
        [Column("Program_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProgramID { get; set; }

        [Required]
        [Column("Name")]
        [StringLength(50)]  
        public string? Name { get; set; }

        [Required]
        [Column("Description")]  
        public string? Description { get; set; }

        [Required]
        [Column("Day_And_Timing")]
        [StringLength(50)]  
        public string? DayAndTiming { get; set; } 

        [Required]
        [Column("Trainers")]
        public string? Trainers { get; set; } 

        [Column("Other_Details")]
        public string? OtherDetails { get; set; } 

        [Required]
        [Column("Charges")]
        public string? Charges { get; set; } 

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



