using ElementFitness.BL.Interfaces;
using ElementFitness.DAL.Interfaces;
using ElementFitness.Models;

namespace ElementFitness.BL.Services
{
    public class JobListingService: IJobListingService
    {
        private readonly IJobListingRepo _jobListingRepo;

        public JobListingService(IJobListingRepo jobListingRepo)
        {
            _jobListingRepo = jobListingRepo;
        }

        public async Task<Job>? AddAsync(Job newInstance)
        {
            return await _jobListingRepo.AddAsync(newInstance);
        }


        public async Task<bool> DeleteAsync(int id)
        {
            return await _jobListingRepo.DeleteAsync(id);
        }

        public IEnumerable<Job>? GetAll()
        {
            return _jobListingRepo.GetAll();
        }

        public IEnumerable<Job>? GetAllActive()
        {
            return _jobListingRepo.GetAllActive();
        }

        public Job? GetById(int id)
        {
            return _jobListingRepo.GetByCondition(j => j.JobID == id);
        }

        public Job? GetByName(string jobName)
        {
            return _jobListingRepo.GetByCondition(j => j.JobTitle == jobName);
        }

        public async Task<bool> UpdateAsync(Job updatedObj)
        {
            return await _jobListingRepo.UpdateAsync(updatedObj);
        }
    }
}