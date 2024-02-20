using ElementFitness.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace ElementFitness.App.Pages
{
    public class JobDetailViewModel : PageModel
    {
        private readonly IJobListingService _jobListingService;
        public Models.Job? Job { get; private set;}


        public JobDetailViewModel(
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

    
    }
}