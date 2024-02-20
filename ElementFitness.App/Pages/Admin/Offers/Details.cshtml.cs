using Image = System.IO.File ;
using ElementFitness.BL.Interfaces;
using ElementFitness.Utils.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using Mapster;

namespace ElementFitness.App.Pages
{
    [Authorize(Roles = "administrator")]
    public class OfferDetailAdminViewModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string WWWRoot;
        private readonly IOfferService _offerService;
        public Models.Offer? Offer { get; private set;}

        [BindProperty]
        public Models.Offer? UpdatedOffer { get; set; }

        public OfferDetailAdminViewModel(
            IWebHostEnvironment environment, 
            IOfferService offerService
            )
        {
            _environment = environment;
            WWWRoot = _environment.WebRootPath;
            _offerService = offerService;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                return RedirectToPage("../PromoVideo"); //remove if you want to enable offers page
                Offer = _offerService.GetById(id);
                if (Offer == null)
                    throw new DatabaseException($"No offer with Id={id} exists.");

                return Page();
            }
            catch(Exception ex)
            {
                
                if(ex is DatabaseException)
                    return RedirectToPage("./Index");
                
                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }

        public async Task<IActionResult> OnPostUpdateOffer(int id, IFormFile imgToBeUpdated)
        {
            try
            {
                Models.Offer? offerToBeUpdated = _offerService.GetById(id);
                offerToBeUpdated = UpdatedOffer.Adapt(offerToBeUpdated);
                FileStream stream = null;
                string imgLink = "";
                string file = "";
                if(imgToBeUpdated != null)
                {
                    file = Path.Combine(WWWRoot, $"lib/offers/{Path.GetFileName(offerToBeUpdated.ImageLink)}");
                    stream = new FileStream(file, FileMode.Open, FileAccess.Read);            
                    try
                    {
                        Image.Delete(file);
                    }
                    catch(Exception ex)
                    { 
                        Log.Error(ex.Message);
                        throw new UploadException("An error occurred while updating the offer. Please try again later."); 
                    }

                    Random randomizer = new Random();
                    string randomizerNumber = "";
                    for(int i = 0; i<3; i++)
                    {
                        randomizerNumber +=  " " + randomizer.NextDouble().ToString(); 
                    }
                    imgLink = Path.Combine(WWWRoot, $"lib/offers/{randomizerNumber}{imgToBeUpdated.FileName}");

                    try
                    {
                        using FileStream fileStream = new FileStream(imgLink, FileMode.Create);
                        await imgToBeUpdated.CopyToAsync(fileStream);
                    }
                    catch(Exception ex)
                    {
                        Log.Error(ex.Message);
                        using FileStream fileStream = new FileStream(file, FileMode.Create);
                        await stream?.CopyToAsync(fileStream);
                        throw new UploadException("An error occurred while updating the offers. Please try again later."); 
                    }
                    offerToBeUpdated.ImageLink = $"~/lib/offers/{randomizerNumber}{imgToBeUpdated.FileName}";
                }

                bool successfullyUpdated = await _offerService.UpdateAsync(offerToBeUpdated);
                if (!successfullyUpdated)
                {
                    if(imgToBeUpdated != null)
                    {
                        Image.Delete(imgLink);
                        using FileStream fileStream = new FileStream(file, FileMode.Create);
                        await stream?.CopyToAsync(fileStream);
                    }
                    throw new DatabaseException("An error occurred while updating the offer. Please refresh the page and try again later.");
                }
                
                return RedirectToPage("./Details", new { id });
            }
            catch(Exception ex)
            {
                if (ex is DatabaseException || ex is UploadException)
                {
                    ViewData["ErrorMessage"] = ex.Message;
                    return OnGet(id);
                }

                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }

        public async Task<IActionResult> OnPostDeleteOffer(int offerId)
        {
            try
            {
                Models.Offer offerToBeDeleted =  _offerService.GetById(offerId);
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
                    return OnGet(offerId);
                }

                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }
    
    }
}