using ElementFitness.BL.Interfaces;
using ElementFitness.DAL.Interfaces;
using ElementFitness.Models;

namespace ElementFitness.BL.Services
{
    public class TrainerService: ITrainerService
    {
        private readonly ITrainerRepo _trainerRepo;

        public TrainerService(ITrainerRepo trainerRepo)
        {
            _trainerRepo = trainerRepo;
        }

        public async Task<Trainer>? AddAsync(Trainer newInstance)
        {
            return await _trainerRepo.AddAsync(newInstance);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _trainerRepo.DeleteAsync(id);
        }

        public IEnumerable<Trainer>? GetAll()
        {
            return _trainerRepo.GetAll();
        }

        public Trainer? GetById(int id)
        {
            return _trainerRepo.GetByCondition(t => t.TrainerID == id);
        }

        public Trainer? GetByName(string firstName, string lastName)
        {
            return _trainerRepo.GetByCondition(t => t.FirstName == firstName && t.LastName == lastName);
        }

        public async Task<bool> UpdateAsync(Trainer updatedObj)
        {
            return await _trainerRepo.UpdateAsync(updatedObj);
        }
    }
}