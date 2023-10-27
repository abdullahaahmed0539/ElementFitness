using Image = System.IO.File ;
using ElementFitness.BL.Interfaces;
using ElementFitness.Utils.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using ElementFitness.Models;

namespace ElementFitness.App.Pages
{
    [Authorize(Roles = "administrator")]
    public class PartnersIndexAdminViewModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string WWWRoot;
        private readonly IPartnerService _partnerService;
        public IEnumerable<Partner>? Partners { get; private set;}

        [BindProperty]
        public Partner? PartnerToBeAdded { get; set; }


        public PartnersIndexAdminViewModel(
            IWebHostEnvironment environment, 
            IPartnerService partnerService)
        {
             _environment = environment;
            WWWRoot = _environment.WebRootPath;
            _partnerService = partnerService;
        }

        public IActionResult OnGet()
        {
            try
            {
                Partners = _partnerService.GetAll();
                return Page();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }

        public async Task<IActionResult> OnPostAsync(IFormFile displayImg)
        {
            try
            {
                if (displayImg == null)
                    throw new UploadException("No display image is added. Please upload an image for the partner");

                if (!ModelState.IsValid)
                {
                    IEnumerable<string> allErrors = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));
                    string errorMessage = "";
                    foreach(string error in allErrors)
                    {
                        errorMessage += $"•{error} \\n";
                    }
                    throw new InvalidModelException(errorMessage);
                }

                Random randomizer = new Random();
                string randomizerNumber = "";
                for(int i = 0; i<3; i++)
                {
                    randomizerNumber +=  " " + randomizer.NextDouble().ToString(); 
                }

                string imgLink = Path.Combine(WWWRoot, $"lib/partners/{randomizerNumber}{displayImg.FileName}");
                using FileStream fileStream = new FileStream(imgLink, FileMode.Create);
                await displayImg.CopyToAsync(fileStream);

                PartnerToBeAdded.ImageLink = $"~/lib/partners/{randomizerNumber}{displayImg.FileName}";
                try
                {
                    await _partnerService.AddAsync(PartnerToBeAdded)!;
                    PartnerToBeAdded = null;
                    return RedirectToPage("./Index");
                }
                catch(Exception ex)
                {
                    Image.Delete(imgLink);
                    PartnerToBeAdded = null;
                    throw new DatabaseException("An error occurred while adding the Partner. Please try again later.");
                }
            }
            catch(Exception ex)
            {
                if (ex is DatabaseException || ex is InvalidModelException || ex is UploadException)
                {
                    ViewData["ErrorMessage"] = ex.Message;
                    return OnGet();
                }

                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }

        public async Task<IActionResult> OnPostDeletePartner(int partnerId)
        {
            try
            {
                Partner offerToBeDeleted = _partnerService.GetById(partnerId);
                string? imageLink = offerToBeDeleted?.ImageLink;
                bool successfullyDeleted = await _partnerService.DeleteAsync(partnerId);
                if (!successfullyDeleted)
                    throw new DatabaseException("An error occurred while deleting the partner. Please try again later.");
                string file = Path.Combine(WWWRoot, $"lib/partners/{Path.GetFileName(imageLink)}");
                Image.Delete(file);
                return RedirectToPage("./Index");
            }
            catch(Exception ex)
            {
                if (ex is DatabaseException)
                {
                    ViewData["ErrorMessage"] = ex.Message;
                    return OnGet();
                }

                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }
    }
}