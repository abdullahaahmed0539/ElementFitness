

using ElementFitness.Models;

namespace ElementFitness.BL.Interfaces
{
    public interface IProgramService
    {
        public IEnumerable<Program>? GetAll();
        public Program? GetById(int id);
        public Program? GetByName(string programName);
        public Task<Program>? AddAsync(Program newInstance);
        public Task<bool> UpdateAsync(Program updatedObj);
        public Task<bool> DeleteAsync(int id);
    }
}