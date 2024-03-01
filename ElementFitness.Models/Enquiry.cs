using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElementFitness.Models {
    
    [Table("tblEnquiries")]
    public class Enquiry {

        [Key]
        [Column("Enquiry_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnquiryID { get; set; }

       // [Required]
        [Column("Suitable_Contact_Time")]
        [StringLength(25)]  
        public string? SuitableContactTime { get; set; }

       // [Required]
        [Column("Objectives")]  
        public string? Objectives { get; set; }

        //[Required]
        [Column("Message")]
        public string? Message { get; set; } 

     
        //[Required]
        [Column("Created_On")]
        public DateTime? CreatedOn { get; set; }

       // [Required]
        [Column("Read")]
        public bool? Read { get; set; } = false;    

       // [Required]
        [Column("Resolved")]
        public bool? Resolved { get; set; } = false;

       // [Required]
        [Column("ContactID")]
        public int ContactID { get; set; }

        public Contact Contact { get; set; } = null!;  

    }
}



