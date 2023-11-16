using Image = System.IO.File ;
using ElementFitness.BL.Interfaces;
using ElementFitness.Utils.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using Mapster;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ElementFitness.App.Pages
{
    [Authorize(Roles = "administrator")]
    public class JobApplicationsDetailAdminViewModel : PageModel
    {
        private readonly IJobApplicantService _jobApplicantService;
        public Models.JobApplicant? JobApplication { get; private set;}


        public JobApplicationsDetailAdminViewModel(
            IJobApplicantService jobApplicantService
            )
        {
            _jobApplicantService = jobApplicantService;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            try
            {
                JobApplication = _jobApplicantService.GetById(id);
                if (JobApplication == null)
                    throw new DatabaseException($"No job application with Id={id} exists.");

                if((bool)!JobApplication.ApplicationRead)
                {
                    JobApplication.ApplicationRead = true;
                    await _jobApplicantService.UpdateAsync(JobApplication);
                }
                   
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


       public async Task<IActionResult> OnPostDeleteJobListing(int jobApplicationId)
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
                    return await OnGet(jobApplicationId);
                }

                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }
    
    }
}