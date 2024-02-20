using ElementFitness.Models;

namespace ElementFitness.BL.Interfaces
{
    public interface IJobListingService
    {
        public IEnumerable<Job>? GetAll();
        public IEnumerable<Job>? GetAllActive();
        public Job? GetById(int id);
        public Job? GetByName(string JobName);
        public Task<Job>? AddAsync(Job newInstance);
        public void SendProfileByEmail(JobApplicant jobApplicant, MemoryStream resumeUpload, string jobTitle);
        public Task<bool> UpdateAsync(Job updatedObj);
        public Task<bool> DeleteAsync(int id);
    }
}