using ElementFitness.App.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ElementFitness.App.Pages
{
    
    public class AdminLoginModel : PageModel
    {

        [BindProperty]
        public Credential? Credential { get; set; }
        private readonly SignInManager<IdentityUser> _signInManager;

        public bool DisplayErrorMessage { get; private set; }

        public AdminLoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult OnGet()
        {
            try
            {
                if(Convert.ToBoolean(User?.Identity?.IsAuthenticated))
                    return RedirectToPage("../Admin/Home");
                
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                DisplayErrorMessage = false;
                if (string.IsNullOrWhiteSpace(Credential?.Username) || string.IsNullOrWhiteSpace(Credential?.Password))
                    return Page();
                
                SignInResult signInResult = await _signInManager.PasswordSignInAsync(Credential!.Username, Credential!.Password, true, false);
                return signInResult.Succeeded ? RedirectToPage("../Admin/Home"): throw new Exception("Incorrect Username or Password", new Exception("AuthenticationError"));
            }
            catch(Exception ex)
            {
                string errorMessage;
                if(ex.InnerException?.Message != "AuthenticationError"){
                    errorMessage = "Internal Server Error. Please contact administrator";
                    Log.Error(ex, ex.Message);
                }
                else{
                    errorMessage = ex.Message;
                }
                ModelState.AddModelError(string.Empty, errorMessage);
                DisplayErrorMessage = true;
                return Page();
            }
        }
    }
}