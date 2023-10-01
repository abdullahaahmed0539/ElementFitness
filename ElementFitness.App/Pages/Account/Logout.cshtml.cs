using ElementFitness.App.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ElementFitness.App.Pages
{
    
    public class LogoutModel : PageModel
    {

        private readonly SignInManager<IdentityUser> _signInManager;

        public LogoutModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet(string returnUrl)
        {
            try
            {
                await _signInManager.SignOutAsync();
                return RedirectToPage("./Login");
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                Log.Warning(returnUrl);
                if(Url.IsLocalUrl(returnUrl))
                    return RedirectToPage(returnUrl);
                return RedirectToPage("/Index");
            }
            
        }
            
    }
}