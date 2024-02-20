using ElementFitness.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace ElementFitness.App.Pages
{
    public class JobApplicationViewModel : PageModel
    {
        private readonly IJobListingService _jobListingService;
        public Models.Job? Job { get; private set;}

        [BindProperty]
        public Models.JobApplicant? Applicant { get; set; }


        public JobApplicationViewModel(
            IJobListingService jobListingService
            )
        {
            _jobListingService = jobListingService;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                Job = _jobListingService.GetById(id);
                if (Job == null)
                    return Redirect("./Index");

                return Page();
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                return Redirect("./Index");
            }
        }

        public async Task<IActionResult> OnPost(int id, IFormFile resumeUpload)
        {
            try
            {
                Models.Job job = _jobListingService.GetById(id);
                using MemoryStream memoryStream = new MemoryStream();
                await resumeUpload.CopyToAsync(memoryStream);
                _jobListingService.SendProfileByEmail(Applicant, memoryStream, job.JobTitle);
                ViewData["ErrorMessage"] = "Your application has been successfully sent.";
                return OnGet(id);
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                ViewData["ErrorMessage"] = ex.Message;
                return Redirect("./Index");
            }
        }

    
    }
}
