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
    public class TrainerDetailAdminViewModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string WWWRoot;
        private readonly ITrainerService _trainerService;
        public Models.Trainer? Trainer { get; private set;}

        [BindProperty]
        public Models.Trainer? UpdatedTrainer { get; set; }

        public TrainerDetailAdminViewModel(
            IWebHostEnvironment environment, 
            ITrainerService trainerService
            )
        {
            _environment = environment;
            WWWRoot = _environment.WebRootPath;
            _trainerService = trainerService;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                Trainer = _trainerService.GetById(id);
                if (Trainer == null)
                    throw new DatabaseException($"No trainer with Id={id} exists.");

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

        public async Task<IActionResult> OnPostUpdateTrainer(int id, IFormFile imgToBeUpdated)
        {
            try
            {
                Models.Trainer? trainerToBeUpdated = _trainerService.GetById(id);
                trainerToBeUpdated = UpdatedTrainer.Adapt(trainerToBeUpdated);
                FileStream stream = null;
                string imgLink = "";
                string file = "";
                if(imgToBeUpdated != null)
                {
                    file = Path.Combine(WWWRoot, $"lib/trainers/{Path.GetFileName(trainerToBeUpdated.ImageLink)}");
                    stream = new FileStream(file, FileMode.Open, FileAccess.Read);            
                    try
                    {
                        Image.Delete(file);
                    }
                    catch(Exception ex)
                    { 
                        Log.Error(ex.Message);
                        throw new UploadException("An error occurred while updating the trainer. Please try again later."); 
                    }

                    Random randomizer = new Random();
                    string randomizerNumber = "";
                    for(int i = 0; i<3; i++)
                    {
                        randomizerNumber +=  " " + randomizer.NextDouble().ToString(); 
                    }
                    imgLink = Path.Combine(WWWRoot, $"lib/trainers/{randomizerNumber}{imgToBeUpdated.FileName}");

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
                        throw new UploadException("An error occurred while updating the trainers. Please try again later."); 
                    }
                    trainerToBeUpdated.ImageLink = $"~/lib/trainers/{randomizerNumber}{imgToBeUpdated.FileName}";
                }

                bool successfullyUpdated = await _trainerService.UpdateAsync(trainerToBeUpdated);
                if (!successfullyUpdated)
                {
                    if(imgToBeUpdated != null)
                    {
                        Image.Delete(imgLink);
                        using FileStream fileStream = new FileStream(file, FileMode.Create);
                        await stream?.CopyToAsync(fileStream);
                    }
                    throw new DatabaseException("An error occurred while updating the trainer. Please refresh the page and try again later.");
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

        public async Task<IActionResult> OnPostDeleteTrainer(int trainerId)
        {
            try
            {
                Models.Trainer? trainerToBeDeleted =  _trainerService.GetById(trainerId);
                string? imageLink = trainerToBeDeleted?.ImageLink;
                bool successfullyDeleted = await _trainerService.DeleteAsync(trainerId);
                if (!successfullyDeleted)
                    throw new DatabaseException("An error occurred while deleting the trainer. Please try again later.");
                string file = Path.Combine(WWWRoot, $"lib/trainers/{Path.GetFileName(imageLink)}");
                Image.Delete(file);
                return RedirectToPage("./Index");
            }
            catch(Exception ex)
            {
                if (ex is DatabaseException)
                {
                    ViewData["ErrorMessage"] = ex.Message;
                    return OnGet(trainerId);
                }

                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }
    
    }
}