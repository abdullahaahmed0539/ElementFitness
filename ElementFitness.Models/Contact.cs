using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElementFitness.Models {
    
    [Table("tblContacts")]
    public class Contact {

        [Key]
        [Column("Contact_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactID { get; set; }

        [Required]
        [Column("First_Name")]
        [StringLength(50)]
        public string? FirstName { get; set; }

        [Column("Last_Name")]
        [StringLength(50)]
        public string? LastName { get; set; } 
 
        [Column("Email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; } 

        [Column("Mobile_Number")]
        [DataType(DataType.PhoneNumber)]
        public string? MobileNumber { get; set; } 

        [Column("Profession")]
        public string? Profession { get; set; } 

       // [Required]
        [Column("Contact_Type")]
        [Display(Name ="Contact Type")]
        public string? ContactType { get; set; } 

        //[Required]
        [Column("Created_On")]
        public DateTime? CreatedOn { get; set; } 

        [Column("Converted_On")]
        public DateTime? ConvertedOn { get; set; }

        public ICollection<Enquiry> Enquiries { get; set; } = new List<Enquiry>(); 
    }



}




