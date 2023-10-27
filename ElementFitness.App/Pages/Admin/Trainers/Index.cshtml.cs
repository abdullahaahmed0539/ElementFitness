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
    public class TrainersIndexAdminViewModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string WWWRoot;
        private readonly ITrainerService _trainerService;
        public IEnumerable<Models.Trainer>? Trainers { get; private set;}

        [BindProperty]
        public Models.Trainer? TrainerToBeAdded { get; set; }


        public TrainersIndexAdminViewModel(
            IWebHostEnvironment environment, 
            ITrainerService trainerService)
        {
             _environment = environment;
            WWWRoot = _environment.WebRootPath;
            _trainerService = trainerService;
        }

        public IActionResult OnGet()
        {
            try
            {
                Trainers = _trainerService.GetAll();
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
                    throw new UploadException("No display image is added. Please upload an image for the trainer");

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

                string imgLink = Path.Combine(WWWRoot, $"lib/trainers/{randomizerNumber}{displayImg.FileName}");
                using FileStream fileStream = new FileStream(imgLink, FileMode.Create);
                await displayImg.CopyToAsync(fileStream);

                TrainerToBeAdded.ImageLink = $"~/lib/trainers/{randomizerNumber}{displayImg.FileName}";
                try
                {
                    await _trainerService.AddAsync(TrainerToBeAdded)!;
                    TrainerToBeAdded = null;
                    return RedirectToPage("./Index");
                }
                catch(Exception ex)
                {
                    Image.Delete(imgLink);
                    TrainerToBeAdded = null;
                    throw new DatabaseException("An error occurred while adding the trainer. Please try again later.");
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

        public async Task<IActionResult> OnPostDeleteTrainer(int trainerId)
        {
            try
            {
                Models.Trainer trainerToBeDeleted = _trainerService.GetById(trainerId);
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
                    return OnGet();
                }

                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }
    }
}