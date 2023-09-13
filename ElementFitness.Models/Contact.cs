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

        [Column("Profession")]
        public string? Profession { get; set; } 

        [Required]
        [Column("Contact_Type")]
        public string? ContactType { get; set; } 

        [Required]
        [Column("Created_On")]
        public DateTime? CreatedOn { get; set; } 

        [Column("Converted_On")]
        public DateTime? ConvertedOn { get; set; }

        public ICollection<Enquiry> Enquiries { get; } = new List<Enquiry>(); 
    }



}




