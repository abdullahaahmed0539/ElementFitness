using System.Diagnostics;
using ElementFitness.App.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Serilog;

namespace ElementFitness.App.Pages
{
    [Authorize(Roles = "administrator")]
    public class SettingsAdminViewModel : PageModel
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;


        [BindProperty]
        public string? CurrentPassword { get; set; }

        [BindProperty]
        public string? NewPassword { get; set; }

        [BindProperty]
        public string? ConfirmPassword { get; set; }

        public SettingsAdminViewModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }



        public async Task OnPost()
        {
            try
            {
                Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync("admin", CurrentPassword, true, false);
                if(!signInResult.Succeeded)
                     throw new Exception("Incorrect current password entered is invalid.");

                if(!NewPassword.Equals(ConfirmPassword))
                    throw new Exception("New Password and Confirm Password fields do not match.");

                IdentityUser user = await _userManager.FindByNameAsync("admin");
                string token = await _userManager.GeneratePasswordResetTokenAsync(user);
                Log.Error(token);
                IdentityResult result = await _userManager.ResetPasswordAsync(user, token, NewPassword);
                ViewData["ErrorMessage"] = "Successfully changed password.";
            }
            catch(Exception ex)
            {
                if (ex.Message.Equals("Incorrect current password entered is invalid.") || ex.Message.Equals("New Password and Confirm Password fields do not match."))
                {
                    ViewData["ErrorMessage"] = ex.Message;
                }

                Log.Error(ex.Message);
                RedirectToPage("../../Error");
            }
        }
    }
}