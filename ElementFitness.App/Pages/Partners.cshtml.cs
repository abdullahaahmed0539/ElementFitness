using ElementFitness.BL.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace ElementFitness.App.Pages
{
    public class PartnerModel : PageModel
    {

        private readonly IPartnerService _partnerService;
        public IEnumerable<Models.Partner>? Partners { get; private set;}


        public PartnerModel(IPartnerService partnerService)
        {
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