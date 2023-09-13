using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElementFitness.Models {
    
    [Table("tblJobApplicants")]
    public class JobApplicant {

        [Key]
        [Column("Job_Applicant_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobApplicantID { get; set; }

        [Required]
        [Column("First_Name")]
        public string? FirstName { get; set; }

        [Required]
        [Column("Last_Name")]
        public string? LastName { get; set; } 

        [Required]
        [Column("Email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; } 

        [Required]
        [Column("Mobile_Number")]
        [DataType(DataType.PhoneNumber)]
        public string? MobileNumber { get; set; } 

        [Required]
        [Column("About")]
        public string? About { get; set; }


        [Required]
        [Column("Application_Read")]
        public bool? ApplicationRead { get; set; } = false;

        public Job Job { get; set; } = null!; 

        [Required]
        [Column("Created_On")]
        public DateTime? CreatedOn { get; set; } 
    }



}




