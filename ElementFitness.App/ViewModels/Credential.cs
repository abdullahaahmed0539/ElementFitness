


using System.ComponentModel.DataAnnotations;

namespace ElementFitness.App.ViewModels {

    public class Credential{

        [Required(ErrorMessage = "Username cannot be empty")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }




}