using Image = System.IO.File ;
using ElementFitness.BL.Interfaces;
using ElementFitness.Utils.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using Mapster;
using ElementFitness.Models;

namespace ElementFitness.App.Pages
{
    [Authorize(Roles = "administrator")]
    public class PartnerDetailAdminViewModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string WWWRoot;
        private readonly IPartnerService _partnerService;
        public Partner? Partner { get; private set;}

        [BindProperty]
        public Partner? UpdatedPartner { get; set; }

        public PartnerDetailAdminViewModel(
            IWebHostEnvironment environment, 
            IPartnerService partnerService
            )
        {
            _environment = environment;
            WWWRoot = _environment.WebRootPath;
            _partnerService = partnerService;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                Partner = _partnerService.GetById(id);
                if (Partner == null)
                    throw new DatabaseException($"No partner with Id={id} exists.");

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

        public async Task<IActionResult> OnPostUpdatePartner(int id, IFormFile imgToBeUpdated)
        {
            try
            {
                Partner? partnerToBeUpdated = _partnerService.GetById(id);
                partnerToBeUpdated = UpdatedPartner.Adapt(partnerToBeUpdated);
                FileStream stream = null;
                string imgLink = "";
                string file = "";
                if(imgToBeUpdated != null)
                {
                    file = Path.Combine(WWWRoot, $"lib/partners/{Path.GetFileName(partnerToBeUpdated.ImageLink)}");
                    stream = new FileStream(file, FileMode.Open, FileAccess.Read);            
                    try
                    {
                        Image.Delete(file);
                    }
                    catch(Exception ex)
                    { 
                        Log.Error(ex.Message);
                        throw new UploadException("An error occurred while updating the partner. Please try again later."); 
                    }

                    Random randomizer = new Random();
                    string randomizerNumber = "";
                    for(int i = 0; i<3; i++)
                    {
                        randomizerNumber +=  " " + randomizer.NextDouble().ToString(); 
                    }
                    imgLink = Path.Combine(WWWRoot, $"lib/partners/{randomizerNumber}{imgToBeUpdated.FileName}");

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
                        throw new UploadException("An error occurred while updating the partners. Please try again later."); 
                    }
                    partnerToBeUpdated.ImageLink = $"~/lib/partners/{randomizerNumber}{imgToBeUpdated.FileName}";
                }

                bool successfullyUpdated = await _partnerService.UpdateAsync(partnerToBeUpdated);
                if (!successfullyUpdated)
                {
                    if(imgToBeUpdated != null)
                    {
                        Image.Delete(imgLink);
                        using FileStream fileStream = new FileStream(file, FileMode.Create);
                        await stream?.CopyToAsync(fileStream);
                    }
                    throw new DatabaseException("An error occurred while updating the partner. Please refresh the page and try again later.");
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

        public async Task<IActionResult> OnPostDeletePartner(int partnerId)
        {
            try
            {
                Partner partnerToBeDeleted =  _partnerService.GetById(partnerId);
                string? imageLink = partnerToBeDeleted?.ImageLink;
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
                    return OnGet(partnerId);
                }

                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }
    
    }
}