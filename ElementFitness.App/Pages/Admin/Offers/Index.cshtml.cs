using Image = System.IO.File ;
using ElementFitness.BL.Interfaces;
using ElementFitness.Utils.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace ElementFitness.App.Pages
{
    [Authorize(Roles = "administrator")]
    public class OffersIndexAdminViewModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string WWWRoot;
        private readonly IOfferService _offerService;
        public IEnumerable<Models.Offer>? Offers { get; private set;}

        [BindProperty]
        public Models.Offer? OfferToBeAdded { get; set; }


        public OffersIndexAdminViewModel(
            IWebHostEnvironment environment, 
            IOfferService offerService)
        {
             _environment = environment;
            WWWRoot = _environment.WebRootPath;
            _offerService = offerService;
        }

        public IActionResult OnGet()
        {
            try
            {
                Offers = _offerService.GetAll();
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
                    throw new UploadException("No display image is added. Please upload an image for the offer");

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

                string imgLink = Path.Combine(WWWRoot, $"lib/offers/{randomizerNumber}{displayImg.FileName}");
                using FileStream fileStream = new FileStream(imgLink, FileMode.Create);
                await displayImg.CopyToAsync(fileStream);

                OfferToBeAdded.ImageLink = $"~/lib/offers/{randomizerNumber}{displayImg.FileName}";
                try
                {
                    await _offerService.AddAsync(OfferToBeAdded)!;
                    OfferToBeAdded = null;
                    return RedirectToPage("./Index");
                }
                catch(Exception ex)
                {
                    Image.Delete(imgLink);
                    OfferToBeAdded = null;
                    throw new DatabaseException("An error occurred while adding the offer. Please try again later.");
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

        public async Task<IActionResult> OnPostDeleteOffer(int offerId)
        {
            try
            {
                Models.Offer offerToBeDeleted = _offerService.GetById(offerId);
                string? imageLink = offerToBeDeleted?.ImageLink;
                bool successfullyDeleted = await _offerService.DeleteAsync(offerId);
                if (!successfullyDeleted)
                    throw new DatabaseException("An error occurred while deleting the offer. Please try again later.");
                string file = Path.Combine(WWWRoot, $"lib/offers/{Path.GetFileName(imageLink)}");
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