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
    public class JobListingsIndexAdminViewModel : PageModel
    {
        private readonly IJobListingService _jobListingService;
        public IEnumerable<Job>? ActiveJobListings { get; private set;}
        public IEnumerable<Job>? ArchievedJobListings { get; private set;}

        [BindProperty]
        public Job? JobToBeAdded { get; set; }
      
        public JobListingsIndexAdminViewModel(
            IJobListingService jobListingService)
        {
            _jobListingService = jobListingService;
        }

        public IActionResult OnGet()
        {
            try
            {
                IEnumerable<Job>? jobListings = _jobListingService.GetAll();
                ActiveJobListings = jobListings.Where(j => j.Active == true);
                ArchievedJobListings = jobListings.Where(j => j.Active == false);
                return Page();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }


        public async Task<IActionResult> OnPost()
        {
            try
            {
                await _jobListingService.AddAsync(JobToBeAdded);
                return RedirectToPage("./Index"); 
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                ViewData["ErrorMessage"] = ex.Message;
                return OnGet();
            }
        }


        public async Task<IActionResult> OnPostArchiveJobListing(int jobListingId)
        {
            try
            {
                Job? jobListing = _jobListingService.GetById(jobListingId);
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
                    return OnGet();
                }

                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }

        public async Task<IActionResult> OnPostUnarchiveJobListing(int jobListingId)
        {
            try
            {
                Job? jobListing = _jobListingService.GetById(jobListingId);
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
                    return OnGet();
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
                    return OnGet();
                }

                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }
    }
}