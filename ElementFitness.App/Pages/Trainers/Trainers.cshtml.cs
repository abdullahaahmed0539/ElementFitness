using ElementFitness.BL.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace ElementFitness.App.Pages
{

    public class TrainerModel : PageModel
    {
        private readonly ITrainerService _trainerService;
        public IEnumerable<Models.Trainer>? Trainers { get; private set; }

        public TrainerModel(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        public void OnGet()
        {
            try
            {
                Trainers = _trainerService.GetAll();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}