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
    public class ProgramsIndexAdminViewModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string WWWRoot;
        private readonly IProgramService _programService;
        public IEnumerable<Models.Program>? Programs { get; private set;}

        [BindProperty]
        public Models.Program? ProgramToBeAdded { get; set; }


        public ProgramsIndexAdminViewModel(
            IWebHostEnvironment environment, 
            IProgramService programService
            )
        {
            _environment = environment;
            WWWRoot = _environment.WebRootPath;
            _programService = programService;
        }

        public IActionResult OnGet()
        {
            try
            {
                Programs = _programService.GetAll();
                return Page();
            }
            catch(Exception ex)
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
                    throw new UploadException("No display image is added. Please upload an image for the program");

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

                string imgLink = Path.Combine(WWWRoot, $"lib/programs/{displayImg.FileName}");
                using FileStream fileStream = new FileStream(imgLink, FileMode.Create);
                await displayImg.CopyToAsync(fileStream);

                ProgramToBeAdded.ImageLink = $"~/lib/programs/{displayImg.FileName}";
                try
                {
                    await _programService.AddAsync(ProgramToBeAdded)!;
                    ProgramToBeAdded.Name = "";
                    ProgramToBeAdded = null;
                    return RedirectToPage("./Index");
                }
                catch(Exception ex)
                {
                    Image.Delete(imgLink);
                    ProgramToBeAdded = null;
                    throw new DatabaseException("An error occurred while adding the program. Please try again later.");
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
    
        public async Task<IActionResult> OnPostDeleteProgram(int programId)
        {
            try
            {

                Models.Program programToBeDeleted =  _programService.GetById(programId);
                string? imageLink = programToBeDeleted?.ImageLink;
                bool successfullyDeleted = await _programService.DeleteAsync(programId);
                if (!successfullyDeleted)
                    throw new DatabaseException("An error occurred while deleting the program. Please try again later.");
                string file = Path.Combine(WWWRoot, $"lib/programs/{Path.GetFileName(imageLink)}");
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