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
    public class JobListingDetailAdminViewModel : PageModel
    {
        private readonly IJobListingService _jobListingService;
        public Models.Job? Job { get; private set;}

        [BindProperty]
        public Models.Job? UpdatedJob { get; set; }

        public JobListingDetailAdminViewModel(
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
                    throw new DatabaseException($"No job with Id={id} exists.");

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


        public async Task<IActionResult> OnPostArchiveJobListing(int jobListingId)
        {
            try
            {
                Models.Job? jobListing = _jobListingService.GetById(jobListingId);
                if(jobListing != null)
                {
                    jobListing.Active = false;
                    try
                    {
                        await _jobListingService.UpdateAsync(jobListing);
                        return RedirectToPage("./Index"); 
                    }
                    catch(Exception ex)
                    {
                        throw new DatabaseException("An error occurred while archiving the job listing. Please try again later.");
                    }
                }
                throw new NullReferenceException($"No such job losting exist with the ID = {jobListingId}");
            }
            catch(Exception ex)
            {
                if (ex is DatabaseException || ex is NullReferenceException)
                {
                    ViewData["ErrorMessage"] = ex.Message;
                    Log.Error(ViewData["ErrorMessage"].ToString());
                    return OnGet(jobListingId);
                }

                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }

        public async Task<IActionResult> OnPostUnarchiveJobListing(int jobListingId)
        {
            try
            {
                Models.Job? jobListing = _jobListingService.GetById(jobListingId);
                if(jobListing != null)
                {
                    jobListing.Active = true;
                    try
                    {
                        await _jobListingService.UpdateAsync(jobListing);
                        return RedirectToPage("./Index"); 
                    }
                    catch(Exception ex)
                    {
                        throw new DatabaseException("An error occurred while unarchiving the job listing. Please try again later.");
                    }
                }
                throw new NullReferenceException($"No such job losting exist with the ID = {jobListingId}");
            }
            catch(Exception ex)
            {
                if (ex is DatabaseException || ex is NullReferenceException)
                {
                    ViewData["ErrorMessage"] = ex.Message;
                    Log.Error(ViewData["ErrorMessage"].ToString());
                    return OnGet(jobListingId);
                }

                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }


        public async Task<IActionResult> OnPostUpdateJob(int id)
        {
            Log.Error(id.ToString());
            try
            {
                Models.Job? jobToBeUpdated = _jobListingService.GetById(id);
                bool? jobstatus = jobToBeUpdated.Active;
                jobToBeUpdated = UpdatedJob.Adapt(jobToBeUpdated);
                jobToBeUpdated.Active = jobstatus;
                bool successfullyUpdated = await _jobListingService.UpdateAsync(jobToBeUpdated);
                if (!successfullyUpdated)
                    throw new DatabaseException("An error occurred while updating the offer. Please refresh the page and try again later.");
                
                
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

       public async Task<IActionResult> OnPostDeleteJobListing(int jobListingId)
        {
            try
            {
                bool successfullyDeleted = await _jobListingService.DeleteAsync(jobListingId);
                if (!successfullyDeleted)
                    throw new DatabaseException("An error occurred while deleting the enquiry. Please try again later.");
                return RedirectToPage("./Index");
            }
            catch(Exception ex)
            {
                if (ex is DatabaseException)
                {
                    ViewData["ErrorMessage"] = ex.Message;
                    return OnGet(jobListingId);
                }

                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }
    
    }
}