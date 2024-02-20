using ElementFitness.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElementFitness.App.Pages
{

    public class CareerModel : PageModel
    {
        private readonly IJobListingService _jobListingService;
        public IEnumerable<Models.Job>? Jobs { get; private set;}

        public CareerModel(IJobListingService jobListingService)
        {
            _jobListingService = jobListingService;

        }

        public void OnGet()
        {
            Jobs = _jobListingService.GetAllActive();
        }
    }
}