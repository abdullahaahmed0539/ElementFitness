using ElementFitness.BL.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace ElementFitness.App.Pages
{
    public class MembershipModel : PageModel
    {

        private readonly IProgramService _programService;
        public IEnumerable<Models.Program>? Programs { get; private set;}

        public MembershipModel(IProgramService programService)
        {
            _programService = programService;
        }

        public void OnGet()
        {
            try
            {
                Programs = _programService.GetAll();
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}