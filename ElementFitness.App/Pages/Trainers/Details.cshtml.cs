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
        public int prevTrainerId { get; private set; }
        public int nextTrainerId { get; private set; }
        public TrainerDetailsModel(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                trainer = _trainerService.GetById(id);
                Trainer prevTrainer = _trainerService.GetAll().SkipWhile(x => x.TrainerID != id).Skip(1).LastOrDefault();
                Trainer nextTrainer = _trainerService.GetAll().SkipWhile(obj => obj.TrainerID != id).Skip(1).FirstOrDefault();

                if (prevTrainer == null)
                    prevTrainerId = 0;
                else
                    prevTrainerId = prevTrainer.TrainerID;

                if (nextTrainer == null)
                    prevTrainerId = 0;
                else
                    nextTrainerId = nextTrainer.TrainerID;

                if (trainer == null)
                    return Redirect("../Index");

                return Page();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return Redirect("../Index");
            }
        }
    }
}
