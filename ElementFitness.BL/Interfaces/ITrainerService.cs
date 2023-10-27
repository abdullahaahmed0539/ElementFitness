using ElementFitness.Models;

namespace ElementFitness.BL.Interfaces
{
    public interface ITrainerService
    {
        public IEnumerable<Trainer>? GetAll();
        public Trainer? GetById(int id);
        public Trainer? GetByName(string firstName, string lastName);
        public Task<Trainer>? AddAsync(Trainer newInstance);
        public Task<bool> UpdateAsync(Trainer updatedObj);
        public Task<bool> DeleteAsync(int id);
    }
}