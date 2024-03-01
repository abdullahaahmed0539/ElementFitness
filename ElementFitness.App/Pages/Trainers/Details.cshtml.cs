using ElementFitness.BL.Interfaces;
using ElementFitness.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace ElementFitness.App.Pages.Trainers
{
    public class TrainerDetailsModel : PageModel
    {
        private readonly ITrainerService _trainerService;
        public Models.Trainer? trainer { get; private set; }

        public TrainerDetailsModel(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                trainer = _trainerService.GetById(id);
                if (trainer == null)
                    return Redirect("./Index");

                return Page();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return Redirect("./Index");
            }
        }
    }
}
