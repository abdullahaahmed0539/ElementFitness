using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElementFitness.Models {
    
    [Table("tblPrograms")]
    public class Program
    {

        [Key]
        [Column("Program_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProgramID { get; set; }

        [Required(ErrorMessage = "Program name is required.")]
        [Column("Name")]
        [StringLength(50)]  
        public string? Name { get; set; }

        [Required(ErrorMessage = "Program description is required.")]
        [Column("Description")]  
        [StringLength(250)]  
        public string? Description { get; set; }

        [Required(ErrorMessage = "Program day and time is required.")]
        [Column("Day_And_Timing")]
        [StringLength(50)]  
        public string? DayAndTiming { get; set; } 

        [Required(ErrorMessage = "Program trainers are required.")]
        [Column("Trainers")]
        [StringLength(100)]  
        public string? Trainers { get; set; } 

        [Column("Other_Details")]
        [StringLength(100)]  
        public string? OtherDetails { get; set; } 

        [Required(ErrorMessage = "Program charges are required.")]
        [Column("Charges")]
        [StringLength(100)]  
        public string? Charges { get; set; } 

        [Column("Image_Link")]    
        [DataType(DataType.ImageUrl)]
        public string? ImageLink { get; set; } 

        [Column("Created_On")]
        public DateTime? CreatedOn { get; set; }

        [Column("Active")]
        public bool? Active { get; set; } = false;
    }
}



