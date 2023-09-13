using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElementFitness.Models {
    
    [Table("tblJobs")]
    public class Job {

        [Key]
        [Column("Job_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobID { get; set; }

        [Required]
        [Column("Job_Title")]
        [StringLength(50)]  
        public string? JobTitle { get; set; }

        [Required]
        [Column("Requirements")]
        public string? Requirements { get; set; } 

        [Required]
        [Column("Description")]  
        public string? Description { get; set; }

        [Required]
        [Column("Created_On")]

        public DateTime? CreatedOn { get; set; }


        [Required]
        [Column("Active")]
        public bool? Active { get; set; } = false;

        public ICollection<JobApplicant> JobApplications { get; } = new List<JobApplicant>(); 

    
    }
}



