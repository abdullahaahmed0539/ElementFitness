using ElementFitness.Models;

namespace ElementFitness.BL.Interfaces
{
    public interface IJobApplicantService
    {
        public IEnumerable<JobApplicant>? GetAll();
        public JobApplicant? GetById(int id);
        public Task<JobApplicant>? AddAsync(JobApplicant newInstance);
        public Task<bool> UpdateAsync(JobApplicant updatedObj);
        public Task<bool> DeleteAsync(int id);
    }
}