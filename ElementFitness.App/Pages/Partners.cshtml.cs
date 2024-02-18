using ElementFitness.BL.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace ElementFitness.App.Pages
{
    public class PartnerModel : PageModel
    {

        private readonly IPartnerService _partnerService;
        private readonly IWebHostEnvironment _environment;
        private readonly string WWWRoot;
        public IEnumerable<Models.Partner>? Partners { get; private set;}


        public PartnerModel(IPartnerService partnerService, IWebHostEnvironment environment)
        {
            _environment = environment;
            WWWRoot = _environment.WebRootPath;
            _partnerService = partnerService;
        }

        public void OnGet()
        {
            try
            {
                Partners = _partnerService.GetAll();

            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}