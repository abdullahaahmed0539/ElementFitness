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
    public class ProgramDetailAdminViewModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string WWWRoot;
        private readonly IProgramService _programService;
        public Models.Program? Program { get; private set;}

        [BindProperty]
        public Models.Program? UpdatedProgram { get; set; }

        public ProgramDetailAdminViewModel(
            IWebHostEnvironment environment, 
            IProgramService programService
            )
        {
            _environment = environment;
            WWWRoot = _environment.WebRootPath;
            _programService = programService;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                Program = _programService.GetById(id);
                if (Program == null)
                    throw new DatabaseException($"No program with Id={id} exists.");

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

        public async Task<IActionResult> OnPostUpdateProgram(int id, IFormFile imgToBeUpdated)
        {
            try
            {
                Models.Program? programToBeUpdated = _programService.GetById(id);
                programToBeUpdated = UpdatedProgram.Adapt(programToBeUpdated);
                FileStream stream = null;
                string imgLink = "";
                string file = "";
                if(imgToBeUpdated != null)
                {
                    file = Path.Combine(WWWRoot, $"lib/programs/{Path.GetFileName(programToBeUpdated.ImageLink)}");
                    stream = new FileStream(file, FileMode.Open, FileAccess.Read);            

                    try
                    {
                        Image.Delete(file);
                    }
                    catch(Exception ex)
                    { 
                        Log.Error(ex.Message);
                        throw new UploadException("An error occurred while updating the program. Please try again later."); 
                    }
                    imgLink = Path.Combine(WWWRoot, $"lib/programs/{imgToBeUpdated.FileName}");
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
                        throw new UploadException("An error occurred while updating the program. Please try again later."); 
                    }
                    programToBeUpdated.ImageLink = $"~/lib/programs/{imgToBeUpdated.FileName}";
                }

                bool successfullyUpdated = await _programService.UpdateAsync(programToBeUpdated);
                if (!successfullyUpdated)
                {
                    if(imgToBeUpdated != null)
                    {
                        Image.Delete(imgLink);
                        using FileStream fileStream = new FileStream(file, FileMode.Create);
                        await stream?.CopyToAsync(fileStream);
                    }
                    throw new DatabaseException("An error occurred while updating the program. Please refresh the page and try again later.");
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
                    return OnGet(programId);
                }

                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }
    
    }
}