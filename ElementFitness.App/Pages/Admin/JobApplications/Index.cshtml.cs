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
    public class JobApplicantIndexAdminViewModel : PageModel
    {
        private readonly IJobApplicantService _jobApplicantService;

        public IEnumerable<JobApplicant> UnreadJobApplicants { get; private set; }
        public IEnumerable<JobApplicant> ReadJobApplicants { get; private set; }
        public IEnumerable<JobApplicant> ArchivedJobApplicants { get; private set; }
      
        public JobApplicantIndexAdminViewModel(
            IJobApplicantService jobApplicantService)
        {
            _jobApplicantService = jobApplicantService;
        }

        public IActionResult OnGet()
        {
            try
            {
                IEnumerable<JobApplicant> jobApplicants = _jobApplicantService.GetAll();
                UnreadJobApplicants = jobApplicants.Where(j => j.JobID != 0 && j.ApplicationRead == false);
                ReadJobApplicants = jobApplicants.Where(j => j.JobID != 0 && j.ApplicationRead == true);
                ArchivedJobApplicants = jobApplicants.Where(j => j.JobID == 0);
                return Page();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }


        // public async Task<IActionResult> OnPost()
        // {
        //     try
        //     {
        //         await _jobListingService.AddAsync(JobToBeAdded);
        //         return RedirectToPage("./Index"); 
        //     }
        //     catch(Exception ex)
        //     {
        //         Log.Error(ex.Message);
        //         ViewData["ErrorMessage"] = ex.Message;
        //         return OnGet();
        //     }
        // }



        public async Task<IActionResult> OnPostDeleteJobApplication(int jobApplicationId)
        {
            try
            {
                bool successfullyDeleted = await _jobApplicantService.DeleteAsync(jobApplicationId);
                if (!successfullyDeleted)
                    throw new DatabaseException("An error occurred while deleting the job application. Please try again later.");
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