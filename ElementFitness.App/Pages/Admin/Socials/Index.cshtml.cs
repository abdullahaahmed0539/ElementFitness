using ElementFitness.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace ElementFitness.App.Pages.Admin.Socials
{
    [Authorize(Roles = "administrator")]
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string? facebookLink { get; set; }
        [BindProperty]
        public string? instagramLink { get; set; }
        private readonly ISocialService _socialService;
        public IEnumerable<Models.Social>? Socials { get; private set; }

        public IndexModel(ISocialService socialService)
        {
            _socialService = socialService;
        }
        public IActionResult OnGet()
        {
            try
            {
                Socials = _socialService.GetAll();
                facebookLink = Socials.FirstOrDefault(x => x.SocialPlatform == "Facebook").SocialLink;
                instagramLink = Socials.FirstOrDefault(x => x.SocialPlatform == "Instagram").SocialLink;
                return Page();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }

        public async Task OnPostFacebookLinkUpdate()
        {
            try
            {
                Models.Social socialplatform = _socialService.GetByName("Facebook");
                socialplatform.SocialLink = facebookLink;
                if (socialplatform == null)
                    throw new Exception("Social Platform Object can not be empty");
                bool result = await _socialService.UpdateLinkAsync(socialplatform);
                if (result)
                    ViewData["ErrorMessage"] = "Facebook Link has been successfully updated.";
                else
                    ViewData["ErrorMessage"] = "Facebook Link was not updated.";
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                RedirectToPage("../../Error");
            }
        }
        public async Task OnPostInstaLinkUpdate()
        {
            try
            {
                Models.Social socialplatform = _socialService.GetByName("Instagram");
                socialplatform.SocialLink = instagramLink;
                if (socialplatform == null)
                    throw new Exception("Social Platform Object can not be empty");
                bool result = await _socialService.UpdateLinkAsync(socialplatform);
                if (result)
                    ViewData["ErrorMessage"] = "Facebook Link has been successfully updated.";
                else
                    ViewData["ErrorMessage"] = "Facebook Link was not updated.";
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                RedirectToPage("../../Error");
            }
        }
    }
}
