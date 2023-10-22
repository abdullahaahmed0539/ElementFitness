

using ElementFitness.BL.Interfaces;
using ElementFitness.DAL.Interfaces;
using ElementFitness.Models;

namespace ElementFitness.BL.Services
{
    public class ProgramService: IProgramService
    {
        private readonly IProgramRepo _programRepo;

        public ProgramService(IProgramRepo programRepo)
        {
            _programRepo = programRepo;
        }

        public async Task<Program>? AddAsync(Program newInstance)
        {
            return await _programRepo.AddAsync(newInstance);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _programRepo.DeleteAsync(id);
        }

        public IEnumerable<Program>? GetAll()
        {
            return _programRepo.GetAll();
        }

        public Program? GetById(int id)
        {
            return _programRepo.GetByCondition(p => p.ProgramID == id);
        }

        public Program? GetByName(string programName)
        {
            return _programRepo.GetByCondition(p => p.Name == programName);
        }

        public async Task<bool> UpdateAsync(Program updatedObj)
        {
            return await _programRepo.UpdateAsync(updatedObj);
        }
    }
}